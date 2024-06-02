using System.Collections.Generic;

using Silk.NET.Input;
using Silk.NET.Windowing;

namespace Influence
{
    public static class Input
    {
        static IInputContext inputContext; // Current Input Context
        internal static IKeyboard activeKeyboard; // Last used Keyboard
        internal static IMouse activeMouse; // Last used Mouse

        static Dictionary<int, bool> keyStates = new Dictionary<int, bool>();

        public static Vector2 mousePosition => new Vector2(activeMouse.Position.X, activeMouse.Position.Y);


        internal static void Initialize(IWindow window)
        {
            inputContext = window.CreateInput();
            RegisterInputs();
        }

        static void RegisterInputs()
        {
            // Register key Events on all keyboards.
            foreach (IKeyboard keyboard in inputContext.Keyboards)
            {
                // We remove the event to ensure we don't have it multiple times.
                keyboard.KeyDown -= OnKeyDown;
                keyboard.KeyUp -= OnKeyUp;

                keyboard.KeyDown += OnKeyDown;
                keyboard.KeyUp += OnKeyUp;
            }

            // Default to the first Keyboard as our "active"
            if(inputContext.Keyboards.Count > 0)
                activeKeyboard = inputContext.Keyboards[0];

            // Register button Events on all mice.
            foreach (IMouse mouse in inputContext.Mice)
            {
                mouse.MouseDown -= OnMouseDown;
                mouse.MouseUp -= OnMouseUp;

                mouse.MouseDown += OnMouseDown;
                mouse.MouseUp += OnMouseUp;
                mouse.Cursor.CursorMode = CursorMode.Raw;
            }

            // Default to the first Mouse as our "active"
            if(inputContext.Mice.Count > 0)
                activeMouse = inputContext.Mice[0];
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

        public static bool IsKeyDown(KeyCode key) => keyStates.GetValueOrDefault((int)key, false);
        public static bool IsKeyUp(KeyCode key) => !keyStates.GetValueOrDefault((int)key, false);

        #endregion

        #region Mouse Functions

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

        public static bool IsMouseButtonDown(byte mouseButton) => keyStates.GetValueOrDefault(mouseButton, false);
        public static bool IsMouseButtonUp(byte mouseButton) => !keyStates.GetValueOrDefault(mouseButton, false);

        #endregion
    }
}
