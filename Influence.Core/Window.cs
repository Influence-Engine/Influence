using SDL3;
using System;
using System.Drawing;

namespace Influence.Core
{
    public class Window
    {
        IntPtr windowPtr;
        public IntPtr WindowHandle => windowPtr;

        public string title
        {
            get => SDL.GetWindowTitle(windowPtr);
            set => SDL.SetWindowTitle(windowPtr, value);
        }

        public Vector2 windowSize
        {
            get
            {
                Vector2 size = new Vector2();
                SDL.GetWindowSize(windowPtr, out size.x, out size.y);
                return size;
            }
            set => SDL.SetWindowSize(windowPtr, value.x, value.y);
        }

        public Vector2 windowMinSize
        {
            get
            {
                Vector2 size = new Vector2();
                SDL.GetWindowMinimumSize(windowPtr, out size.x, out size.y);
                return size;
            }
            set => SDL.SetWindowMinimumSize(windowPtr, value.x, value.y);
        }
        public Vector2 windowMaxSize
        {
            get
            {
                Vector2 size = new Vector2();
                SDL.GetWindowMaximumSize(windowPtr, out size.x, out size.y);
                return size;
            }
            set => SDL.SetWindowMaximumSize(windowPtr, value.x, value.y);
        }

        public bool fullscreen
        {
            get => ((SDL.WindowFlags)SDL.GetWindowFlags(windowPtr) & SDL.WindowFlags.Fullscreen) == 0;
        }

        public Window(int width, int height, string title, bool resizable, bool fullscreen)
        {
            if(string.IsNullOrEmpty(title))
                title = "Influence";

            CreateWindow(width, height, title, resizable, fullscreen);
        }

        void CreateWindow(int width, int height, string title, bool resizable, bool fullscreen)
        {
#if DEBUG
            Console.WriteLine($"Creating Window: {title} ({width}x{height})");
#endif

            SDL.WindowFlags flags = new SDL.WindowFlags();

            if (fullscreen)
                flags |= SDL.WindowFlags.Fullscreen;

            if (resizable)
                flags |= SDL.WindowFlags.Resizable;

            windowPtr = SDL.CreateWindow(title, width, height, flags);

            if (windowPtr == IntPtr.Zero)
            {
                Console.WriteLine("Unable to create a window. Error: " + SDL.GetError());
                return;
            }

#if DEBUG
            Console.WriteLine("New Window Handle: " + windowPtr.ToInt64().ToString());
#endif
        }

        void ProcessInput()
        {
            SDL.Event e;
            while(SDL.PollEvent(out e) != 0)
            {
                Console.WriteLine(e.type.ToString());
                /*if(e.type == SDL.EventType.Quit)
                {
                    isRunning = false;
                }*/
            }
        }

        ~Window()
        {
            SDL.DestroyWindow(windowPtr);
        }
    }
}
