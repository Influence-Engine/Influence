using Silk.NET.Input;

namespace Influence
{
    public static class Cursor
    {
        public static Vector2 position
        {
            get
            {
                if (onlyTrackInBounds)
                    return Input.mousePositionInBound;

                return Input.mousePosition;
            }
        }

        public static bool onlyTrackInBounds = false;

        static bool _visibility = true;
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
