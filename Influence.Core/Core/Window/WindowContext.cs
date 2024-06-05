using System;

using Influence.Core;

using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Influence.Window
{
    /// <summary>
    /// An abstract base class for managing window contexts in an application. <br/>
    /// Implements basic functionalities such as window resizing, updating, rendering, and disposing of resources.
    /// </summary>
    public abstract class WindowContext : IDisposable
    {
        IWindow window;
        /// <summary>The window being managed by this context.</summary>
        public IWindow Window => window;

        GLContext glContext;

        /// <summary>The OpenGL context associated with this window.</summary>
        public GLContext GLContext => glContext;

        /// <summary>The OpenGL API isntance managed by the GLContext.</summary>
        public GL OpenGL => GLContext.OpenGL;

        string _title;
        /// <summary>The title of the window.</summary>
        public string title
        {
            get => _title;
            set
            {
                title = value;
                UpdateWindowTitle();
            }
        }

        /// <summary>The size of the window.</summary>
        public Vector2Int size
        {
            get
            {
                return new Vector2Int(window.Size.X, window.Size.Y);
            }
            set
            {
                window.Size = new Vector2D<int>(value.x, value.y);
                UpdateWindowTitle();
            }
        }

        // The title that is shown at the top of the window.
        protected string WindowTitle => string.Format("{0} ({1}x{2}) - ({3})", title, size.x, size.y, "OpenGL");
        protected void UpdateWindowTitle() => window.Title = WindowTitle;

        /// <summary>The target framerate for the window.</summary>
        public int targetFramerate
        {
            get => (int)window.UpdatesPerSecond;
            set
            {
                window.UpdatesPerSecond = value;
                window.FramesPerSecond = value; 
            }
        }

        /// <summary>The fixed framerate for the window.</summary>
        public int fixedFramerate = 30;

        /// <summary>Indicates whether vertical synchronization (VSync) is enabled.</summary>
        public bool vSync
        {
            get => window.VSync;
            set => window.VSync = value;
        }

        /// <summary>Indicates whether the window is in fullscreen mode.</summary>
        public bool fullscreen
        {
            get => window.WindowState == WindowState.Fullscreen;
            set => window.WindowState = value ? WindowState.Fullscreen : WindowState.Normal;
        }

        /// <summary>Initializes a new instance of the WindowContext class with specified size and title.</summary>
        /// <param name="windowOptions">The initial options of the window.</param>
        public WindowContext(WindowOptions windowOptions)
        {
            // Set the title :3
            _title = title;

            // Create the Window
            window = Silk.NET.Windowing.Window.Create(windowOptions.ToSilk());

            // Initialize / Create the window on the underlying platform.
            window.Initialize();

            // Initialize a new GL instance for the window.
            glContext = new GLContext(this);

            // Initialize / hook the Input System to the window
            Input.Initialize(window);

            // Update window title
            UpdateWindowTitle();

            // Register / Handle Events
            window.Update += Update;
            window.Render += Render;
            window.Resize += (size) => OnWindowResized(new Vector2Int(size.X, size.Y));
            window.FocusChanged += OnWindowFocus;
            window.Closing += OnWindowQuit;
            window.StateChanged += OnWindowStateChanged;
        }

        /// <summary>Initializes a new instance of the WindowContext class with specified size and title.</summary>
        /// <param name="size">The initial size of the window.</param>
        /// <param name="title">The initial title of the window.</param>
        public WindowContext(Vector2Int size, string title = "Influence") : this(new WindowOptions(size, title)) { }

        /// <summary>Runs the main loop of the window context.</summary>
        public abstract void Run();

        /// <summary>Disposes of the window context.</summary>
        public virtual void Dispose()
        {
            window.Dispose();
            OpenGL.Dispose();
        }

        ~WindowContext()
        {
            Dispose();
        }


        #region Functions

        // QUESTION should we allow these to be set publicly and manually? Giving full controll to creators.

        /// <summary>Enables depth testing in the OpenGL context.</summary>
        internal void EnableDepth() => OpenGL.Enable(EnableCap.DepthTest);

        /// <summary>Enables culling face in the OpenGL context.</summary>
        internal void EnableCullFace() => OpenGL.Enable(EnableCap.CullFace);

        /// <summary>Clears the color and depth buffers in the OpenGL context.</summary>
        internal void Clear() => OpenGL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        /// <summary>Flushes the OpenGL context.</summary>
        internal void Flush() => OpenGL.Flush();

        #endregion

        #region Override Functions

        /// <summary>Update is called every frame. </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        protected abstract void Update(double deltaTime);

        /// <summary>Render is called every frame after Update. Use Render for all rendering related content. </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        protected abstract void Render(double deltaTime);

        // QUESTION should we make these public and give full controll to creators.

        /// <summary>Handles window resize events.</summary>
        /// <param name="size">The new size of the window.</param>
        protected virtual void OnWindowResized(Vector2Int size)
        {
            UpdateWindowTitle();
            glContext.SetViewport(size);
        }

        /// <summary>Handles window focus change events.</summary>
        /// <param name="focus">Whether the window has gained focus.</param>
        protected virtual void OnWindowFocus(bool focus) { }

        /// <summary>Handles window quit events.</summary>
        protected virtual void OnWindowQuit() { }

        // TODO use custom enum to get away from Silk.NET Library

        /// <summary>Handles window state change events.</summary>
        /// <param name="state">The new state of the window.</param>
        protected virtual void OnWindowStateChanged(WindowState state) { }

        #endregion

    }
}
