using SDL3;
using System;

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
            get => ((SDL.WindowFlags)SDL.GetWindowFlags(windowPtr) & SDL.WindowFlags.Fullscreen) != 0;
            set => SDL.SetWindowFullscreen(windowPtr, value);
        }

        public bool alwaysOnTop
        {
            get => ((SDL.WindowFlags)SDL.GetWindowFlags(windowPtr) & SDL.WindowFlags.AlwaysOnTop) != 0;
            set => SDL.SetWindowAlwaysOnTop(windowPtr, value);
        }

        public bool borderless
        {
            get => ((SDL.WindowFlags)SDL.GetWindowFlags(windowPtr) & SDL.WindowFlags.Borderless) != 0;
            set => SDL.SetWindowBordered(windowPtr, !value);
        }

        public Window(int width = 512, int height = 512, string title = "Influence",  bool resizable = false, bool fullscreen = false)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Window width and height must be greater than 0.");

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be a null or empty", nameof(title));

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

        ~Window()
        {
            SDL.DestroyWindow(windowPtr);
        }


    }
}
