using System;

namespace Influence
{
    public struct Vector2: IEquatable<Vector2>
    {
        /// <summary>X vector.</summary>
        public float x;
        /// <summary>Y vector.</summary>
        public float y;

        public Vector2() {}
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2(Vector2Int vector)
        {
            this.x = vector.x;
            this.y = vector.y;
        }
        public Vector2(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
        }
        // TODO Add Vector3Int
        public Vector2(Vector4 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
        }

        public float this[int index]
        {
            get
            {
                switch(index)
                {
                    case 0: return x;
                    case 1: return y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index.");
                }
            }

            set
            {
                switch(index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index.");
                }
            }
        }

        #region Common Overrides (ToString, GetHashCode, Equals)

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public override bool Equals(object? other)
        {
            if (!(other is Vector2)) return false;

            return Equals((Vector2)other);
        }

        public bool Equals(Vector2 other)
        {
            return x == other.x && y == other.y;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() * 4);
        }

        #endregion

        #region Quick Returns

        /// <summary>
        /// Returns Vector2 : (0,0)
        /// </summary>
        public static Vector2 Zero => new Vector2(0, 0);

        /// <summary>
        /// Returns Vector2 : (1,1)
        /// </summary>
        public static Vector2 One => new Vector2(1, 1);

        /// <summary>
        /// Returns Vector2 : (0,1)
        /// </summary>
        public static Vector2 Up => new Vector2(0, 1);

        /// <summary>
        /// Returns Vector2 : (0,-1)
        /// </summary>
        public static Vector2 Down => new Vector2(0, -1);

        /// <summary>
        /// Returns Vector2 : (-1,0)
        /// </summary>
        public static Vector2 Left => new Vector2(-1, 0);

        /// <summary>
        /// Returns Vector2 : (1,0)
        /// </summary>
        public static Vector2 Right => new Vector2(1, 0);

        #endregion

        #region Functions

        /// <summary>Dot Product of two vectors.</summary>
        public static float Dot(Vector2 lhs, Vector2 rhs) => lhs.x * rhs.x + lhs.y * rhs.y;

        /// <summary>Returns the angle in degrees between two vectors.</summary>
        public static float Angle(Vector2 from, Vector2 to)
        {
            float denominator = (float)Mathq.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
            if (denominator < 0f)
                denominator = 0f;

            float dot = Mathq.Clamp(Dot(from, to) / denominator, -1f, 1f);
            return (float)Trigq.Acos(dot) * Mathq.Rad2Deg;
        }

        /// <summary>Returns the signed angle in degrees between two vectors.</summary>
        public static float SignedAngle(Vector2 from, Vector2 to)
        {
            float unsignedAngle = Angle(from, to);
            float sign = Math.Sign(from.x * to.y - from.y * to.x);
            return unsignedAngle * sign;
        }

        public static Vector2 Reflect(Vector2 inDirection , Vector2 inNormal)
        {
            float factor = -2f * Dot(inNormal, inDirection);
            return new Vector2(factor * inNormal.x + inDirection.x, factor * inNormal.y + inDirection.y);
        }

        public static Vector2 Perpendicular(Vector2 inDirection)
        {
            return new Vector2(-inDirection.y, inDirection.x);
        }

        public float magnitude => (float)Mathq.Sqrt(x * x + y * y);
        public float sqrMagnitude => (x * x + y * y);
        public Vector2 normalized
        {
            get
            {
                Vector2 v = new Vector2(x, y);
                v.Normalize();
                return v;
            }
        }

        public void Normalize()
        {
            this = this / magnitude;
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            float xDif = (a.x - b.x);
            float yDif = (a.y - b.y);
            return (float)Mathq.Sqrt(xDif * xDif + yDif * yDif);
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            t = Mathq.Clamp01(t);
            return new Vector2(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t
                );
        }

        public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t
                );
        }

        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            float xDir = target.x - current.x;
            float yDir = target.y - current.y;

            float sqDistance = (xDir * xDir + yDir * yDir);

            if (sqDistance == 0 || (maxDistanceDelta >= 0 && sqDistance <= maxDistanceDelta * maxDistanceDelta))
                return target;

            float distance = (float)Mathq.Sqrt(sqDistance);

            return new Vector2(
                current.x + xDir / distance * maxDistanceDelta,
                current.y + yDir / distance * maxDistanceDelta
                );
        }

        #endregion

        #region Operators

        public static Vector2 operator + (Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
        public static Vector2 operator + (Vector2 a, float b) => new Vector2(a.x + b, a.y + b);

        public static Vector2 operator - (Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
        public static Vector2 operator - (Vector2 a, float b) => new Vector2(a.x - b, a.y - b);

        public static Vector2 operator -(Vector2 a) => new Vector2(-a.x, -a.y);

        public static Vector2 operator *(Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);
        public static Vector2 operator *(Vector2 a, float b) => new Vector2(a.x * b, a.y * b);

        public static Vector2 operator / (Vector2 a, Vector2 b) => new Vector2(a.x / b.x, a.y / b.y);
        public static Vector2 operator / (Vector2 a, float b) => new Vector2(a.x / b, a.y / b);

        public static bool operator == (Vector2 a, Vector2 b) => a.x == b.x && a.y == b.y;

        public static bool operator != (Vector2 a, Vector2 b) => a.x != b.x || a.y != b.y;

        public static bool operator > (Vector2 a, Vector2 b)
        {
            if (a.x > b.x || a.y > b.y)
                return true;

            return false;
        }

        public static bool operator >= (Vector2 a, Vector2 b)
        {
            if (a.x >= b.x || a.y >= b.y)
                return true;

            return false;
        }

        public static bool operator < (Vector2 a, Vector2 b)
        {
            if (a.x < b.x || a.y < b.y)
                return true;

            return false;
        }

        public static bool operator <= (Vector2 a, Vector2 b)
        {
            if (a.x <= b.x || a.y <= b.y)
                return true;

            return false;
        }

        #endregion

    }
}
