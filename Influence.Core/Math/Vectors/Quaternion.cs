using System;

namespace Influence
{
    public struct Quaternion: IEquatable<Quaternion>
    {
        const float slerpEpsilon = 1e-6f;

        /// <summary>X vector.</summary>
        public float x;
        /// <summary>Y vector.</summary>
        public float y;
        /// <summary>Z vector.</summary>
        public float z;
        /// <summary>W vector.</summary>
        public float w;

        public Quaternion() { }
        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Quaternion(Vector4 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
            w = vector.w;
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
            if (!(other is Quaternion)) return false;

            return Equals((Quaternion)other);
        }

        public bool Equals(Quaternion other)
        {
            return x == other.x && y == other.y && z == other.z && w == other.w;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() * 4) ^ (z.GetHashCode() / 4) ^ (w.GetHashCode() / 2);
        }

#endregion

        #region Quick Returns

        public Vector3 eulerAngles
        {
            get => ToEulerAngles(this);
            set => FromEuler(value);
        }

        public static Quaternion Zero => new Quaternion(0, 0, 0, 0);
        public static Quaternion Identity => new Quaternion(0, 0, 0, 1);

        #endregion

        #region Functions

        /// <summary>Dot Product of two quaternions.</summary>
        public static float Dot(Quaternion lhs, Quaternion rhs) => lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;

        public float magnitude => Mathq.Sqrt(x * x + y * y + z * z + w * w);
        public float sqrMagnitude => (x * x + y * y + z * z + w * w);

        public Quaternion normalized => Normalize(this);
        public void Normalize() => this = Normalize(this);

        public static Quaternion Normalize(Quaternion quaternion)
        {
            float magnitude = Mathq.Sqrt(Dot(quaternion, quaternion));

            if (magnitude < Mathq.Epsilon)
                return Identity;

            return new Quaternion(quaternion.x / magnitude, quaternion.y / magnitude, quaternion.z / magnitude, quaternion.w / magnitude);
        }

        public Quaternion Conjugate() => new Quaternion(-x, -y, -z, w);

        public static Quaternion Inverse(Quaternion quaternion)
        {
            float srqMag = quaternion.sqrMagnitude;

            if (srqMag <= 0)
                throw new InvalidOperationException("Cannot compute the inverse of a quaternion with zero magnitude.");

            return quaternion.Conjugate() / srqMag;
        }

