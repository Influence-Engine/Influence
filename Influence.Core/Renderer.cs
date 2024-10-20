using SDL3;
using System;

namespace Influence.Core
{
    public class Renderer
    {
        IntPtr rendererPtr;
        public IntPtr RendererHandle => rendererPtr;

        SDL.Rect viewport;

        public Renderer(IntPtr windowPtr, int width, int height)
        {
#if DEBUG
            Console.WriteLine($"Creating Renderer: Window Handle => {windowPtr.ToInt64()} ({width}x{height})");
#endif

            rendererPtr = SDL.CreateRenderer(windowPtr, string.Empty);

            if (rendererPtr == IntPtr.Zero)
            {
                Console.WriteLine("Unable to create a renderer. Error: " + SDL.GetError());
                return;
            }

#if DEBUG
            Console.WriteLine("New Renderer Handle: " + rendererPtr.ToInt64().ToString());
#endif

            viewport = new SDL.Rect();
            viewport.w = width;
            viewport.h = height;

            SDL.SetRenderViewport(rendererPtr, ref viewport);
        }

        ~Renderer()
        {
            SDL.DestroyRenderer(rendererPtr);
        }
    }
}
