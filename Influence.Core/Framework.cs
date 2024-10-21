﻿using SDL3;
using System;

namespace Influence.Core
{
    public class Framework
    {
        Window window;
        Renderer renderer;

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

                //Console.WriteLine(1000000000 / Time.preciseDeltaTime);
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
