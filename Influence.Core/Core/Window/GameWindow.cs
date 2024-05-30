using System;
using System.Linq;

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

        GameObject cameraObject;
        Camera camera;

        static IKeyboard keyboard;

        static Vector2 LastMousePosition;
        static float xRotation = 0f;

        /// <summary>Executes the main game loop.</summary>
        public override void Run()
        {
            IInputContext input = Window.CreateInput();
            keyboard = input.Keyboards.FirstOrDefault();
            if (keyboard != null)
            {
                keyboard.KeyDown += KeyDown;
            }
            for (int i = 0; i < input.Mice.Count; i++)
            {
                input.Mice[i].Cursor.CursorMode = CursorMode.Raw;
                input.Mice[i].MouseMove += OnMouseMove;
            }


            Window.Initialize();

            cameraObject = new GameObject();
            cameraObject.transform.position = new Vector3(0, 0, 3f);
            camera = cameraObject.AddComponent<Camera>();

            Camera.mainCamera = camera;

            Console.WriteLine("Can I print the mainCamera viewMatrix?: " + (Camera.mainCamera.viewMatrix).ToString());


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

            MoveCamera();

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

        // TESTING AREA :3
        #region Testing Zone (Remove later)

        void MoveCamera()
        {
            var moveSpeed = 2.5f * (float)Time.deltaTime;

            if (keyboard.IsKeyPressed(Key.W))
            {
                //Move forwards
                cameraObject.transform.Translate(cameraObject.transform.forward * moveSpeed);
            }
            if (keyboard.IsKeyPressed(Key.S))
            {
                //Move backwards
                cameraObject.transform.Translate(-cameraObject.transform.forward * moveSpeed);
            }
            if (keyboard.IsKeyPressed(Key.A))
            {
                //Move left
                cameraObject.transform.Translate(-cameraObject.transform.right * moveSpeed);
            }
            if (keyboard.IsKeyPressed(Key.D))
            {
                //Move right
                cameraObject.transform.Translate(cameraObject.transform.right * moveSpeed);
            }

            if (keyboard.IsKeyPressed(Key.Space))
            {
                //Move down
                cameraObject.transform.Translate(cameraObject.transform.up * moveSpeed);
            }

            if (keyboard.IsKeyPressed(Key.ShiftLeft))
            {
                //Move down
                cameraObject.transform.Translate(-cameraObject.transform.up * moveSpeed);
            }
        }

        unsafe void OnMouseMove(IMouse mouse, System.Numerics.Vector2 position)
        {
            var lookSensitivity = 1f * Time.deltaTime;
            if (LastMousePosition == default) { LastMousePosition = new Vector2(position.X, position.Y); }
            else
            {
                var mouseX = (position.X - LastMousePosition.x) * lookSensitivity;
                var mouseY = (position.Y - LastMousePosition.y ) * lookSensitivity;

                // Up Down
                xRotation -= mouseY;
                xRotation = Mathq.Clamp(xRotation, -89f, 89f);

                // Apply Left Right
                camera.transform.Rotate(0f, mouseX, 0f);

                Quaternion pitch = Quaternion.Euler(xRotation, 0f, 0f);
                Quaternion yaw = Quaternion.Euler(0f, mouseX, 0f);
                camera.transform.rotation = pitch * yaw;

                //camera.transform.rotation = Quaternion.Euler(0f, mouseX, 0f);
                //camera.transform.Rotate(Vector3.Up * mouseX);

                LastMousePosition = new Vector2(position.X, position.Y);
            }
        }

        void KeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            if (key == Key.Escape)
            {
                Window.Close();
            }
        }

        #endregion
    }
}
