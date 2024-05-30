using System;

namespace Influence
{
    public struct Color : IEquatable<Color>
    {
        /// <summary>Red color.</summary>
        public float r;
        /// <summary>Group color.</summary>
        public float g;
        /// <summary>Blue color.</summary>
        public float b;

        /// <summary>Alpha color.</summary>
        public float a;

        public Color() {}

        public Color(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;

            this.a = 1f;
        }

        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;

            this.a = a;
        }

        public float this[int index]
        {
            get
            {
                switch(index)
                {
                    case 0: return r;
                    case 1: return g;
                    case 2: return b;
                    case 3: return a;
                    default:
                        throw new IndexOutOfRangeException("Invalid Color index.");
                }
            }

            set
            {
                switch(index)
                {
                    case 0: r = value; break;
                    case 1: g = value; break;
                    case 2: b = value; break;
                    case 3: a = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Color index.");
                }
            }
        }

        #region Common Overrides (ToString, Equals, GetHashCode)

        public override string ToString()
        {
            return $"({r}, {g}, {b}, {a})";
        }

        public override bool Equals(object? other)
        {
            if (!(other is Color)) return false;

            return Equals((Color)other);
        }

        public bool Equals(Color other)
        {
            return r == other.r && g == other.g && b == other.b && a == other.a;
        }

        public override int GetHashCode()
        {
            return r.GetHashCode() ^ (g.GetHashCode() * 4) ^ (b.GetHashCode() / 4) ^ (a.GetHashCode() / 2);
        }

        #endregion

        #region Quick Returns

        public static Color Red => new Color(1f, 0f, 0f);
        public static Color Green => new Color(0f, 1f, 0f);
        public static Color Blue => new Color(0f, 0f, 1f);

        public static Color Black => new Color(0f, 0f, 0f);
        public static Color White => new Color(1f, 1f, 1f);

        public static Color Yellow => new Color(1f, 1f, 0f);
        public static Color Cyan => new Color(0f, 1f, 1f);
        public static Color Magenta => new Color(1f, 0f, 1f);

        public static Color Clear => new Color(0f, 0f, 0f, 0f);

        #endregion

        #region Functions

        public static Color Lerp(Color a, Color b, float t)
        {
            t = Mathq.Clamp01(t);
            return new Color(
                a.r + (b.r - a.r) * t,
                a.g + (b.g - a.g) * t,
                a.b + (b.b - a.b) * t,
                a.a + (b.a - a.a) * t
                );
        }

        public static Color LerpUnclamped(Color a, Color b, float t)
        {
            return new Color(
                a.r + (b.r - a.r) * t,
                a.g + (b.g - a.g) * t,
                a.b + (b.b - a.b) * t,
                a.a + (b.a - a.a) * t
                );
        }

        #endregion

        #region Operators

        public static Color operator + (Color a, Color b)
        {
            return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        }

        public static Color operator + (Color a, float b)
        {
            return new Color(a.r + b, a.g + b, a.b + b, a.a + b);
        }

        public static Color operator - (Color a, Color b)
        {
            return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
        }

        public static Color operator - (Color a, float b)
        {
            return new Color(a.r - b, a.g - b, a.b - b, a.a - b);
        }

        public static Color operator * (Color a, Color b)
        {
            return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        }

        public static Color operator * (Color a, float b)
        {
            return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
        }

        public static Color operator / (Color a, Color b)
        {
            return new Color(a.r / b.r, a.g / b.g, a.b / b.g, a.a / b.a);
        }

        public static Color operator / (Color a, float b)
        {
            return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
        }

        public static bool operator == (Color a, Color b)
        {
            return (a.r == b.r) && (a.g == b.g) && (a.b == b.b) && (a.a == b.a);
        }

        public static bool operator != (Color a, Color b)
        {
            return (a.r != b.r) || (a.g != b.g) || (a.b != b.b) || (a.a != b.a);
        }

        #endregion
    }
}
