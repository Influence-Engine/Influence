
namespace Influence
{
    public struct Rect
    {
        public int x;
        public int y;

        public int w;
        public int h;

        public Rect()
        {
            this.x = 0;
            this.y = 0;

            this.w = 0;
            this.h = 0;
        }

        public Rect(int x, int y)
        {
            this.x = x;
            this.y = y;

            this.w = 0;
            this.h = 0;
        }

        public Rect(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;

            this.w = w;
            this.h = h;
        }

        public Rect(Vector2 size)
        {
            this.x = (int)size.x;
            this.y = (int)size.y;

            this.w = 0;
            this.h = 0;
        }

        public Rect(Vector2Int size)
        {
            this.x = size.x;
            this.y = size.y;

            this.w = 0;
            this.h = 0;
        }

        public Rect(Vector2 size, Vector2 dimensions)
        {
            this.x = (int)size.x;
            this.y = (int)size.y;

            this.w = (int)dimensions.x;
            this.h = (int)dimensions.y;
        }

        public Rect(Vector2Int size, Vector2Int dimensions)
        {
            this.x = size.x;
            this.y = size.y;

            this.w = dimensions.x;
            this.h = dimensions.y;
        }

        /*public SDL.SDL_Rect GetSDLRect()
        {
            SDL.SDL_Rect rect;

            rect.x = x;
            rect.y = y;
            rect.w = w;
            rect.h = h;

            return rect;
        }*/

        public Vector2 Center => new Vector2(x + w / 2, y + h / 2);
        /*public SDL.SDL_Point GetSDLCenterPoint()
        {
            SDL.SDL_Point point;

            point.x = x + w / 2;
            point.y = y + h / 2;

            return point;
        }*/

        public override string ToString()
        {
            return $"({x}, {y}, {w}, {h})";
        }
    }
}
