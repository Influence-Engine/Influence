using System;

namespace Influence
{
    // Credits to dotNet-Quirks for this code <3 (MIT LICENSE)
    // https://github.com/dotNet-Quirks/Util-Lib

    public partial struct Trigq
    {
        #region Sin

        /// <summary>Returns the sine of angle x.</summary>
        public static double Sin(double x) => Math.Sin(x);
        /// <summary>Returns the sine of angle x.</summary>
        public static float Sin(float x) => (float)Math.Sin(x);

        #endregion

        #region Sinh

        public static double Sinh(double x) => Math.Sinh(x);
        public static float Sinh(float x) => (float)Math.Sinh(x);

        #endregion

        #region Asin

        /// <summary>Returns the arc-sine of x - the angle in radians whose sine is x.</summary>
        public static double Asin(double x) => Math.Asin(x);
        /// <summary>Returns the arc-sine of x - the angle in radians whose sine is x.</summary>
        public static float Asin(float x) => (float)Math.Asin(x);

        #endregion

        #region Asinh

        public static double Asinh(double x) => Math.Asinh(x);
        public static float Asinh(float x) => (float)Math.Asinh(x);

        #endregion



        #region Cos

        /// <summary>Returns the cosine of angle x.</summary>
        public static double Cos(double x) => Math.Cos(x);
        /// <summary>Returns the cosine of angle x.</summary>
        public static float Cos(float x) => (float)Math.Cos(x);

        #endregion

        #region Cosh

        public static double Cosh(double x) => Math.Cosh(x);
        public static float Cosh(float x) => (float)Math.Cosh(x);

        #endregion

        #region Acos

        /// <summary>Returns the arc-cosine of x - the angle in radians whose cosine is x.</summary>
        public static double Acos(double x) => Math.Acos(x);
        /// <summary>Returns the arc-cosine of x - the angle in radians whose cosine is x.</summary>
        public static float Acos(float x) => (float)Math.Acos(x);

        #endregion

        #region Acosh

        public static double Acosh(double x) => Math.Acosh(x);
        public static float Acosh(float x) => (float)Math.Acosh(x);

        #endregion



        #region Tan

        /// <summary>Returns the tangent of angle x in radians.</summary>
        public static double Tan(double x) => Math.Tan(x);
        /// <summary>Returns the tangent of angle x in radians.</summary>
        public static float Tan(float x) => (float)Math.Tan(x);

        #endregion

        #region Tanh

        public static double Tanh(double x) => Math.Tanh(x);
        public static float Tanh(float x) => (float)Math.Tanh(x);

        #endregion

        #region Atan

        /// <summary>Returns the arc-tangent of x - the angle in radians whose tangent is x.</summary>
        public static double Atan(double x) => Math.Atan(x);
        /// <summary>Returns the arc-tangent of x - the angle in radians whose tangent is x.</summary>
        public static float Atan(float x) => (float)Math.Atan(x);

        #endregion

        #region Atan2

        /// <summary>Returns the angle in radians whose tangent is y/x.</summary>
        public static double Atan2(double y, double x) => Math.Atan2(y, x);
        /// <summary>Returns the angle in radians whose tangent is y/x.</summary>
        public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);

        #endregion

        #region Atanh

        public static double Atanh(double x) => Math.Atanh(x);
        public static float Atanh(float x) => (float)Math.Atanh(x);

        #endregion



        #region Sec

        /// <summary>Returns the secant of an angle in radians, or hypotenuse / adjacent.</summary>
        public static float Sec(float x) => 1f / Cos(x);
        /// <summary>Returns the secant of an angle in radians, or hypotenuse / adjacent.</summary>
        public static double Sec(double x) => 1.0 / Cos(x);

        #endregion

        #region Sech

        /// <summary>Returns the secant.</summary>
        public static float Sech(float x) => 1f / Cosh(x);
        /// <summary>Returns the arc-secant.</summary>
        public static double Sech(double x) => 1.0 / Cosh(x);

        #endregion

        #region Asec

        /// <summary>Returns the arc-secant.</summary>
        public static float Asec(float x) => Acos(1f / x);
        /// <summary>Returns the arc-secant.</summary>
        public static double Asec(double x) => Acos(1 / x);

        #endregion

        #region Asech

        /// <summary>Returns the area-secant.</summary>
        public static float Asech(float x) => Acosh(1f / x);
        /// <summary>Returns the area-secant.</summary>
        public static double Asech(double x) => Acosh(1.0 / x);

        #endregion



        #region Csc

        /// <summary>Returns the cosecant of an angle in radians, or hypotenuse / opposite.</summary>
        public static float Csc(float x) => 1f / Sin(x);
        /// <summary>Returns the cosecant of an angle in radians, or hypotenuse / opposite.</summary>
        public static double Csc(double x) => 1.0 / Sin(x);

        #endregion

        #region Csch

        /// <summary>Returns the hyperbolic-cosecant.</summary>
        public static float Csch(float x) => 1f / Sinh(x);
        /// <summary>Returns the hyperbolic-cosecant.</summary>
        public static double Csch(double x) => 1.0 / Sinh(x);

        #endregion

        #region Acsc

        /// <summary>Returns the arc-cosecant in radians.</summary>
        public static float Acsc(float x) => Asin(1f / x);
        /// <summary>Returns the arc-cosecant in radians.</summary>
        public static double Acsc(double x) => Asin(1.0 / x);

        #endregion

        #region Acsch

        /// <summary>Returns the hyperbolic-area-cosecant.</summary>
        public static float Acsch(float x) => Asinh(1f / x);
        /// <summary>Returns the hyperbolic-area-cosecant.</summary>
        public static double Acsch(double x) => Asinh(1.0 / x);

        #endregion



        #region Cot

        /// <summary>Returns the cotangent of an angle in radians, or adjacent / opposite.</summary>
        public static float Cot(float x) => 1f / Tan(x);
        /// <summary>Returns the cotangent of an angle in radians, or adjacent / opposite.</summary>
        public static double Cot(double x) => 1.0 / Tan(x);

        #endregion

        #region Coth

        /// <summary>Returns the hyperbolic-cotangent.</summary>
        public static float Coth(float x)
        {
            if (x > 19.115)
                return 1;

            if (x < -19.115)
                return -1;

            float exp1 = Mathq.Exp(x);
            float exp2 = Mathq.Exp(-x);
            return (exp1 + exp2) / (exp1 - exp2);
        }
        /// <summary>Returns the hyperbolic-cotangent.</summary>
        public static double Coth(double x)
        {
            if (x > 19.115)
                return 1;

            if (x < -19.115)
                return -1;

            double exp1 = Mathq.Exp(x);
            double exp2 = Mathq.Exp(-x);
            return (exp1 + exp2) / (exp1 - exp2);
        }

        #endregion

        #region Acot

        /// <summary>Returns the arc-cotangent.</summary>
        public static float Acot(float x) => Atan(1f / x);
        /// <summary>Returns the arc-cotangent.</summary>
        public static double Acot(double x) => Atan(1.0 / x);

        #endregion

        #region Acoth

        /// <summary>Returns the hyperbolic-area-cotangent</summary>
        public static float Acoth(float x) => 0.5f * Mathq.Log((x + 1) / (x - 1), Mathq.E);
        /// <summary>Returns the hyperbolic-area-cotangent</summary>
        public static double Acoth(double x) => 0.5 * Mathq.Log((x + 1) / (x - 1), Mathq.doubleE);

        #endregion
    }
}
