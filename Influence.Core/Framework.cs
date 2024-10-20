using SDL3;
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

        public void Simulate()
        {
            simulating = true;

            while(simulating)
            {
                ulong currentTicks = SDL.GetTickNS();

                Time.preciseDeltaTime = currentTicks - Time.preciseTime;
                Time.deltaTime = Time.preciseDeltaTime * 1.0e-9;

                Time.time += Time.deltaTime;
                Time.preciseTime += Time.preciseDeltaTime;

                Console.WriteLine(1000000000 / Time.preciseDeltaTime);
            }
        }
    }
}
