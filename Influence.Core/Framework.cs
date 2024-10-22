using SDL3;
using System;

namespace Influence.Core
{
    public class Framework
    {
        double timePerFrame = 0.016666666666666667;
        int _targetFramerate = 60;
        public int targetFramerate
        {
            set
            {
                timePerFrame = 1d / value;
                _targetFramerate = value;
            }
            get => _targetFramerate;
        }

        Window window;
        public Window Window => window;

        Renderer renderer;
        public Renderer Renderer => renderer;

        bool videoInitialized;
        bool simulating;

        public Framework(Vector2 size, string title)
        {
            InitializeVideo();

            window = new Window(size.x, size.y, title, true, false);
            renderer = new Renderer(window.WindowHandle, size.x, size.y);

            Simulate();
        }

        void InitializeVideo()
        {
            if (videoInitialized)
                return;

            if (SDL.Init(SDL.InitFlags.Video) < 0)
            {
                Console.WriteLine("Unable to initialize SDL Video. Error: " + SDL.GetError());
                return;
            }

            videoInitialized = true;
        }

        protected virtual void Simulate()
        {
            simulating = true;
            SDL.Event e;

            while(simulating)
            {
                ulong currentTicks = SDL.GetTickNS();

                Time.preciseDeltaTime = currentTicks - Time.preciseTime;
                Time.deltaTime = Time.preciseDeltaTime * 1.0e-9;

                while(SDL.PollEvent(out e) && e.type != SDL.EventType.PollSentinel)
                {
                    ProccessEvents(e);
                }

                Render();

                Time.time += Time.deltaTime;
                Time.preciseTime += Time.preciseDeltaTime;

                Input.Reset();

                double passedTime = Time.frameTime;
                if(passedTime < timePerFrame)
                {
                    uint delay = (uint)((timePerFrame - passedTime) * 1000);
                    SDL.Delay(delay);
                }
            }

            SDL.ClearError();
        }

        protected virtual void ProccessEvents(SDL.Event e)
        {
            switch (e.type)
            {
                case SDL.EventType.Quit:

#if DEBUG
                    Console.WriteLine("Closing Framework requested...");
#endif
                    simulating = false;

                    SDL.Quit();
                    break;
                case SDL.EventType.KeyDown:
                    Input.keyStates[e.key.key] = true;
                    Input.keysPressedThisFrame.Add(e.key.key);
                    break;
                case SDL.EventType.KeyUp:
                    Input.keyStates[e.key.key] = false;
                    Input.keysReleasedThisFrame.Add(e.key.key);
                    break;
                case SDL.EventType.MouseMotion:
                    Input.mousePosition = new Vector2(e.motion.x, e.motion.y);
                    Input.mouseDelta = new Vector2(e.motion.xRel, e.motion.yRel);
                    break;
            }
        }

        protected virtual void Render()
        {
            // Clear the screen
            Clear();

            // Set background Color



            // Lastly Display it all
            Display();
        }

        public void Quit()
        {
            SDL.Event e = new();
            e.type = SDL.EventType.Quit;

            SDL.PushEvent(ref e);
        }

        /// <summary>Clear the renderer. Call before rendering context.</summary>
        public void Clear() => renderer.Clear();

        /// <summary>Update the screen with any rendering performed since the previous call.</summary>
        public void Display() => renderer.Display();
    }
}
