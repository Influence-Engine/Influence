using Silk.NET.Maths;

namespace Influence.Window
{
    public struct WindowOptions
    {
        /// <summary>The window title.</summary>
        public string title;

        /// <summary>The position of the window.</summary>
        public Vector2Int position;
        /// <summary>The size of the window in pixels.</summary>
        public Vector2Int size;

        /// <summary>The number of operations to run every second.</summary>
        public int targetFramerate;
        /// <summary>Whether or not VSync is enabled for this view.</summary>
        public bool vSync;

        /// <summary>Whether or not the window is visible.</summary>
        public bool isVisible;
        /// <summary>Whether or not the window should be on top of all other windows.</summary>
        public bool topMost;

        /// <summary>Should SwapBuffers be called automatically at the end of each Render event?</summary>
        public bool swapBuffersAutomatically;

        /// <summary>Sensible default options for creating a window.</summary>
        public static WindowOptions Default { get; }

        public WindowOptions(bool isVisible, Vector2Int position, Vector2Int size, string title, int targetFramerate, bool vSync = true, bool swapBuffersAutomatically = true, bool topMost = false)
        {
            this.title = title;

            this.position = position;
            this.size = size;

            this.targetFramerate = targetFramerate;
            this.vSync = vSync;

            this.isVisible = isVisible;
            this.topMost = topMost;

            this.swapBuffersAutomatically = swapBuffersAutomatically;
        }
        public WindowOptions(Vector2Int size, string title): this(true, new Vector2Int(64, 64), size, title, 60) { }

        static WindowOptions()
        {
            string title = "Influence";
            Default = new WindowOptions(true, new Vector2Int(64, 64), new Vector2Int(1280, 720), title, 60, true, true, false);
        }
    }

    internal static class WindowOptionsExtensions
    {
        internal static Silk.NET.Windowing.WindowOptions ToSilk(this WindowOptions settings)
        {
            Silk.NET.Windowing.WindowOptions options = Silk.NET.Windowing.WindowOptions.Default with
            {
                Title = settings.title,

                Size = new Vector2D<int>(settings.size.x, settings.size.y),
                Position = new Vector2D<int>(settings.position.x, settings.position.y),

                UpdatesPerSecond = settings.targetFramerate,
                FramesPerSecond = settings.targetFramerate,
                VSync = settings.vSync,

                IsVisible = settings.isVisible,
                TopMost = settings.topMost,

                ShouldSwapAutomatically = settings.swapBuffersAutomatically
            };

            return options;
        }
    }
}
