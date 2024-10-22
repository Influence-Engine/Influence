using SDL3;
using System.Collections.Generic;

namespace Influence
{
    // TODO Use our own KeyCodes
    // TODO look at a different system to handle press and release
    public static class Input
    {
        #region Keyboard

        public static Dictionary<SDL.KeyCode, bool> keyStates = new Dictionary<SDL.KeyCode, bool>();
        public static HashSet<SDL.KeyCode> keysPressedThisFrame = new HashSet<SDL.KeyCode>();
        public static HashSet<SDL.KeyCode> keysReleasedThisFrame = new HashSet<SDL.KeyCode>();

        /// <summary>Returns true if the key is being held.</summary>
        public static bool GetKey(SDL.KeyCode key) => keyStates.ContainsKey(key) && keyStates[key];

        /// <summary>Returns true if the key was pressed down this frame.</summary>
        public static  bool GetKeyDown(SDL.KeyCode key)  => keysPressedThisFrame.Contains(key);

        /// <summary>Returns true if the key was released this frame.</summary>
        public static bool GetKeyUp(SDL.KeyCode key) => keysReleasedThisFrame.Contains(key);

        #endregion

        #region Mouse

        public static Vector2 mousePosition = Vector2.zero;
        public static Vector2 mouseDelta = Vector2.zero;

        #endregion

        /// <summary>
        /// Resets input states for the next frame. <br></br>
        /// Call at the end of each frame to prepare for the next frame's input processing.
        /// </summary>
        public static void Reset()
        {
            keysPressedThisFrame.Clear();
            keysReleasedThisFrame.Clear();

            mouseDelta = Vector2.zero;
        }
    }
}
