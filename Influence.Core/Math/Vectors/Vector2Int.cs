using System;

namespace Influence
{
    public struct Vector2Int
    {
        public int x;
        public int y;

        public Vector2Int()
        {
            x = 0;
            y = 0;
        }
        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2Int(float x, float y)
        {
            this.x = (int)x;
            this.y = (int)y;
        }

        public Vector2Int(Vector2 vector)
        {
            this.x = (int)vector.x;
            this.y = (int)vector.y;
        }
        public Vector2Int(Vector3 vector)
        {
            this.x = (int)vector.x;
            this.y = (int)vector.y;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        #region Quick Returns

        /// <summary>
        /// Returns Vector2Int : (0,0)
        /// </summary>
        public static Vector2Int zero => new Vector2Int(0, 0);

        /// <summary>
        /// Returns Vector2Int : (1,1)
        /// </summary>
        public static Vector2Int one => new Vector2Int(1, 1);

        /// <summary>
        /// Returns Vector2Int : (0,1)
        /// </summary>
        public static Vector2Int up => new Vector2Int(0, 1);

        /// <summary>
        /// Returns Vector2Int : (0,-1)
        /// </summary>
        public static Vector2Int down => new Vector2Int(0, -1);

        /// <summary>
        /// Returns Vector2Int : (-1,0)
        /// </summary>
        public static Vector2Int left => new Vector2Int(-1, 0);

        /// <summary>
        /// Returns Vector2Int : (1,0)
        /// </summary>
        public static Vector2Int right => new Vector2Int(1, 0);

        #endregion

        #region Operators

        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
            => new Vector2Int(a.x + b.x, a.y + b.y);
        public static Vector2Int operator +(Vector2Int a, int b)
            => new Vector2Int(a.x + b, a.y + b);

        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
            => new Vector2Int(a.x - b.x, a.y - b.y);
        public static Vector2Int operator -(Vector2Int a, int b)
            => new Vector2Int(a.x - b, a.y - b);

        public static Vector2Int operator *(Vector2Int a, Vector2Int b)
            => new Vector2Int(a.x * b.x, a.y * b.y);
        public static Vector2Int operator *(Vector2Int a, int b)
            => new Vector2Int(a.x * b, a.y * b);

        public static Vector2Int operator /(Vector2Int a, Vector2Int b)
        {
            if (b.x == 0 || b.y == 0)
                throw new DivideByZeroException();

            return new Vector2Int(a.x / b.x, a.y / b.y);
        }
        public static Vector2Int operator /(Vector2Int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException();

            return new Vector2Int(a.x / b, a.y / b);
        }

        public static bool operator ==(Vector2Int a, Vector2Int b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Vector2Int a, Vector2Int b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public static bool operator >(Vector2Int a, Vector2Int b)
        {
            if (a.x > b.x || a.y > b.y)
                return true;

            return false;
        }

        public static bool operator >=(Vector2Int a, Vector2Int b)
        {
            if (a.x >= b.x || a.y >= b.y)
                return true;

            return false;
        }

        public static bool operator <(Vector2Int a, Vector2Int b)
        {
            if (a.x < b.x || a.y < b.y)
                return true;

            return false;
        }

        public static bool operator <=(Vector2Int a, Vector2Int b)
        {
            if (a.x <= b.x || a.y <= b.y)
                return true;

            return false;
        }

        #endregion
    }
}
