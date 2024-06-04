using Silk.NET.Input;

namespace Influence
{
    public static class Cursor
    {
        /// <summary>Gets the current cursor position.</summary>
        public static Vector2 position
        {
            get
            {
                if (onlyTrackInBounds)
                    return Input.mousePositionInBound;

                return Input.mousePosition;
            }
        }

        /// <summary>Indicates whether the application should track the cursor position within its bounds only.</summary>
        public static bool onlyTrackInBounds = false;

        static bool _visibility = true;

        /// <summary>Gets or sets whether the mouse cursor is visible.</summary>
        public static bool visible
        {
            get { return _visibility; }
            set
            {
                _visibility = value;

                CursorMode cursorMode = value ? CursorMode.Normal : CursorMode.Disabled;
                foreach (IMouse mouse in Input.mice)
                {
                    mouse.Cursor.CursorMode = cursorMode;
                }
            }
        }
    }
}
