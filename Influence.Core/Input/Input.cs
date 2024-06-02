using System.Collections.Generic;

using Silk.NET.Input;
using Silk.NET.Windowing;

namespace Influence
{
    public static class Input
    {
        static IInputContext inputContext;
        static Dictionary<int, bool> keyStates = new Dictionary<int, bool>();

        static IMouse mouse;
        public static Vector2 mousePosition => new Vector2(mouse.Position.X, mouse.Position.Y);

        internal static void Initialize(IWindow window)
        {
            inputContext = window.CreateInput();

            foreach(var keyboard in inputContext.Keyboards)
            {
                keyboard.KeyDown += OnKeyDown;
                keyboard.KeyUp += OnKeyUp;
            }

            mouse = inputContext.Mice[0];
            mouse.Cursor.CursorMode = CursorMode.Raw;
            // Create hookable Action where which you can register too for MouseMove Event
        }

        static void OnKeyDown(IKeyboard keyboard, Key key, int keyCode) => keyStates[(int)key] = true;
        static void OnKeyUp(IKeyboard keyboard, Key key, int keyCode) => keyStates[(int)key] = false;

        public static bool IsKeyDown(KeyCode key) => keyStates.GetValueOrDefault((int)key, false);
        public static bool IsKeyUp(KeyCode key) => !keyStates.GetValueOrDefault((int)key, false);
    }
}