        public Quaternion Inverse()
        {
            float srqMag = sqrMagnitude;

            if (srqMag <= 0)
                throw new InvalidOperationException("Cannot compute the inverse of a quaternion with zero magnitude.");

            return Conjugate() / srqMag;
        }

        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float t)
        {
            float t1 = 1f - t;

            Quaternion result = new Quaternion();

            float dot = Dot(quaternion1, quaternion2);

            if (dot >= 0f)
            {
                result.x = t1 * quaternion1.x + t * quaternion2.x;
                result.y = t1 * quaternion1.y + t * quaternion2.y;
                result.z = t1 * quaternion1.z + t * quaternion2.z;
                result.w = t1 * quaternion1.w + t * quaternion2.w;
            }
            else
            {
                result.x = t1 * quaternion1.x - t * quaternion2.x;
                result.y = t1 * quaternion1.y - t * quaternion2.y;
                result.z = t1 * quaternion1.z - t * quaternion2.z;
                result.w = t1 * quaternion1.w - t * quaternion2.w;
            }

            // Normalize it.
            float ls = Dot(result, result);
            float invNorm = 1f / Mathq.Sqrt(ls);

            result.x *= invNorm;
            result.y *= invNorm;
            result.z *= invNorm;
            result.w *= invNorm;

            return result;
        }

        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float t)
        {
            float cosOmega = Dot(quaternion1, quaternion2);
            bool flip = false;

            if (cosOmega < 0.0f)
            {
                flip = true;
                cosOmega = -cosOmega;
            }

            float s1, s2;

            if (cosOmega > (1.0f - slerpEpsilon))
            {
                s1 = 1.0f - t;
                s2 = (flip) ? -t : t;
            }
            else
            {
                float omega = Trigq.Acos(cosOmega);
                float invSinOmega = 1f / Trigq.Sin(omega);

                s1 = Trigq.Sin((1f - t) * omega) * invSinOmega;
                s2 = (flip) ? -Trigq.Sin(t * omega) * invSinOmega : Trigq.Sin(t * omega) * invSinOmega;
            }

            Quaternion result = new Quaternion();

            result.x = s1 * quaternion1.x + s2 * quaternion2.x;
            result.y = s1 * quaternion1.y + s2 * quaternion2.y;
            result.z = s1 * quaternion1.z + s2 * quaternion2.z;
            result.w = s1 * quaternion1.w + s2 * quaternion2.w;

            return result;
        }

        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angleInDegrees)
        {
            float angleInRadians = Mathq.Pi * angleInDegrees / 180f;
            float halfAngle = angleInRadians * 0.5f;
            float sin = Trigq.Sin(halfAngle);
            float cos = Trigq.Cos(halfAngle);

            Vector3 normalizedAxis = axis.normalized;

            Quaternion result = new Quaternion();
            result.x = normalizedAxis.x * sin;
            result.y = normalizedAxis.y * sin;
            result.z = normalizedAxis.z * sin;
            result.w = cos;

            return result;
        }

        public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            float sr, cr, sp, cp, sy, cy;

            float halfRoll = roll * 0.5f;
            sr = Trigq.Sin(halfRoll);
            cr = Trigq.Cos(halfRoll);

            float halfPitch = pitch * 0.5f;
            sp = Trigq.Sin(halfPitch);
            cp = Trigq.Cos(halfPitch);

            float halfYaw = yaw * 0.5f;
            sy = Trigq.Sin(halfYaw);
            cy = Trigq.Cos(halfYaw);

            Quaternion result = new Quaternion();

            result.x = cy * sp * cr + sy * cp * sr;
            result.y = sy * cp * cr - cy * sp * sr;
            result.z = cy * cp * sr - sy * sp * cr;
            result.w = cy * cp * cr + sy * sp * sr;

            return result;
        }

        public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
        {
            float trace = matrix.Matrix[0, 0] + matrix.Matrix[1, 1] + matrix.Matrix[2, 2];

            Quaternion quaternion = new Quaternion();

            if (trace > 0.0f)
            {
                float sqrt = Mathq.Sqrt(trace + 1.0f);
                quaternion.w = sqrt * 0.5f;
                sqrt = 0.5f / sqrt;
                quaternion.x = (matrix.Matrix[1, 2] - matrix.Matrix[2, 1]) * sqrt;
                quaternion.y = (matrix.Matrix[2, 0] - matrix.Matrix[0, 2]) * sqrt;
                quaternion.z = (matrix.Matrix[0, 1] - matrix.Matrix[1, 0]) * sqrt;
            }
            else
            {
                if (matrix.Matrix[0, 0] >= matrix.Matrix[1, 1] && matrix.Matrix[0, 0] >= matrix.Matrix[2, 2])
                {
                    float sqrt = Mathq.Sqrt(1.0f + matrix.Matrix[0, 0] - matrix.Matrix[1, 1] - matrix.Matrix[2, 2]);
                    float invS = 0.5f / sqrt;
                    quaternion.x = 0.5f * sqrt;
                    quaternion.y = (matrix.Matrix[0, 1] + matrix.Matrix[1, 0]) * invS;
                    quaternion.z = (matrix.Matrix[0, 2] + matrix.Matrix[2, 0]) * invS;
                    quaternion.w = (matrix.Matrix[1, 2] - matrix.Matrix[2, 1]) * invS;
                }
                else if (matrix.Matrix[1, 1] > matrix.Matrix[2, 2])
                {
                    float sqrt = Mathq.Sqrt(1.0f + matrix.Matrix[1, 1] - matrix.Matrix[0, 0] - matrix.Matrix[2, 2]);
                    float invS = 0.5f / sqrt;
                    quaternion.x = (matrix.Matrix[1, 0] + matrix.Matrix[0, 1]) * invS;
                    quaternion.y = 0.5f * sqrt;
                    quaternion.z = (matrix.Matrix[2, 1] + matrix.Matrix[1, 2]) * invS;
                    quaternion.w = (matrix.Matrix[2, 0] - matrix.Matrix[0, 2]) * invS;
                }
                else
                {
                    float sqrt = Mathq.Sqrt(1.0f + matrix.Matrix[2, 2] - matrix.Matrix[0, 0] - matrix.Matrix[1, 1]);
                    float invS = 0.5f / sqrt;
                    quaternion.x = (matrix.Matrix[2, 0] + matrix.Matrix[0, 2]) * invS;
                    quaternion.y = (matrix.Matrix[2, 1] + matrix.Matrix[1, 2]) * invS;
                    quaternion.z = 0.5f * sqrt;
                    quaternion.w = (matrix.Matrix[0, 1] - matrix.Matrix[1, 0]) * invS;
                }
            }

            return quaternion;
        }

        public static Quaternion Euler(Vector3 euler)
        {
            float pitchRad = euler.x * Mathq.Pi / 180f;
            float yawRad = euler.y * Mathq.Pi / 180f;
            float rollRad = euler.z * Mathq.Pi / 180f;

            float cy = Trigq.Cos(yawRad * 0.5f);
            float sy = Trigq.Sin(yawRad * 0.5f);
            float cp = Trigq.Cos(pitchRad * 0.5f);
            float sp = Trigq.Sin(pitchRad * 0.5f);
            float cr = Trigq.Cos(rollRad * 0.5f);
            float sr = Trigq.Sin(rollRad * 0.5f);

            Quaternion q = new Quaternion();
            q.x = cy * cp * sr - sy * sp * cr;
            q.y = sy * cp * sr + cy * sp * cr;
            q.z = sy * cp * cr - cy * sp * sr;
            q.w = cy * cp * cr + sy * sp * sr;

            return q;
        }

        //public static Quaternion Euler(Vector3 euler) => Quaternion.CreateFromAxisAngle(euler.normalized, euler.magnitude * Mathq.Rad2Deg);
        public static Quaternion Euler(float x, float y, float z) => Euler(new Vector3(x, y, z));

        public static Vector3 ToEulerAngles(Quaternion quaternion)
        {
            float sinr_cosp = 2f * (quaternion.x * quaternion.w + quaternion.y * quaternion.z);
            float cosr_cosp = 1f - 2f * (quaternion.x * quaternion.x + quaternion.y * quaternion.y);
            float roll = Trigq.Atan2(sinr_cosp, cosr_cosp) * Mathq.Rad2Deg;

            float sinp = 2f * (quaternion.y * quaternion.w - quaternion.x * quaternion.z);
            float pitch;
            if (Mathq.Abs(sinp) >= 1)
            {
                pitch = Mathq.Sign(sinp) * 90f; // Use 90 degrees if out of range
            }
            else
            {
                pitch = Trigq.Asin(sinp) * Mathq.Rad2Deg;
            }

            float siny_cosp = 2f * (quaternion.z * quaternion.w + quaternion.x * quaternion.y);
            float cosy_cosp = 1f - 2f * (quaternion.y * quaternion.y + quaternion.z * quaternion.z);
            float yaw = Trigq.Atan2(siny_cosp, cosy_cosp) * Mathq.Rad2Deg;

            return new Vector3(pitch, yaw, roll);
        }
        public Vector3 ToEuler() => ToEulerAngles(this);

        public static void SetEulerRotation(Quaternion quaternion, Vector3 euler) => quaternion = Euler(euler);
        public void SetEuler(Vector3 euler) => this = Euler(euler);

        public Quaternion FromEuler(Vector3 euler) => Euler(euler);

        public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
        {
            Vector3 axis = Vector3.Cross(fromDirection, toDirection);
            float angle = Vector3.Angle(fromDirection, toDirection);
            return AngleAxis(angle, axis.normalized);
        }

        public static Quaternion AngleAxis(float angleInDegrees, Vector3 axis)
        {
            float radianAngle = angleInDegrees * Mathq.Pi / 180f;
            float halfAngle = radianAngle * 0.5f;

            float sinHalfAngle = Trigq.Sin(halfAngle);
            float cosHalfAngle = Trigq.Cos(halfAngle);

            Quaternion result = new Quaternion(axis.x * sinHalfAngle, axis.y * sinHalfAngle, axis.z * sinHalfAngle, cosHalfAngle);

            return result;
        }

        public static Quaternion FromAxisAngle(Vector3 axis, float angle)
        {
            // Normalize the axis to ensure it has a length of 1
            axis.Normalize();

            // Calculate half-angle cosine and sine values
            float halfCos = Trigq.Cos(angle / 2);
            float halfSin = Trigq.Sin(angle / 2);

            // Construct the quaternion from the axis and angle
            return new Quaternion(axis.x * halfSin, axis.y * halfSin, axis.z * halfSin, halfCos);
        }

        public Matrix4x4 ToRotationMatrix()
        {
            Quaternion q = this;

            float xx = q.x * q.x;
            float yy = q.y * q.y;
            float zz = q.z * q.z;
            float xy = q.x * q.y;
            float xz = q.x * q.z;
            float yz = q.y * q.z;
            float wx = q.x * q.w;
            float wy = q.y * q.w;
            float wz = q.z * q.w;

            Matrix4x4 matrix = Matrix4x4.Identity;

            matrix.Matrix[0, 0] = 1 - 2 * (yy + zz);
            matrix.Matrix[1, 1] = 1 - 2 * (xx + zz);
            matrix.Matrix[2, 2] = 1 - 2 * (xx + yy);
            matrix.Matrix[0, 1] = 2 * (xy - wz);
            matrix.Matrix[0, 2] = 2 * (xz + wy);
            matrix.Matrix[1, 0] = 2 * (xy + wz);
            matrix.Matrix[1, 2] = 2 * (yz - wx);
            matrix.Matrix[2, 0] = 2 * (xz - wy);
            matrix.Matrix[2, 1] = 2 * (yz + wx);

            return matrix;
        }

        #endregion

        #region Operators

        public static Quaternion operator + (Quaternion a, Quaternion b)
        {
            return new Quaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Quaternion operator - (Quaternion a, Quaternion b)
        {
            return new Quaternion(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Quaternion operator * (Quaternion a, float b)
        {
            return new Quaternion(a.x * b, a.y * b, a.z * b, a.w * b);
        }

        public static Quaternion operator * (Quaternion lhs, Quaternion rhs)
        {
            return new Quaternion(
                lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
                lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
        }

        public static Vector3 operator * (Quaternion rotation, Vector3 point)
        {
            float x = rotation.x * 2F;
            float y = rotation.y * 2F;
            float z = rotation.z * 2F;
            float xx = rotation.x * x;
            float yy = rotation.y * y;
            float zz = rotation.z * z;
            float xy = rotation.x * y;
            float xz = rotation.x * z;
            float yz = rotation.y * z;
            float wx = rotation.w * x;
            float wy = rotation.w * y;
            float wz = rotation.w * z;

            Vector3 result = new Vector3();
            result.x = (1F - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
            result.y = (xy + wz) * point.x + (1F - (xx + zz)) * point.y + (yz - wx) * point.z;
            result.z = (xz - wy) * point.x + (yz + wx) * point.y + (1F - (xx + yy)) * point.z;
            return result;
        }

        public static Quaternion operator / (Quaternion a, float b)
        {
            return new Quaternion(a.x / b, a.y / b, a.z / b, a.w / b);
        }

        public static bool operator == (Quaternion a, Quaternion b)
        {
            return (a.x == b.x) && (a.y == b.y) && (a.z == b.z) && (a.w == b.w);
        }

        public static bool operator != (Quaternion a, Quaternion b)
        {
            return (a.x != b.x) || (a.y != b.y) || (a.z != b.z) || (a.w != b.w);
        }

        #endregion
    }
}
