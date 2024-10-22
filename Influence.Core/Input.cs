using SDL3;
using System.Collections.Generic;

namespace Influence
{
    // TODO Use our own KeyCodes
    // TODO look at a different system to handle press and release
    public static class Input
    {
        public static Dictionary<SDL.KeyCode, bool> keyStates = new Dictionary<SDL.KeyCode, bool>();
        public static HashSet<SDL.KeyCode> keysPressedThisFrame = new HashSet<SDL.KeyCode>();
        public static HashSet<SDL.KeyCode> keysReleasedThisFrame = new HashSet<SDL.KeyCode>();

        public static bool GetKey(SDL.KeyCode key) => keyStates.ContainsKey(key) && keyStates[key];
        public static  bool GetKeyDown(SDL.KeyCode key)  => keysPressedThisFrame.Contains(key);
        public static bool GetKeyUp(SDL.KeyCode key) => keysReleasedThisFrame.Contains(key);
    }
}
