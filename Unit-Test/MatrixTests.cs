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
            return data[index % 4, index / 4];
        }
    }
}
