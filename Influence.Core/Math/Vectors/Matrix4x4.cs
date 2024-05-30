using System;

namespace Influence
{
    public struct Matrix4x4: IEquatable<Matrix4x4>
    {
        // Memory Layout:
        //              row
        //           |  0   1   2   3
        //      ---+--------------
        //       0  | 00 10 20 30         0   1    2   3
        //       1   | 01  11  21 31          4   5    6   7
        //       2  | 02 12 22 32         8   9   10 11
        //       3  | 03 13 23 33         12 13 14 15
        // column


        float[,] data;
        public float[,] Matrix => data;

        public Matrix4x4() => data = new float[4, 4];

        public Matrix4x4(float[] matrixData)
        {
            if (matrixData.Length != 16)
                throw new ArgumentException("Initial data must be a 4x4 matrix.");

            data = new float[4, 4];
            for (int i = 0; i < matrixData.Length; i++)
            {
                this[i] = matrixData[i];
            }
        }

        public Matrix4x4(float[,] matrixData)
        {
            if (matrixData.GetLength(0) != 4 || matrixData.GetLength(1) != 4)
                throw new ArgumentException("Initial data must be a 4x4 matrix.");

            data = matrixData;
        }

        public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            data = new float[4, 4];

            data[0, 0] = column0.x; data[0, 1] = column1.x; data[0, 2] = column2.x; data[0, 3] = column3.x;
            data[1, 0] = column0.y; data[1, 1] = column1.y; data[1, 2] = column2.y; data[1, 3] = column3.y;
            data[2, 0] = column0.z; data[2, 1] = column1.z; data[2, 2] = column2.z; data[2, 3] = column3.z;
            data[3, 0] = column0.w; data[3, 1] = column1.w; data[3, 2] = column2.w; data[3, 3] = column3.w;
        }

