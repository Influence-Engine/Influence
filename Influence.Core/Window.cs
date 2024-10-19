using SDL3;
using System;

namespace Influence.Core
{
    public class Window
    {
        Vector2 curWindowSize = new();
        public Vector2 WindowSize => curWindowSize;

        IntPtr windowPtr;
        IntPtr renderPtr;
        SDL.Rect viewport = new();

        bool videoInitilialized;

        public Window(int width, int height, string title, bool resizable, bool fullscreen)
        {
            InitializeVideo();

            if(string.IsNullOrEmpty(title))
                title = "Influence";

            SDL.WindowFlags flags = new SDL.WindowFlags();

            if(fullscreen)
                flags |= SDL.WindowFlags.Fullscreen;

            if(resizable)
                flags |= SDL.WindowFlags.Resizable;

            windowPtr = SDL.CreateWindow(title, width, height, flags);

            if(windowPtr == IntPtr.Zero)
            {
                Console.WriteLine("Unable to create a window. Error: " + SDL.GetError());
                return;
            }

            renderPtr = SDL.CreateRenderer(windowPtr, -1, SDL.RendererFlags.Accelerated);

            viewport.w = width;
            viewport.h = height;

            SDL.SetRenderViewport(renderPtr, ref viewport);

            curWindowSize = new Vector2(width, height);
        }

        void InitializeVideo()
        {
            if (videoInitilialized)
                return;

            if(SDL.Init(SDL.InitFlags.Video) < 0)
            {
                Console.WriteLine("Unable to initialize SDL Video. Error: " + SDL.GetError());
                return;
            }

            videoInitilialized = true;
        }


        void CreateWindow()
        {

        }
    }
}
