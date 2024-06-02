using System.Collections.Generic;

using Silk.NET.Input;

using Influence.Core;

namespace Influence.Input
{
    public static class Input
    {
        static IInputContext inputContext;
        static Dictionary<Key, bool> keyStates = new Dictionary<Key, bool>();

        internal static void Initialize(WindowContext window)
        {
            inputContext = window.Window.CreateInput();

            foreach(var keyboard in inputContext.Keyboards)
            {
                keyboard.KeyDown += OnKeyDown;
                keyboard.KeyUp += OnKeyUp;
            }
        }

        static void OnKeyDown(IKeyboard keyboard, Key key, int keyCode) => keyStates[key] = true;
        static void OnKeyUp(IKeyboard keyboard, Key key, int keyCode) => keyStates[key] = false;

        public static bool IsKeyDown(Key key) => keyStates.GetValueOrDefault(key, false);
        public static bool IsKeyUp(Key key) => !keyStates.GetValueOrDefault(key, false);
    }
}
