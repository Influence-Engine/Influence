
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
    }
}
