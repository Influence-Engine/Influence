using System.Collections.Generic;

using Silk.NET.Input;
using Silk.NET.Windowing;

namespace Influence
{
    public static class Input
    {
        static IInputContext inputContext; // Current Input Context

        static IKeyboard activeKeyboard; // Last used Keyboard
        internal static IReadOnlyList<IKeyboard> keyboards; // All active Keyboards

        static IMouse activeMouse; // Last used Mouse
        internal static IReadOnlyList<IMouse> mice; // All active Mice

        static Dictionary<int, bool> keyStates = new Dictionary<int, bool>();

        static Vector2 _inBoundPosition = Vector2.Zero;

        /// <summary>Gets the current position of the mouse within the application's bounds.</summary>
        public static Vector2 mousePositionInBound => _inBoundPosition;

        /// <summary>Gets the current position of the mouse.</summary>
        public static Vector2 mousePosition => new Vector2(activeMouse.Position.X, activeMouse.Position.Y);

        /// <summary>
        /// Initializes the input system with the specified window. <br/>
        /// Registers input event handlers for keyboards and mice.
        /// </summary>
        /// <param name="window"></param>
        internal static void Initialize(IWindow window)
        {
            inputContext = window.CreateInput();
            RegisterInputs();
        }

        // Registers input event handlers for all keyboards and mice.
        static void RegisterInputs()
        {
            keyboards = inputContext.Keyboards;

            // Register key Events on all keyboards.
            foreach (IKeyboard keyboard in keyboards)
            {
                // We remove the event to ensure we don't have it multiple times.
                keyboard.KeyDown -= OnKeyDown;
                keyboard.KeyUp -= OnKeyUp;

                keyboard.KeyDown += OnKeyDown;
                keyboard.KeyUp += OnKeyUp;
            }

            // Default to the first Keyboard as our "active"
            if(keyboards.Count > 0)
                activeKeyboard = keyboards[0];

            mice = inputContext.Mice;

            // Register button Events on all mice.
            foreach (IMouse mouse in mice)
            {
                mouse.MouseMove -= OnMouseMove;
                mouse.MouseDown -= OnMouseDown;
                mouse.MouseUp -= OnMouseUp;

                mouse.MouseMove += OnMouseMove;
                mouse.MouseDown += OnMouseDown;
                mouse.MouseUp += OnMouseUp;
            }

            // Default to the first Mouse as our "active"
            if(mice.Count > 0)
                activeMouse = mice[0];
        }

        #region Keyboard Functions

        static void OnKeyDown(IKeyboard keyboard, Key key, int keyCode)
        {
            activeKeyboard = keyboard;
            keyStates[(int)key] = true;
        }
        static void OnKeyUp(IKeyboard keyboard, Key key, int keyCode)
        {
            activeKeyboard = keyboard;
            keyStates[(int)key] = false;
        }

        /// <summary>Returns true if the given KeyCode is currently being pressed down.</summary>
        /// <param name="key">The Key to check for.</param>
        /// <returns></returns>
        public static bool IsKeyDown(KeyCode key) => keyStates.GetValueOrDefault((int)key, false);

        /// <summary>Returns true if the given KeyCode is currently not pressed down.</summary>
        /// <param name="key">The Key to check for.</param>
        /// <returns></returns>
        public static bool IsKeyUp(KeyCode key) => !keyStates.GetValueOrDefault((int)key, false);

        #endregion

        #region Mouse Functions

        static void OnMouseMove(IMouse mouse, System.Numerics.Vector2 position)
        {
            activeMouse = mouse;
            _inBoundPosition = new Vector2(position.X, position.Y);
        }

        static void OnMouseDown(IMouse mouse, MouseButton mouseButton)
        {
            activeMouse = mouse;
            keyStates[(int)mouseButton] = true;
        }
        static void OnMouseUp(IMouse mouse, MouseButton mouseButton)
        {
            activeMouse = mouse;
            keyStates[(int)mouseButton] = false;
        }

        /// <summary>Returns true if the given Mouse Button is currently being pressed down.</summary>
        /// <param name="mouseButton">0 = Left Click, 1 = Right Click, etc.</param>
        /// <returns></returns>
        public static bool IsMouseButtonDown(byte mouseButton) => keyStates.GetValueOrDefault(mouseButton, false);

        /// <summary>Returns true if the given Mouse Button is currently not being pressed down.</summary>
        /// <param name="mouseButton">0 = Left Click, 1 = Right Click, etc.</param>
        /// <returns></returns>
        public static bool IsMouseButtonUp(byte mouseButton) => !keyStates.GetValueOrDefault(mouseButton, false);

        #endregion
    }
}
