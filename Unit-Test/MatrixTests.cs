
using Influence;

namespace UnitTest.Math
{
    public class MatrixTests
    {
        [Fact]
        public void Multiplication()
        {
            Matrix4x4 a = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
            Matrix4x4 b = new Matrix4x4(new Vector4(2, 0, 0, 0), new Vector4(0, 2, 0, 0), new Vector4(0, 0, 2, 0), new Vector4(0, 0, 0, 2));
            Matrix4x4 result = a * b;

            Matrix4x4 expected = new Matrix4x4(new Vector4(2, 0, 0, 0), new Vector4(0, 2, 0, 0), new Vector4(0, 0, 2, 0), new Vector4(0, 0, 0, 2));

            Assert.Equal(result, expected);
        }

        [Fact]
        public void Translation_1()
        {
            Matrix4x4 translation = Matrix4x4.Translate(new Vector3(1, 2, 3));
            Vector3 originalPoint = new Vector3(4, 5, 6);

            Vector3 result = translation.MultiplyPoint3x4(originalPoint);
            Vector3 expected = new Vector3(5, 7, 9);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void Translation_2()
        {
            Matrix4x4 translation = Matrix4x4.Translate(new Vector3(-5.75f, 3.25f, -1.5f));
            Vector3 originalPoint = new Vector3(2.5f, -4.5f, 1.25f);

            Vector3 result = translation.MultiplyPoint3x4(originalPoint);
            Vector3 expected = new Vector3(-3.25f, -1.25f, -0.25f);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void Transpose()
        {
            Matrix4x4 a = new Matrix4x4(
                new Vector4(1, 2, 3, 4),
                new Vector4(5, 6, 7, 8),
                new Vector4(9, 10, 11, 12),
                new Vector4(13, 14, 15, 16)
            );

            a.Transpose();

            Matrix4x4 expected = new Matrix4x4(
                new Vector4(1, 5, 9, 13),
                new Vector4(2, 6, 10, 14),
                new Vector4(3, 7, 11, 15),
                new Vector4(4, 8, 12, 16)
            );

            Assert.Equal(a, expected);
        }
    }
}
