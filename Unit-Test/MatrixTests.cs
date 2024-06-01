using System.Diagnostics;

using Influence;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace UnitTest.Math
{
    public class MatrixTests
    {
        readonly ITestOutputHelper output;
        public MatrixTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        readonly Matrix4x4 testMatrix1 = Matrix4x4.Identity;
        readonly Matrix4x4 testMatrix2 = new Matrix4x4
            ([
                0, 1, 2, 3,
                4, 5, 6, 7,
                8, 9, 10, 11,
                12, 13, 14, 15
            ]);

        const int testCount = 100000;

        #region Creation Tests

        [Fact]
        public void IdentityMatrix()
        {
            Matrix4x4 identity = Matrix4x4.Identity;

            // Test each element against expected values
            Assert.Equal(1f, identity[0, 0]);
            Assert.Equal(0f, identity[0, 1]);
            Assert.Equal(0f, identity[0, 2]);
            Assert.Equal(0f, identity[0, 3]);

            Assert.Equal(0f, identity[1, 0]);
            Assert.Equal(1f, identity[1, 1]);
            Assert.Equal(0f, identity[1, 2]);
            Assert.Equal(0f, identity[1, 3]);

            Assert.Equal(0f, identity[2, 0]);
            Assert.Equal(0f, identity[2, 1]);
            Assert.Equal(1f, identity[2, 2]);
            Assert.Equal(0f, identity[2, 3]);

            Assert.Equal(0f, identity[3, 0]);
            Assert.Equal(0f, identity[3, 1]);
            Assert.Equal(0f, identity[3, 2]);
            Assert.Equal(1f, identity[3, 3]);
        }

        [Fact]
        public void TranslationMatrix()
        {
            Matrix4x4 translationMatrix = Matrix4x4.CreateTranslate(new Vector3(1f, 2f, 3f));

            // Test translation matrix against expected values
            Assert.Equal(1f, translationMatrix[0, 0]);
            Assert.Equal(0f, translationMatrix[0, 1]);
            Assert.Equal(0f, translationMatrix[0, 2]);
            Assert.Equal(1f, translationMatrix[0, 3]);

            Assert.Equal(0f, translationMatrix[1, 0]);
            Assert.Equal(1f, translationMatrix[1, 1]);
            Assert.Equal(0f, translationMatrix[1, 2]);
            Assert.Equal(2f, translationMatrix[1, 3]);

            Assert.Equal(0f, translationMatrix[2, 0]);
            Assert.Equal(0f, translationMatrix[2, 1]);
            Assert.Equal(1f, translationMatrix[2, 2]);
            Assert.Equal(3f, translationMatrix[2, 3]);

            Assert.Equal(0f, translationMatrix[3, 0]);
            Assert.Equal(0f, translationMatrix[3, 1]);
            Assert.Equal(0f, translationMatrix[3, 2]);
            Assert.Equal(1f, translationMatrix[3, 3]);
        }

        [Fact]
        public void ScaleMatrix()
        {
            // Create a scale matrix
            Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(new Vector3(2f, 3f, 4f));

            // Test scale matrix against expected values
            Assert.Equal(2f, scaleMatrix[0, 0]);
            Assert.Equal(0f, scaleMatrix[0, 1]);
            Assert.Equal(0f, scaleMatrix[0, 2]);
            Assert.Equal(0f, scaleMatrix[0, 3]);

            Assert.Equal(0f, scaleMatrix[1, 0]);
            Assert.Equal(3f, scaleMatrix[1, 1]);
            Assert.Equal(0f, scaleMatrix[1, 2]);
            Assert.Equal(0f, scaleMatrix[1, 3]);

            Assert.Equal(0f, scaleMatrix[2, 0]);
            Assert.Equal(0f, scaleMatrix[2, 1]);
            Assert.Equal(4f, scaleMatrix[2, 2]);
            Assert.Equal(0f, scaleMatrix[2, 3]);

            Assert.Equal(0f, scaleMatrix[3, 0]);
            Assert.Equal(0f, scaleMatrix[3, 1]);
            Assert.Equal(0f, scaleMatrix[3, 2]);
            Assert.Equal(1f, scaleMatrix[3, 3]);
        }

        [Fact]
        public void UniformScaleMatrix()
        {
            // Create a uniform scale matrix
            Matrix4x4 uniformScaleMatrix = Matrix4x4.CreateUniformScale(2f);

            // Test uniform scale matrix against expected values
            Assert.Equal(2f, uniformScaleMatrix[0, 0]);
            Assert.Equal(0f, uniformScaleMatrix[0, 1]);
            Assert.Equal(0f, uniformScaleMatrix[0, 2]);
            Assert.Equal(0f, uniformScaleMatrix[0, 3]);

            Assert.Equal(0f, uniformScaleMatrix[1, 0]);
            Assert.Equal(2f, uniformScaleMatrix[1, 1]);
            Assert.Equal(0f, uniformScaleMatrix[1, 2]);
            Assert.Equal(0f, uniformScaleMatrix[1, 3]);

            Assert.Equal(0f, uniformScaleMatrix[2, 0]);
            Assert.Equal(0f, uniformScaleMatrix[2, 1]);
            Assert.Equal(2f, uniformScaleMatrix[2, 2]);
            Assert.Equal(0f, uniformScaleMatrix[2, 3]);

            Assert.Equal(0f, uniformScaleMatrix[3, 0]);
            Assert.Equal(0f, uniformScaleMatrix[3, 1]);
            Assert.Equal(0f, uniformScaleMatrix[3, 2]);
            Assert.Equal(1f, uniformScaleMatrix[3, 3]);
        }

        #endregion

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
            Matrix4x4 translation = Matrix4x4.CreateTranslate(new Vector3(1, 2, 3));
            Vector3 originalPoint = new Vector3(4, 5, 6);

            Vector3 result = translation.MultiplyPoint3x4(originalPoint);
            Vector3 expected = new Vector3(5, 7, 9);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void Translation_2()
        {
            Matrix4x4 translation = Matrix4x4.CreateTranslate(new Vector3(-5.75f, 3.25f, -1.5f));
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

        [Fact]
        public void MultiplyPoint()
        {
            // Create a matrix
            Matrix4x4 matrix = testMatrix2;

            // Multiply a point
            Vector3 point = new Vector3(1, 2, 3); 
            Vector3 result = matrix.MultiplyPoint(point);

            // Define the expected result
            Vector3 expectedResult = new Vector3(
                (0f * 1f + 1f * 2f + 2f * 3f + 3f) / ((12f * 1f + 13f * 2f + 14f * 3f + 15f)), // x component
                (4f * 1f + 5f * 2f + 6f * 3f + 7f) / ((12f * 1f + 13f * 2f + 14f * 3f + 15f)), // y component
                (8f * 1f + 9f * 2f + 10f * 3f + 11f) / ((12f * 1f + 13f * 2f + 14f * 3f + 15f)) // z component
            );

            // Assert that the result matches the expected result
            Assert.Equal(expectedResult.x, result.x, 0.0001f);
            Assert.Equal(expectedResult.y, result.y, 0.0001f);
            Assert.Equal(expectedResult.z, result.z, 0.0001f);
        }

        [Fact]
        public void MultiplyVector()
        {
            // Create a matrix
            Matrix4x4 matrix = testMatrix2;

            // Multiply a vector
            Vector3 vector = new Vector3(1, 2, 3);
            Vector3 result = matrix.MultiplyVector(vector);

            // Test the result
            Assert.Equal(8, result.x); // 0*1 + 1*2 + 2*3 = 8
            Assert.Equal(32, result.y); // 4*1 + 5*2 + 6*3 = 32
            Assert.Equal(56, result.z); // 8*1 + 9*2 + 10*3= 56
        }

        [Fact]
        public void CreateLookAt()
        {
            // Define the position, target, and up vector
            Vector3 position = new Vector3(0, 0, -5);
            Vector3 target = new Vector3(0, 0, 0);
            Vector3 upVector = new Vector3(0, 1, 0);

            // Call the CreateLookAt method
            Matrix4x4 transformationMatrix = Matrix4x4.CreateLookAt(position, target, upVector);

            // Calculate the expected forward vector
            Vector3 expectedForwardVector = target - position;

            // Normalize the expected forward vector to match the output of CreateLookAt
            expectedForwardVector.Normalize();

            // Extract the forward vector from the transformation matrix
            Vector3 actualForwardVector = new Vector3(transformationMatrix[0,2], transformationMatrix[1,2], transformationMatrix[2,2]);

            // Normalize the actual forward vector
            actualForwardVector.Normalize();

            // Assert that the extracted forward vector matches the expected one
            Assert.Equal(expectedForwardVector, actualForwardVector); // Adjust the precision as needed
        }

        #region Timed Tests

            // On Quartzi's Machine this method averages around (~73k - ~78k) Ticks for 100k runs
            [Fact]
        public void Timed_IndexerLogicSpeed_Switch()
        {
            Stopwatch sw = Stopwatch.StartNew();
            float valueBySwitch = 0f;
            for (int c = 0; c < testCount; c++)
            {
                for (int i = 0; i < 16; i++)
                {
                    valueBySwitch = GetBySwitch(testMatrix2, i);
                }
            }
            sw.Stop();

            output.WriteLine($"Elapsed time for Switch method: {sw.ElapsedTicks} ticks");
            output.WriteLine($"Method run count: {testCount}");
            Assert.True(true);
        }

        // On Quartzi's Machine this method averages around (~21k - ~22k) Ticks for 100k runs
        [Fact]
        public void Timed_IndexerLogicSpeed_Modulo()
        {
            Stopwatch sw = Stopwatch.StartNew();
            float valueBySwitch = 0f;
            for (int c = 0; c < testCount; c++)
            {
                for (int i = 0; i < 16; i++)
                {
                    valueBySwitch = GetByModulo(testMatrix2, i);
                }
            }
            sw.Stop();

            output.WriteLine($"Elapsed time for Modulo method: {sw.ElapsedTicks} ticks");
            output.WriteLine($"Method run count: {testCount}");
            Assert.True(true);
        }

        #endregion

        #region Testing Functions

        public float GetBySwitch(Matrix4x4 data, int index)
        {
            switch (index)
            {
                case 0: return data[0, 0];
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

        public float GetByModulo(Matrix4x4 data, int index)
        {
            return data[index / 4, index % 4];
        }

        #endregion
    }
}
