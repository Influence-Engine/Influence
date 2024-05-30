using System;

namespace Influence
{
    public struct Vector4 : IEquatable<Vector4>
    {
        /// <summary>X vector.</summary>
        public float x;
        /// <summary>Y vector.</summary>
        public float y;
        /// <summary>Z vector.</summary>
        public float z;
        /// <summary>W vector.</summary>
        public float w;

        public Vector4() {}
        public Vector4(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector4(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Vector4(Vector2 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
        }
        public Vector4(Vector2Int vector)
        {
            x = vector.x;
            y = vector.y;
        }
        public Vector4(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }
        // TODO Add Vector3Int
        public Vector4(Color color)
        {
            x = color.r;
            y = color.g;
            z = color.b;
            w = color.a;
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
                    case 3: return w;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4 index.");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4 index.");
                }
            }
        }

        #region Common Overrides (ToString, GetHashCode, Equals)

        public override string ToString()
        {
            return $"({x}, {y}, {z}, {w})";
        }

        public override bool Equals(object? other)
        {
            if (!(other is Vector4)) return false;

            return Equals((Vector4)other);
        }

        public bool Equals(Vector4 other)
        {
            return x == other.x && y == other.y && z == other.z && w == other.w;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() * 4) ^ (z.GetHashCode() / 4) ^ (w.GetHashCode() / 2);
        }

        #endregion

        #region Quick Returns

        public static Vector4 Zero => new Vector4(0, 0, 0, 0);
        public static Vector4 One => new Vector4(1, 1, 1, 1);

        #endregion

        #region Functions

        /// <summary>Dot Product of two vectors.</summary>
        public static float Dot(Vector4 lhs, Vector4 rhs) => lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;

        /// <summary>Projects a vector into another vector.</summary>
        public static Vector4 Project(Vector4 a, Vector4 b) => b * (Dot(a, b) / Dot(b, b));

        public float magnitude => Mathq.Sqrt(x * x + y * y + z * z + w * w);
        public float sqrMagnitude => (x * x + y * y + z * z + w * w);
        public Vector4 normalized
        {
            get
            {
                Vector4 v = new Vector4(x,y,z,w);
                v.Normalize();
                return v;
            }
        }

        public void Normalize()
        {
            this = this / magnitude;
        }

        public static float Distance(Vector4 a, Vector4 b)
        {
            Vector4 dif = new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
            return Mathq.Sqrt(dif.x * dif.x + dif.y * dif.y + dif.z * dif.z + dif.w * dif.w);
        }

        public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
        {
            t = Mathq.Clamp01(t);
            return new Vector4(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t,
                a.w + (b.w - a.z) * t
                );
        }

        public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
        {
            return new Vector4(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t,
                a.w + (b.w - a.z) * t
                );
        }

        public static Vector4 MoveTowards(Vector4 current, Vector4 target, float maxDistanceDelta)
        {
            float xDir = target.x - current.x;
            float yDir = target.y - current.y;
            float zDir = target.z - current.z;
            float wDir = target.w - current.w;

            float sqDistance = (xDir * xDir + yDir * yDir + zDir * zDir + wDir * wDir);

            if (sqDistance == 0 || (maxDistanceDelta >= 0 && sqDistance <= maxDistanceDelta * maxDistanceDelta))
                return target;

            float distance = (float)Mathq.Sqrt(sqDistance);

            return new Vector4(
                current.x + xDir / distance * maxDistanceDelta,
                current.y + yDir / distance * maxDistanceDelta,
                current.z + zDir / distance * maxDistanceDelta,
                current.w + wDir / distance * maxDistanceDelta
                );
        }

        #endregion

        #region Operators

        public static Vector4 operator + (Vector4 a, Vector4 b)
        {
            return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }
        public static Vector4 operator + (Vector4 a, float b)
        {
            return new Vector4(a.x + b, a.y + b, a.z + b, a.w + b);
        }

        public static Vector4 operator - (Vector4 a, Vector4 b)
        {
            return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }
        public static Vector4 operator - (Vector4 a, float b)
        {
            return new Vector4(a.x - b, a.y - b, a.z - b, a.w - b);
        }

        public static Vector4 operator -(Vector4 a) => new Vector4(-a.x, -a.y, -a.z, -a.w);

        public static Vector4 operator * (Vector4 a, Vector4 b)
        {
            return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }
        public static Vector4 operator * (Vector4 a, float b)
        {
            return new Vector4(a.x * b, a.y * b, a.z * b, a.w * b);
        }

        public static Vector4 operator / (Vector4 a, Vector4 b)
        {
            return new Vector4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
        }
        public static Vector4 operator / (Vector4 a, float b)
        {
            return new Vector4(a.x / b, a.y / b, a.z / b, a.w / b);
        }

        public static bool operator ==(Vector4 a, Vector4 b)
        {
            return (a.x == b.x) && (a.y == b.y) && (a.z == b.z) && (a.w == b.w);
        }

        public static bool operator !=(Vector4 a, Vector4 b)
        {
            return (a.x != b.x) || (a.y != b.y) || (a.z != b.z) || (a.w != b.w);
        }

        public static bool operator >(Vector4 a, Vector4 b)
        {
            if (a.x > b.x || a.y > b.y || a.z > b.z || a.w > b.w)
                return true;

            return false;
        }

        public static bool operator >=(Vector4 a, Vector4 b)
        {
            if (a.x >= b.x || a.y >= b.y || a.z >= b.z || a.w >= b.w)
                return true;

            return false;
        }

        public static bool operator <(Vector4 a, Vector4 b)
        {
            if (a.x < b.x || a.y < b.y || a.z < b.z || a.w < b.w)
                return true;

            return false;
        }

        public static bool operator <=(Vector4 a, Vector4 b)
        {
            if (a.x <= b.x || a.y <= b.y || a.z <= b.z || a.w <= b.w)
                return true;

            return false;
        }

        #endregion
    }
}
