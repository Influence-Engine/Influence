using System;

using Influence.Core;

using Silk.NET.Input;

namespace Influence
{
    /// <summary>Represents a game window, inheriting from WindowContext to manage window-related operations.</summary>
    public class GameWindow : WindowContext
    {
        /// <summary>Initializes a new instance of the GameWindow class with specified size and title.</summary>
        /// <param name="size">The initial size of the window.</param>
        /// <param name="title">The initial title of the window.</param>
        public GameWindow(Vector2Int size, string title = "Influence") : base(size, title) {}

        /// <summary>Executes the main game loop.</summary>
        public override void Run()
        {
            // Starts game loop
            Window.Run(delegate
            {
                Window.DoEvents();

                if(!Window.IsClosing)
                {
                    Window.DoUpdate();

                    Window.DoRender();
                }

            });

            Window.DoEvents();

            Window.Reset();
        }

        /// <summary>Updates the game state based on elapsed time and processes all updates.</summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        protected override void Update(double deltaTime)
        {
            Time.unscaledDeltaTimeAsDouble = deltaTime;

            Time.accumaltedTimeScaleFactor *= Time.timeScale;
            deltaTime *= Time.accumaltedTimeScaleFactor;

            Time.smoothDeltaTimeAsDouble = (Time.smoothDeltaTimeAsDouble * 0.9d) + (deltaTime * 0.1d);
            Time.timeAsDouble += deltaTime;
            Time.deltaTimeAsDouble = deltaTime;

            //Console.WriteLine("FPS: " + Time.frameCount);

            HandleAllUpdates();
        }

        // Goes through all registered Singularities and runs their update method.
        void HandleAllUpdates()
        {
            int count = CreationRegistry.registeredSingularitys.Count; // Cache Count of Singularitys
            for (int i = 0; i < count; i++) // Go through all Singularitys
            {
                Singularity singularity = CreationRegistry.registeredSingularitys[i]; // Cache for simplicity

                if (!singularity.activeSelf) // If not active.. we skip
                    continue;

                singularity.Update();
            }
        }

        /// <summary>Renders the current frame, clearing the screen and rendering all visible entities.</summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        protected override void Render(double deltaTime)
        {
            Time.renderDeltaTimeAsDouble = deltaTime;
            Time.smoothRenderDeltaTimeAsDouble = (Time.smoothRenderDeltaTimeAsDouble * 0.9d) + (deltaTime * 0.1d);

            //Console.WriteLine("Render FPS: " + Time.renderFrameCount);
            EnableDepth();
            Clear();

            RenderAllRenderables();
        }

        // Goes through all registered Renderables and runs their render method.
        void RenderAllRenderables()
        {
            int count = CreationRegistry.RenderablesCount; // Cache Count of Renderables
            for (int i = 0; i < count; i++) // Go through all Renderables
            {
                IRenderable renderable = CreationRegistry.registeredRenderables[i]; // Cache for simplicity
                if (renderable is Component c) // Check renderable is on a component
                {
                    if (!c.activeSelf) // If not active.. we skip
                        continue;

                    renderable.Render();
                }
            }
        }

        /// <summary>
        /// Toggles the visibility of the mouse cursor.
        /// </summary>
        /// <param name="visible">True to show the cursor, false to hide it.</param>
        public void SetCursorVisibility(bool visible)
        {
            var cursorMode = visible ? CursorMode.Normal : CursorMode.Disabled;
            foreach (var mouse in Input.mice)
            {
                mouse.Cursor.CursorMode = cursorMode;
            }
        }
    }
}
