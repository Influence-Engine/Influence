using System.Collections.Generic;

namespace Influence
{
    // Credits to dotNet-Quirks for this code <3 (MIT LICENSE)
    // https://github.com/dotNet-Quirks/Util-Lib

    public partial struct Interpq
    {
        #region Lerp

        public static int Lerp(int a, int b, float t) => Mathq.AsInt(a + (b - a) * Mathq.Clamp01(t));
        public static uint Lerp(uint a, uint b, float t) => Mathq.AsUInt(a + (b - a) * Mathq.Clamp01(t));
        public static long Lerp(long a, long b, float t) => Mathq.AsLong(a + (b - a) * Mathq.Clamp01(t));
        public static ulong Lerp(ulong a, ulong b, float t) => Mathq.AsULong(a + (b - a) * Mathq.Clamp01(t));
        public static float Lerp(float a, float b, float t) => a + (b - a) * Mathq.Clamp01(t);
        public static double Lerp(double a, double b, double t) => a + (b - a) * Mathq.Clamp01(t);

        #endregion

        #region LerpUnclamped

        public static int LerpUnclamped(int a, int b, float t) => Mathq.AsInt(a + (b - a) * t);
        public static uint LerpUnclamped(uint a, uint b, float t) => Mathq.AsUInt(a + (b - a) * t);
        public static long LerpUnclamped(long a, long b, float t) => Mathq.AsLong(a + (b - a) * t);
        public static ulong LerpUnclamped(ulong a, ulong b, float t) => Mathq.AsULong(a + (b - a) * t);
        public static float LerpUnclamped(float a, float b, float t) => a + (b - a) * t;
        public static double LerpUnclamped(double a, double b, double t) => a + (b - a) * t;

        #endregion

        #region InverseLerp

        public static float InverseLerp(float a, float b, float x)
        {
            if (a != b)
            {
                return Mathq.Clamp01((x - a) / (b - a));
            }

            return 0f;
        }
        public static double InverseLerp(double a, double b, double x)
        {
            if (a != b)
            {
                return Mathq.Clamp01((x - a) / (b - a));
            }

            return 0.0;
        }

        #endregion

        #region Spline (Untested)

        public static List<float> Spline(List<float> x, List<float> y)
        {
            int n = x.Count;
            float[] differences = new float[n];
            float[] alpha = new float[n];

            float[] c = new float[n + 1];
            float[] l = new float[n + 1];
            float[] mu = new float[n];
            float[] z = new float[n + 1];

            float[] a = new float[n];
            float[] b = new float[n];
            float[] d = new float[n];

            for (int i = 1; i < n; i++)
            {
                differences[i] = x[i] - x[i - 1];
            }

            alpha[0] = 3 * (y[1] - y[0]) / differences[1] - 3 * (y[2] - y[1]) / differences[2];

            for (int i = 1; i < n - 1; i++)
            {
                alpha[i] = 3 * (y[i + 1] - y[i]) / differences[i + 1] - 3 * (y[i] - y[i - 1]) / differences[i];
            }
            alpha[n - 1] = 3 * (y[n - 1] - y[n - 2]) / differences[n - 1] - 3 * (y[n - 2] - y[n - 3]) / differences[n - 2];

            l[1] = 2 * (x[2] - x[1]) - differences[1];
            mu[1] = 0.5f;
            z[1] = alpha[1] / l[1];

            for (int i = 2; i < n; i++)
            {
                l[i] = 2 * (x[i + 1] - x[i - 1]) - differences[i - 1] * mu[i - 1];
                mu[i] = differences[i] / l[i];
                z[i] = (alpha[i] - differences[i - 1] * z[i - 1]) / l[i];
            }

            l[n] = differences[n - 1] * (2 * mu[n - 1] + 1) / 3;
            z[n] = 0;
            c[n] = 0;

            for (int i = n - 1; i >= 0; i--)
            {
                c[i] = z[i] - mu[i] * c[i + 1];
                b[i] = (y[i + 1] - y[i]) / differences[i] - differences[i] * (c[i + 1] + 2 * c[i]) / 3;
                d[i] = (c[i + 1] - c[i]) / (3 * differences[i]);
                a[i] = y[i];
            }

            List<float> interpolatedValues = new List<float>();
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < 100; j++) // 100 steps between each pair of points
                {
                    float t = j / 100f;
                    //double xi = x[i] + t * differences[i];
                    interpolatedValues.Add(a[i] + b[i] * t + c[i] * t * t + d[i] * t * t * t);
                }
            }

            // Add the last point
            interpolatedValues.Add(y[n - 1]);

