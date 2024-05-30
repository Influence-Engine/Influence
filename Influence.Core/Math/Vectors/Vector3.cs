using System;

namespace Influence
{
    public struct Vector3: IEquatable<Vector3>
    {
        /// <summary>X vector.</summary>
        public float x;

        /// <summary>Y vector.</summary>
        public float y;

        /// <summary>Z vector.</summary>
        public float z;

        public Vector3() {}
        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3(Vector2 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
        }
        public Vector3(Vector2Int vector)
        {
            this.x = vector.x;
            this.y = vector.y;
        }
        // TODO Add Vector3Int
        public Vector3(Vector4 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index.");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index.");
                }
            }
        }

        #region Common Overrides (ToString, GetHashCode, Equals)

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        public override bool Equals(object? other)
        {
            if (!(other is Vector3)) return false;

            return Equals((Vector3)other);
        }

        public bool Equals(Vector3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() * 4) ^ (z.GetHashCode() / 4);
        }

        #endregion

        #region Quick Returns

        /// <summary>
        /// Returns Vector3 : (0,0,0)
        /// </summary>
        public static Vector3 Zero => new Vector3(0, 0, 0);

        /// <summary>
        /// Returns Vector3 : (1,1,1)
        /// </summary>
        public static Vector3 One => new Vector3(1, 1, 1);

        /// <summary>
        /// Returns Vector3 : (0,0,1)
        /// </summary>
        public static Vector3 Forward => new Vector3(0, 0, 1);

        /// <summary>
        /// Returns Vector3 : (0,0,-1)
        /// </summary>
        public static Vector3 Back => new Vector3(0, 0, -1);

        /// <summary>
        /// Returns Vector3 : (0,1,0)
        /// </summary>
        public static Vector3 Up => new Vector3(0, 1, 0);

        /// <summary>
        /// Returns Vector3 : (0,-1,0)
        /// </summary>
        public static Vector3 Down => new Vector3(0, -1, 0);

        /// <summary>
        /// Returns Vector3 : (-1,0,0)
        /// </summary>
        public static Vector3 Left => new Vector3(-1, 0, 0);

        /// <summary>
        /// Returns Vector3 : (1,0,0)
        /// </summary>
        public static Vector3 Right => new Vector3(1, 0, 0);

        #endregion

        #region Functions

        /// <summary>Dot Product of two vectors.</summary>
        public static float Dot(Vector3 lhs, Vector3 rhs) => lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;

        /// <summary>Returns the angle in degrees between two vectors.</summary>
        public static float Angle(Vector3 from, Vector3 to)
        {
            float denominator = Mathq.Sqrt(from.sqrMagnitude * to.sqrMagnitude);

            float dot = Mathq.Clamp(Dot(from, to) / denominator, -1f, 1f);
            return Trigq.Acos(dot) * Mathq.Rad2Deg;
        }

        /// <summary>Returns the signed angle in degrees between two vectors.</summary>
        public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
        {
            float unsignedAngle = Angle(from, to);

            float crossX = from.y * to.z - from.z * to.y;
            float crossY = from.z * to.x - from.x * to.z;
            float crossZ = from.x * to.y - from.y * to.x;

            float sign = Math.Sign(axis.x * crossX + axis.y * crossY + axis.z * crossZ) ;
            return unsignedAngle * sign;
        }

        public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
        {
            float factor = -2f * Dot(inNormal, inDirection);
            return new Vector3(
                factor * inNormal.x + inDirection.x,
                factor * inNormal.y + inDirection.y,
                factor * inNormal.z + inDirection.z);
        }

        public static Vector3 Cross(Vector3 lhs,  Vector3 rhs)
        {
            return new Vector3(
                lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.x * rhs.y - lhs.y * rhs.x);
        }

        public float magnitude => Mathq.Sqrt(x * x + y * y + z * z);
        public float sqrMagnitude => (x * x + y * y + z * z);
        public Vector3 normalized
        {
            get
            {
                Vector3 v = new Vector3(x, y, z);
                v.Normalize();
                return v;
            }
        }

        public void Normalize()
        {
            this = this / magnitude;
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            Vector3 dif = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
            return Mathq.Sqrt(dif.x * dif.x + dif.y * dif.y + dif.z * dif.z);
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            t = Mathq.Clamp01(t);
            return new Vector3(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t
                );
        }

        public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t
                );
        }

        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            float xDir = target.x - current.x;
            float yDir = target.y - current.y;
            float zDir = target.z - current.z;

            float sqDistance = (xDir * xDir + yDir * yDir + zDir * zDir);

            if (sqDistance == 0 || (maxDistanceDelta >= 0 && sqDistance <= maxDistanceDelta * maxDistanceDelta))
                return target;

            float distance = (float)Mathq.Sqrt(sqDistance);

            return new Vector3(
                current.x + xDir / distance * maxDistanceDelta,
                current.y + yDir / distance * maxDistanceDelta,
                current.z + zDir / distance * maxDistanceDelta
                );
        }

        #endregion

        #region Operators

        public static Vector3 operator + (Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector3 operator + (Vector3 a, float b) => new Vector3(a.x + b, a.y + b, a.z + b);

        public static Vector3 operator - (Vector3 a, Vector3 b)=> new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vector3 operator - (Vector3 a, float b) => new Vector3(a.x - b, a.y - b, a.z - b);

        public static Vector3 operator -(Vector3 a) => new Vector3(-a.x, -a.y, -a.z);

        public static Vector3 operator * (Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        public static Vector3 operator *(Vector3 a, float b) => new Vector3(a.x * b, a.y * b, a.z * b);

        public static Vector3 operator / (Vector3 a, Vector3 b) => new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        public static Vector3 operator / (Vector3 a, float b) => new Vector3(a.x / b, a.y / b, a.z / b);

        public static bool operator == (Vector3 a, Vector3 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator != (Vector3 a, Vector3 b)
        {
            return a.x != b.x || a.y != b.y || a.z != b.z;
        }

        public static bool operator > (Vector3 a, Vector3 b)
        {
            if (a.x > b.x || a.y > b.y || a.z > b.z)
                return true;

            return false;
        }

        public static bool operator >= (Vector3 a, Vector3 b)
        {
            if (a.x >= b.x || a.y >= b.y || a.z >= b.z)
                return true;

            return false;
        }

        public static bool operator < (Vector3 a, Vector3 b)
        {
            if (a.x < b.x || a.y < b.y || a.z < b.z)
                return true;

            return false;
        }

        public static bool operator <= (Vector3 a, Vector3 b)
        {
            if (a.x <= b.x || a.y <= b.y || a.z <= b.z)
                return true;

            return false;
        }

        #endregion

    }
}
