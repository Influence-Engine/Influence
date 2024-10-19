
namespace Influence
{
    public struct Rect
    {
        public int x;
        public int y;

        public int width;
        public int height;

        public Rect()
        {
            x = 0;
            y = 0;

            width = 0;
            height = 0;
        }

        public Rect(int x, int y)
        {
            this.x = x;
            this.y = y;

            width = 0;
            height = 0;
        }

        public Rect(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;

            this.width = width;
            this.height = height;
        }

        public Rect(Vector2 size)
        {
            this.x = size.x;
            this.y = size.y;

            width = 0;
            height = 0;
        }

        public Rect(Vector2 size, Vector2 dimension)
        {
            this.x = size.x;
            this.y = size.y;

            this.width = dimension.x;
            this.height = dimension.y;
        }

        public Vector2 Center => new Vector2(x + width / 2, y + height / 2);

        public override string ToString() => $"({x}, {y}, {width}, {height})";
    }
}
