using SDL3;

namespace Influence
{
    public static class Time
    {
        /// <summary>Time in seconds since the Framework has started.</summary>
        public static double time;
        /// <summary>Time interval in seconds from the last frame to the current one.</summary>
        public static double deltaTime;

        /// <summary>Time in nanoseconds since the Framework has started.</summary>
        public static ulong preciseTime;
        /// <summary>Time interval in nanoseconds from the last frame to the current one.</summary>
        public static ulong preciseDeltaTime;


        /// <summary>Time passed in seconds since the end of last frame.</summary>
        public static double frameTime
        {
            get
            {
                ulong deltaTicks = SDL.GetTickNS() - preciseTime;
                return deltaTicks * 1.0e-9;
            }
        }

        /// <summary>Time passed in nanoseconds since the end of last frame.</summary>
        public static ulong preciseFrameTime => SDL.GetTickNS() - preciseTime;
    }
}
