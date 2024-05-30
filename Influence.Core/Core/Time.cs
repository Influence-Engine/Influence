
namespace Influence
{
    /// <summary>
    /// A utility class for managing various types of time measurements in the application.
    /// <br/>
    /// Provides access to total elapsed time, delta times, time scales, and frame counts.
    /// </summary>
    public static class Time
    {
        #region Game Time

        internal static double timeAsDouble {  get; set; }

        /// <summary>Total elapsed time since the application started.</summary>
        public static float time => (float)timeAsDouble;

        internal static double deltaTimeAsDouble { get; set; }
        /// <summary>
        /// Delta time for the last frame.
        /// <br/>
        /// Representing the time passed since the last update.
        /// </summary>
        public static float deltaTime => (float)deltaTimeAsDouble;

        internal static double smoothDeltaTimeAsDouble { get; set; }
        /// <summary>Smoothed delta time for constistancy across frames.</summary>
        public static float smoothDeltaTime => (float)smoothDeltaTimeAsDouble;

        internal static double unscaledDeltaTimeAsDouble { get; set; }
        /// <summary>Unscaled delta time, unaffected by time scale changes.</summary>
        public static float unscaledDeltaTime => (float)unscaledDeltaTimeAsDouble;

        /// <summary>Time scale factor.</summary>
        public static float timeScale { get; set; } = 1f;

        // Accumulated time scale factor, used internally for calculations involving time scaling.
        internal static double accumaltedTimeScaleFactor = 1d;

        /// <summary>Frame count calculated based on smoothed delta time. </summary>
        public static int frameCount => (int)(1f / smoothDeltaTimeAsDouble);

        #endregion

        #region Render Time

        internal static double renderDeltaTimeAsDouble { get; set; }

        /// <summary>Render delta time, specifically for rendering operations, potentially different from gameplay delta time.</summary>
        public static float renderDeltaTime => (float)renderDeltaTimeAsDouble;

        internal static double smoothRenderDeltaTimeAsDouble { get; set; }

       /// <summary>Smoothed render delta time, similar to smoothDeltaTime but optimized for rendering calculations.</summary>
        public static float smoothRenderDeltaTime => (float)smoothRenderDeltaTimeAsDouble;

        /// <summary>Frame count calculated based on smoothed render delta time. </summary>
        public static int renderFrameCount => (int)(1f / smoothRenderDeltaTimeAsDouble);

        #endregion

    }
}