        public float this[int row, int column]
        {
            get => data[row, column];
            set => data[row, column] = value;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return data[0,0];
                    case 1: return data[1, 0];
                    case 2: return data[2, 0];
                    case 3: return data[3, 0];
                    case 4: return data[0, 1];
                    case 5: return data[1, 1];
                    case 6: return data[2, 1];
                    case 7: return data[3, 1];
                    case 8: return data[0, 2];
                    case 9: return data[1, 2];
                    case 10: return data[2, 2];
                    case 11: return data[3, 2];
                    case 12: return data[0, 3];
                    case 13: return data[1, 3];
                    case 14: return data[2, 3];
                    case 15: return data[3, 3];
                    default:
                        throw new IndexOutOfRangeException("Invalid Matrix4x4 index.");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: data[0, 0] = value; break;
                    case 1: data[1, 0] = value; break;
                    case 2: data[2, 0] = value; break;
                    case 3: data[3, 0] = value; break;
                    case 4: data[0, 1] = value; break;
                    case 5: data[1, 1] = value; break;
                    case 6: data[2, 1] = value; break;
                    case 7: data[3, 1] = value; break;
                    case 8: data[0, 2] = value; break;
                    case 9: data[1, 2] = value; break;
                    case 10: data[2, 2] = value; break;
                    case 11: data[3, 2] = value; break;
                    case 12: data[0, 3] = value; break;
                    case 13: data[1, 3] = value; break;
                    case 14: data[2, 3] = value; break;
                    case 15: data[3, 3] = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Matrix4x4 index.");
                }
            }
        }

        #region Common Overrides (ToString, GetHashCode, Equals)

        public override string ToString()
        {
            // This looks dangerous
            return string.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15})",
                data[0, 0], data[1, 0], data[2, 0], data[3, 0],
                data[0, 1], data[1, 1], data[2, 1], data[3, 1],
                data[0, 2], data[1, 2], data[2, 2], data[3, 2],
                data[0, 3], data[1, 3], data[2, 3], data[3, 3]);
        }

        public override bool Equals(object? other)
        {
            if (!(other is Matrix4x4)) return false;

            return Equals((Matrix4x4)other);
        }

        public bool Equals(Matrix4x4 other)
        {
            return GetColumn(0).Equals(other.GetColumn(0))
                && GetColumn(1).Equals(other.GetColumn(1))
                && GetColumn(2).Equals(other.GetColumn(2))
                && GetColumn(3).Equals(other.GetColumn(3));
        }

        public override int GetHashCode()
        {
            return GetColumn(0).GetHashCode() ^ (GetColumn(1).GetHashCode() * 4) ^ (GetColumn(2).GetHashCode() / 4) ^ (GetColumn(3).GetHashCode() / 2);
        }

        #endregion

        #region Quick Returns

        public static Matrix4x4 Zero => new Matrix4x4(
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0));

        public static Matrix4x4 Identity => new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1));

        #endregion

        #region Functions

        #region Get

        public Vector4 GetColumn(int index)
        {
            switch(index)
            {
                case 0: return new Vector4(data[0, 0], data[1, 0], data[2, 0], data[3, 0]);
                case 1: return new Vector4(data[0, 1], data[1, 1], data[2, 1], data[3, 1]);
                case 2: return new Vector4(data[0, 2], data[1, 2], data[2, 2], data[3, 2]);
                case 3: return new Vector4(data[0, 3], data[1, 3], data[2, 3], data[3, 3]);
                default: 
                    throw new IndexOutOfRangeException("Invalid column index.");
            }
        }

        public Vector4 GetRow(int index)
        {
            switch (index)
            {
                case 0: return new Vector4(data[0, 0], data[0, 1], data[0, 2], data[0, 3]);
                case 1: return new Vector4(data[1, 0], data[1, 1], data[1, 2], data[1, 3]);
                case 2: return new Vector4(data[2, 0], data[2, 1], data[2, 2], data[2, 3]);
                case 3: return new Vector4(data[3, 0], data[3, 1], data[3, 2], data[3, 3]);
                default:
                    throw new IndexOutOfRangeException("Invalid row index.");
            }
        }

        public Vector3 GetPosition() => new Vector3(data[0, 3], data[1, 3], data[2, 3]);

        #endregion

        #region Set

        public void SetColumn(int index, Vector4 column)
        {
            this[0, index] = column.x;
            this[1, index] = column.y;
            this[2, index] = column.z;
            this[3, index] = column.w;
        }

        public void SetColumn(int index, Vector3 column)
        {
            this[0, index] = column.x;
            this[1, index] = column.y;
            this[2, index] = column.z;
            this[3, index] = 0;
        }

        public void SetRow(int index, Vector4 row)
        {
            this[index, 0] = row.x;
            this[index, 1] = row.y;
            this[index, 2] = row.z;
            this[index, 3] = row.w;
        }

        public void SetRow(int index, Vector3 row)
        {
            this[index, 0] = row.x;
            this[index, 1] = row.y;
            this[index, 2] = row.z;
            this[index, 3] = 0;
        }

        #endregion

        public void Transpose()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    float temp = data[i, j];
                    data[i, j] = data[j, i];
                    data[j, i] = temp;
                }
            }
        }

        /// <summary>Creates a scaling Matrix</summary>
        public static Matrix4x4 Scale(Vector3 vector)
        {
            Matrix4x4 matrix = Matrix4x4.Identity;
            matrix[0, 0] = vector.x;
            matrix[1,1] = vector.y;
            matrix[2, 2] = vector.z;
            return matrix;
        }

        /// <summary>Creates a uniform scaling Matrix</summary>
        public static Matrix4x4 UniformScale(float scale)
        {
            Matrix4x4 matrix = Matrix4x4.Identity;
            matrix[0, 0] = scale;
            matrix[1, 1] = scale;
            matrix[2, 2] = scale;
            return matrix;
        }

        /// <summary>Creates a translate Matrix</summary>
        public static Matrix4x4 Translate(Vector3 vector)
        {
            Matrix4x4 matrix = Matrix4x4.Identity;
            matrix[3,0] = vector.x;
            matrix[3, 1] = vector.y;
            matrix[3, 2] = vector.z;
            return matrix;
        }

        // TODO Create Rotate

        /// <summary>Transforms a position by this matrix, with a perspective divide</summary>
        public Vector3 MultiplyPoint(Vector3 point)
        {
            Vector3 result = new Vector3();
            float w;

            result.x = data[0,0] * point.x + data[0,1] * point.y + data[0,2] * point.z + data[0,3];
            result.y = data[1, 0] * point.x + data[1, 1] * point.y + data[1, 2] * point.z + data[1, 3];
            result.z = data[2, 0] * point.x + data[2, 1] * point.y + data[2, 2] * point.z + data[2, 3];
            w = data[3, 0] * point.x + data[3, 1] * point.y + data[3, 2] * point.z + data[3, 3];

            w = 1f / w;
            result.x *= w;
            result.y *= w;
            result.z *= w;

            return result;
        }

        /// <summary>Transform a position by this matrix, without a perspective divide</summary>
        public Vector3 MultiplyPoint3x4(Vector3 point)
        {
            Vector3 result = new Vector3();

            result.x = data[0, 0] * point.x + data[0, 1] * point.y + data[0, 2] * point.z + data[0, 3];
            result.y = data[1, 0] * point.x + data[1, 1] * point.y + data[1, 2] * point.z + data[1, 3];
            result.z = data[2, 0] * point.x + data[2, 1] * point.y + data[2, 2] * point.z + data[2, 3];

            return result;
        }

        /// <summary>Transforms a direction by this matrix.</summary>
        public Vector3 MultiplyVector(Vector3 vector)
        {
            Vector3 result = new Vector3();

            result.x = data[0, 0] * vector.x + data[0, 1] * vector.y + data[0, 2] * vector.z;
            result.x = data[1, 0] * vector.x + data[1, 1] * vector.y + data[1, 2] * vector.z;
            result.x = data[2, 0] * vector.x + data[2, 1] * vector.y + data[2, 2] * vector.z;

            return result;
        }

        public static Matrix4x4 CreateRotationX(float angleInRadians)
        {
            float cosTheta = (float)Trigq.Cos(angleInRadians);
            float sinTheta = (float)Trigq.Sin(angleInRadians);

            Matrix4x4 matrix = Matrix4x4.Identity;

            matrix[1, 1] = cosTheta;
            matrix[1, 2] = sinTheta;
            matrix[2, 1] = -sinTheta;
            matrix[2, 2] = cosTheta;

            return matrix;
        }

        public static Matrix4x4 CreateRotationY(float angleInRadians)
        {
            float cosTheta = (float)Trigq.Cos(angleInRadians);
            float sinTheta = (float)Trigq.Sin(angleInRadians);

            Matrix4x4 matrix = Identity;
            matrix[0, 0] = cosTheta;
            matrix[0, 2] = -sinTheta;
            matrix[2, 0] = sinTheta;
            matrix[2, 2] = cosTheta;

            return matrix;
        }

        public static Matrix4x4 CreateRotationZ(float angleInRadians)
        {
            float cosTheta = (float)Trigq.Cos(angleInRadians);
            float sinTheta = (float)Trigq.Sin(angleInRadians);

            Matrix4x4 matrix = Matrix4x4.Identity;

            matrix[0, 0] = cosTheta;
            matrix[0, 1] = sinTheta;
            matrix[1, 0] = -sinTheta;
            matrix[1, 1] = cosTheta;

            return matrix;
        }

        public static Matrix4x4 CreateLookAt(Vector3 position, Vector3 target, Vector3 upVector)
        {
            Vector3 forward = (target - position).normalized;

            Vector3 right = Vector3.Cross(upVector, forward).normalized;
            Vector3 up = Vector3.Cross(forward, right);

            Matrix4x4 matrix = Identity;

            matrix.SetColumn(0, right);
            matrix.SetColumn(1, up);
            matrix.SetColumn(2, forward);

            matrix[3, 0] = -Vector3.Dot(right, position);
            matrix[3, 1] = -Vector3.Dot(up, position);
            matrix[3, 2] = -Vector3.Dot(forward, position);

            return matrix;
        }

        public static Matrix4x4 CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
        {
            if(fieldOfView <= 0 || fieldOfView >= 179)
                throw new ArgumentOutOfRangeException(nameof(fieldOfView), "Field of view must be between 1 and 178 degrees.");

            if(aspectRatio < 0)
                throw new ArgumentOutOfRangeException(nameof(aspectRatio), "Aspect ratio must be greater than or equal to zero.");

            if(nearPlaneDistance <= 0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance), "Near clipping plane must be greater than zero.");

            if(farPlaneDistance <=  nearPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance), "Far clipping plane must be greater than the near clipping plane.");

            float halfFovRadians = fieldOfView * 0.5f * Mathq.Pi / 180f;
            float tanHalfFovy = Trigq.Tan(halfFovRadians);

            float yScale = 1f / tanHalfFovy;
            float xScale = yScale / aspectRatio;

            float zRange = farPlaneDistance - nearPlaneDistance;
            float zScale = 1.0f / zRange;

            Matrix4x4 matrix = Zero;

            matrix[0, 0] = xScale;
            matrix[1, 1] = yScale;

            float nearFarRange = float.IsInfinity(farPlaneDistance) ? -1f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            matrix[2, 2] = nearFarRange;

            // Cant set
            matrix[2, 3] = -1f;
            matrix[3, 2] = nearPlaneDistance * nearFarRange;

            return matrix;
        }

        #endregion

        #region Operators

        public static Matrix4x4 operator * (Matrix4x4 lhs, Matrix4x4 rhs)
        {
            Matrix4x4 result = new Matrix4x4();
            result.data[0, 0] = lhs.data[0, 0] * rhs.data[0, 0] + lhs.data[0, 1] * rhs.data[1, 0] + lhs.data[0, 2] * rhs.data[2, 0] + lhs.data[0, 3] * rhs.data[3, 0];
            result.data[0, 1] = lhs.data[0, 0] * rhs.data[0, 1] + lhs.data[0, 1] * rhs.data[1, 1] + lhs.data[0, 2] * rhs.data[2, 1] + lhs.data[0, 3] * rhs.data[3, 1];
            result.data[0, 2] = lhs.data[0, 0] * rhs.data[0, 2] + lhs.data[0, 1] * rhs.data[1, 2] + lhs.data[0, 2] * rhs.data[2, 2] + lhs.data[0, 3] * rhs.data[3, 2];
            result.data[0, 3] = lhs.data[0, 0] * rhs.data[0, 3] + lhs.data[0, 1] * rhs.data[1, 3] + lhs.data[0, 2] * rhs.data[2, 3] + lhs.data[0, 3] * rhs.data[3, 3];

            result.data[1, 0] = lhs.data[1, 0] * rhs.data[0, 0] + lhs.data[1, 1] * rhs.data[1, 0] + lhs.data[1, 2] * rhs.data[2, 0] + lhs.data[1, 3] * rhs.data[3, 0];
            result.data[1, 1] = lhs.data[1, 0] * rhs.data[0, 1] + lhs.data[1, 1] * rhs.data[1, 1] + lhs.data[1, 2] * rhs.data[2, 1] + lhs.data[1, 3] * rhs.data[3, 1];
            result.data[1, 2] = lhs.data[1, 0] * rhs.data[0, 2] + lhs.data[1, 1] * rhs.data[1, 2] + lhs.data[1, 2] * rhs.data[2, 2] + lhs.data[1, 3] * rhs.data[3, 2];
            result.data[1, 3] = lhs.data[1, 0] * rhs.data[0, 3] + lhs.data[1, 1] * rhs.data[1, 3] + lhs.data[1, 2] * rhs.data[2, 3] + lhs.data[1, 3] * rhs.data[3, 3];

            result.data[2, 0] = lhs.data[2, 0] * rhs.data[0, 0] + lhs.data[2, 1] * rhs.data[1, 0] + lhs.data[2, 2] * rhs.data[2, 0] + lhs.data[2, 3] * rhs.data[3, 0];
            result.data[2, 1] = lhs.data[2, 0] * rhs.data[0, 1] + lhs.data[2, 1] * rhs.data[1, 1] + lhs.data[2, 2] * rhs.data[2, 1] + lhs.data[2, 3] * rhs.data[3, 1];
            result.data[2, 2] = lhs.data[2, 0] * rhs.data[0, 2] + lhs.data[2, 1] * rhs.data[1, 2] + lhs.data[2, 2] * rhs.data[2, 2] + lhs.data[2, 3] * rhs.data[3, 2];
            result.data[2, 3] = lhs.data[2, 0] * rhs.data[0, 3] + lhs.data[2, 1] * rhs.data[1, 3] + lhs.data[2, 2] * rhs.data[2, 3] + lhs.data[2, 3] * rhs.data[3, 3];

            result.data[3, 0] = lhs.data[3, 0] * rhs.data[0, 0] + lhs.data[3, 1] * rhs.data[1, 0] + lhs.data[3, 2] * rhs.data[2, 0] + lhs.data[3, 3] * rhs.data[3, 0];
            result.data[3, 1] = lhs.data[3, 0] * rhs.data[0, 1] + lhs.data[3, 1] * rhs.data[1, 1] + lhs.data[3, 2] * rhs.data[2, 1] + lhs.data[3, 3] * rhs.data[3, 1];
            result.data[3, 2] = lhs.data[3, 0] * rhs.data[0, 2] + lhs.data[3, 1] * rhs.data[1, 2] + lhs.data[3, 2] * rhs.data[2, 2] + lhs.data[3, 3] * rhs.data[3, 2];
            result.data[3, 3] = lhs.data[3, 0] * rhs.data[0, 3] + lhs.data[3, 1] * rhs.data[1, 3] + lhs.data[3, 2] * rhs.data[2, 3] + lhs.data[3, 3] * rhs.data[3, 3];

            return result;
        }

        public static Vector4 operator * (Matrix4x4 lhs, Vector4 vector)
        {
            Vector4 result = new Vector4();
            result.x = lhs.data[0, 0] * vector.x + lhs.data[0, 1] * vector.y + lhs.data[0, 2] * vector.z + lhs.data[0, 3] * vector.w;
            result.y = lhs.data[1, 0] * vector.x + lhs.data[1, 1] * vector.y + lhs.data[1, 2] * vector.z + lhs.data[1, 3] * vector.w;
            result.z = lhs.data[2, 0] * vector.x + lhs.data[2, 1] * vector.y + lhs.data[2, 2] * vector.z + lhs.data[2, 3] * vector.w;
            result.w = lhs.data[3, 0] * vector.x + lhs.data[3, 1] * vector.y + lhs.data[3, 2] * vector.z + lhs.data[3, 3] * vector.w;

            return result;
        }

        public static bool operator == (Matrix4x4 lhs, Matrix4x4 rhs)
        {
            return lhs.GetColumn(0) == rhs.GetColumn(0)
                && lhs.GetColumn(1) == rhs.GetColumn(1)
                && lhs.GetColumn(2) == rhs.GetColumn(2)
                && lhs.GetColumn(3) == rhs.GetColumn(3);
        }

        public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}