            return interpolatedValues;
        }

        public static List<double> Spline(List<double> x, List<double> y)
        {
            int n = x.Count;
            double[] differences = new double[n];
            double[] alpha = new double[n];

            double[] c = new double[n + 1];
            double[] l = new double[n + 1];
            double[] mu = new double[n];
            double[] z = new double[n + 1];

            double[] a = new double[n];
            double[] b = new double[n];
            double[] d = new double[n];

            for (int i = 1; i < n; i++)
            {
                differences[i] = x[i] - x[i - 1];
            }

            alpha[0] = 3 * (y[1] - y[0]) / differences[1] - 3 * (y[2] - y[1]) / differences[2];

            for (int i = 1; i < n - 1; i++)
            {
                alpha[i] = 3 * (y[i + 1] - y[i]) / differences[i + 1] - 3 * (y[i] - y[i - 1]) / differences[i];
            }
            alpha[n - 1] = 3 * (y[n - 1] - y[n - 2]) / differences[n - 1] - 3 * (y[n - 2] - y[n - 3]) / differences[n - 2];

            l[1] = 2 * (x[2] - x[1]) - differences[1];
            mu[1] = 0.5;
            z[1] = alpha[1] / l[1];

            for (int i = 2; i < n; i++)
            {
                l[i] = 2 * (x[i + 1] - x[i - 1]) - differences[i - 1] * mu[i - 1];
                mu[i] = differences[i] / l[i];
                z[i] = (alpha[i] - differences[i - 1] * z[i - 1]) / l[i];
            }

            l[n] = differences[n - 1] * (2 * mu[n - 1] + 1) / 3;
            z[n] = 0;
            c[n] = 0;

            for (int i = n - 1; i >= 0; i--)
            {
                c[i] = z[i] - mu[i] * c[i + 1];
                b[i] = (y[i + 1] - y[i]) / differences[i] - differences[i] * (c[i + 1] + 2 * c[i]) / 3;
                d[i] = (c[i + 1] - c[i]) / (3 * differences[i]);
                a[i] = y[i];
            }

            List<double> interpolatedValues = new List<double>();
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < 100; j++) // 100 steps between each pair of points
                {
                    double t = j / 100.0;
                    //double xi = x[i] + t * differences[i];
                    interpolatedValues.Add(a[i] + b[i] * t + c[i] * t * t + d[i] * t * t * t);
                }
            }

            // Add the last point
            interpolatedValues.Add(y[n - 1]);

            return interpolatedValues;
        }

        #endregion

        #region Repeat

        /// <summary>Loops the value t, so that it is never smaller than 0 and never larger than length.</summary>
        public static float Repeat(float t, float length) => Mathq.Clamp(t - Mathq.Floor(t / length) * length, 0f, length);
        /// <summary>Loops the value t, so that it is never smaller than 0 and never larger than length.</summary>
        public static double Repeat(double t, double length) => Mathq.Clamp(t - Mathq.Floor(t / length) * length, 0f, length);

        #endregion

        #region Remap

        public static int Remap(int value, int from1, int to1, int from2, int to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static uint Remap(uint value, uint from1, uint to1, uint from2, uint to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static long Remap(long value, long from1, long to1, long from2, long to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static ulong Remap(ulong value, ulong from1, ulong to1, ulong from2, ulong to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static double Remap(double value, double from1, double to1, double from2, double to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        #endregion

        #region SmoothStep

        public static int SmoothStep(int from, int to, float t)
        {
            int x = Mathq.AsInt(Mathq.Clamp01((t - from) / (to - from)));
            return x * x * (3 - 2 * x);
        }
        public static uint SmoothStep(uint from, uint to, float t)
        {
            uint x = Mathq.AsUInt(Mathq.Clamp01((t - from) / (to - from)));
            return x * x * (3 - 2 * x);
        }
        public static long SmoothStep(long from, long to, float t)
        {
            long x = Mathq.AsLong(Mathq.Clamp01((t - from) / (to - from)));
            return x * x * (3 - 2 * x);
        }
        public static ulong SmoothStep(ulong from, ulong to, float t)
        {
            ulong x = Mathq.AsULong(Mathq.Clamp01((t - from) / (to - from)));
            return x * x * (3 - 2 * x);
        }
        public static float SmoothStep(float from, float to, float t)
        {
            float x = Mathq.Clamp01((t - from) / (to - from));
            return x * x * (3 - 2 * x);
        }
        public static double SmoothStep(double from, double to, double t)
        {
            double x = Mathq.Clamp01((t - from) / (to - from));
            return x * x * (3 - 2 * x);
        }

        #endregion
    }
}
