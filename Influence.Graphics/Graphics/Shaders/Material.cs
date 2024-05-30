
namespace Influence
{
    /// <summary>Represents a material in a rendering pipeline, encapsulating a shader and a color.</summary>
    public class Material
    {
        /// <summary>The shader associated with this material.</summary>
        Shader shader;

        /// <summary>The color of the material.</summary>
        Color _color;

        /// <summary>The location of the color uniform in the shader program.</summary>
        int colorLocation;

        /// <summary>The color of the material.</summary>
        public Color color
        {
            get { return _color; }
            set
            {
                _color = value;
                shader.gl.Uniform4(colorLocation, value.r, value.g, value.b, value.a);
            }
        }

        /// <summary>Initializes a new instance of the Material class with a specific shader and color.</summary>
        /// <param name="shader">The shader to associate with this material.</param>
        /// <param name="color">The initial color of the material.</param>
        public Material(Shader shader, Color color)
        {
            this.shader = shader;
            this.colorLocation = shader.gl.GetUniformLocation(shader.programID, "mColor");
            this.color = color;
        }

        /// <summary>Initializes a new instance of the Material class with a specified shader.</summary>
        /// <param name="shader">The shader to associate with this material.</param>
        public Material(Shader shader) : this(shader, Color.White) { }

        /// <summary>Initializes a new instance of the Material class with a default shader and a specified color.</summary>
        /// <param name="color">The initial color of the material.</param>
        public Material(Color color) : this(new Shader(), color) { }

        /// <summary>Initializes a new instance of the Material class with a default shader.</summary>
        public Material() : this(new Shader(), Color.White) { }

        /// <summary>Sets up the material for use.</summary>
        public void Use()
        {
            shader.Use();
            SetColor(colorLocation, color);
        }

        /// <summary>Disposes of the material, cleaning up resources.</summary>
        public void Dispose()
        {
            shader.Dispose();
        }

        #region Functions

        #region Set

        public void SetBool(string name, bool value) => shader.SetBool(name, value);

        public void SetInt(string name, int value) => shader.SetInt(name, value);

        public void SetFloat(string name, float value) => shader.SetFloat(name, value);

        public void SetDouble(string name, double value) => shader.SetDouble(name, value);

        public void SetVector2(string name, Vector2 vector) => shader.SetVector2(name, vector);

        public void SetVector3(string name, Vector3 vector) => shader.SetVector3(name, vector);

        public void SetVector4(string name, Vector4 vector) => shader.SetVector4(name, vector);
        public void SetVector4(int location, Vector4 vector) => shader.SetVector4(location, vector);

        public void SetMatrix4(string name, Matrix4x4 matrix) => shader.SetMatrix4(name, matrix);

        public void SetMatrices(string name, params Matrix4x4[] matrices) => shader.SetMatrices(name, matrices);

        public void SetColor(string name, Color color) => shader.SetColor(name, color);
        public void SetColor(int location, Color color) => shader.SetColor(location, color);

        // TODO SetTexture

        #endregion

        #region Get

        public bool GetBool(string name) => shader.GetBool(name);

        public int GetInt(string name) => shader.GetInt(name);

        public float GetFloat(string name) => shader.GetFloat(name);

        public double GetDouble(string name) => shader.GetDouble(name);

        public Vector2 GetVector2(string name) => shader.GetVector2(name);

        public Vector3 GetVector3(string name) => shader.GetVector3(name);

        public Vector4 GetVector4(string name) => shader.GetVector4(name);

        public Matrix4x4 GetMatrix4(string name) => shader.GetMatrix4(name);

        public Matrix4x4[] GetMatrices(string name, int count) => shader.GetMatrices(name, count);

        public Color GetColor(string name) => shader.GetColor(name);

        // TODO GetTexture

        #endregion

        #endregion
    }
}
