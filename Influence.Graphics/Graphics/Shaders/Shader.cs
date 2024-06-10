using System;
using System.Collections.Generic;
using System.Linq;

using Influence.Core;

using Silk.NET.OpenGL;

namespace Influence
{
    /// <summary>Represents a shader program in OpenGL.</summary>
    public class Shader
    {
        public readonly GL gl;
        internal uint programID { get; private set; }

        // Caching uniform locations improves performance significantly compared to repeatedly calling GetUniformLocation for each access.
        // Caching shows an average efficiency gain of ~20%.
        internal Dictionary<string, int> uniformLocations = new Dictionary<string, int>();

        /// <summary>Initializes a new instance of the Shader class with custom vertex and fragment shader source code.</summary>
        /// <param name="vertexShaderSource">The source code for the vertex shader.</param>
        /// <param name="fragmentShaderSource">The source code for the fragment shader.</param>
        public Shader(string vertexShaderSource, string fragmentShaderSource)
        {
            this.gl = GLContext.OpenGL;
            CreateProgram(vertexShaderSource, fragmentShaderSource);
        }

        /// <summary>Initializes a new instance of the Shader class with default vertex and fragment shader source code.</summary>
        public Shader() : this(defaultVertexShaderSource, defaultFragmentShaderSource) { }

        /// <summary>Initializes a new instance of the Shader class with vertex and fragment shader source code loaded from files.</summary>
        /// <param name="vertexShaderFile">The source file containing the vertex shader source code.</param>
        /// <param name="fragmentShaderFile">The source file containing the fragment shader source code.</param>
        public Shader(SourceFile vertexShaderFile, SourceFile fragmentShaderFile) : this(vertexShaderFile.GetFileContent(), fragmentShaderFile.GetFileContent()) { }

        /// <summary>Compiles and links the vertex and fragment shaders into a shader program.</summary>
        /// <param name="vertexShaderSource">The source code for the vertex shader.</param>
        /// <param name="fragmentShaderSource">The source code for the fragment shader.</param>
        void CreateProgram(string vertexShaderSource, string fragmentShaderSource)
        {
            uint vertexShader = CompileShader(ShaderType.VertexShader, vertexShaderSource);
            uint fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentShaderSource);

            programID = gl.CreateProgram();
            gl.AttachShader(programID, vertexShader);
            gl.AttachShader(programID, fragmentShader);
            gl.LinkProgram(programID);

            CheckLinkingErrors();
            CacheUniforms([vertexShaderSource, fragmentShaderSource]);

            gl.DetachShader(programID, vertexShader);
            gl.DetachShader(programID, fragmentShader);
            gl.DeleteShader(vertexShader);
            gl.DeleteShader(fragmentShader);
        }

        /// <summary>Compiles a shader from its source code.</summary>
        /// <param name="type">The type of shader to compile.</param>
        /// <param name="source">The source code of the shader.</param>
        /// <returns>The compiled shader ID.</returns>
        uint CompileShader(ShaderType type, string source)
        {
            uint shader = gl.CreateShader(type);
            gl.ShaderSource(shader, source);
            gl.CompileShader(shader);

            CheckCompilationErrors(shader);

            return shader;
        }

        /// <summary>Activates the shader program.</summary>
        public void Use()
        {
            gl.UseProgram(programID);
        }

        /// <summary>Deletes the shader program.</summary>
        public void Dispose()
        {
            gl.DeleteProgram(programID);
        }

        #region Functions

        /// <summary>Retrieves the location of a uniform variable within the shader program.</summary>
        /// <param name="name">The name of the uniform variable.</param>
        /// <returns>The location of the uniform variable.</returns>
        internal int GetUniformLocation(string name)
        {
            if (uniformLocations.TryGetValue(name, out int location))
                return location;

            location = gl.GetUniformLocation(programID, name);
            if (location == -1)
                throw new Exception($"Uniform '{name}' not found.");
            else
                uniformLocations[name] = location;

            return location;
        }

        #region Set

        public void SetBool(string name, bool value)
        {
            int location = GetUniformLocation(name);
            gl.Uniform1(location, value ? 1 : 0);
        }

        public void SetInt(string name, int value)
        {
            int location = GetUniformLocation(name);
            gl.Uniform1(location, value);
        }

        public void SetFloat(string name, float value)
        {
            int location = GetUniformLocation(name);
            gl.Uniform1(location, value);
        }

        public void SetDouble(string name, double value)
        {
            int location = GetUniformLocation(name);
            gl.Uniform1(location, value);
        }

        public void SetVector2(string name, Vector2 vector)
        {
            int location = GetUniformLocation(name);
            gl.Uniform2(location, vector.x, vector.y);
        }

        public void SetVector3(string name, Vector3 vector)
        {
            int location = GetUniformLocation(name);
            gl.Uniform3(location, vector.x, vector.y, vector.z);
        }

        public void SetVector4(string name, Vector4 vector)
        {
            int location = GetUniformLocation(name);
            gl.Uniform4(location, vector.x, vector.y, vector.z, vector.w);
        }
        public void SetVector4(int location, Vector4 vector) => gl.Uniform4(location, vector.x, vector.y, vector.z, vector.w);

        public void SetMatrix4(string name, Matrix4x4 matrix)
        {
            int location = gl.GetUniformLocation(programID, name);

            ReadOnlySpan<float> spanMatrix = matrix.Matrix.Cast<float>().ToArray();

            gl.UniformMatrix4(location, false, spanMatrix);
        }

        public void SetMatrices(string name, params Matrix4x4[] matrices)
        {
            int location = GetUniformLocation(name);
            foreach (Matrix4x4 matrix in matrices)
            {
                ReadOnlySpan<float> spanMatrix = matrix.Matrix.Cast<float>().ToArray();
                gl.UniformMatrix4(location, false, spanMatrix);
                location++;
            }
        }

        public void SetColor(string name, Color color)
        {
            int location = GetUniformLocation(name);
            gl.Uniform4(location, color.r, color.g, color.b, color.a);
        }
        public void SetColor(int location, Color color) => gl.Uniform4(location, color.r, color.g, color.b, color.a);
        // TODO SetTexture

        #endregion

        #region Get

        public bool GetBool(string name)
        {
            int location = GetUniformLocation(name);

            int value = 0;
            gl.GetUniform(programID, location, out value);

            return value == 1;
        }

        public int GetInt(string name)
        {
            int location = GetUniformLocation(name);

            int value = 0;
            gl.GetUniform(programID, location, out value);

            return value;
        }

        public float GetFloat(string name)
        {
            int location = GetUniformLocation(name);

            float value = 0;
            gl.GetUniform(programID, location, out value);

            return value;
        }

        public double GetDouble(string name)
        {
            int location = GetUniformLocation(name);

            double value = 0;
            gl.GetUniform(programID, location, out value);

            return value;
        }

        public Vector2 GetVector2(string name)
        {
            int location = GetUniformLocation(name);

            float[] values = new float[2];
            gl.GetUniform(programID, location, values);

            return new Vector2(values[0], values[1]);
        }

        public Vector3 GetVector3(string name)
        {
            int location = GetUniformLocation(name);

            float[] values = new float[3];
            gl.GetUniform(programID, location, values);

            return new Vector3(values[0], values[1], values[2]);
        }

        public Vector4 GetVector4(string name)
        {
            int location = GetUniformLocation(name);

            float[] values = new float[4];
            gl.GetUniform(programID, location, values);

            return new Vector4(values[0], values[1], values[2], values[3]);
        }

        public Matrix4x4 GetMatrix4(string name)
        {
            int location = gl.GetUniformLocation(programID, name);

            float[] values = new float[16];
            gl.GetUniform(programID, location, values);

            return new Matrix4x4(values);
        }

        public Matrix4x4[] GetMatrices(string name, int count)
        {
            int location = GetUniformLocation(name);

            Matrix4x4[] matrices = new Matrix4x4[count];

            for (int i = 0; i < count; i++)
            {
                float[] values = new float[16];
                gl.GetUniform(programID, location, values);

                matrices[i] = new Matrix4x4(values);

                location++;
            }

            return matrices;
        }

        public Color GetColor(string name)
        {
            int location = GetUniformLocation(name);

            float[] values = new float[4];
            gl.GetUniform(programID, location, values);

            return new Color(values[0], values[1], values[2], values[3]);
        }

        // TODO GetTexture

        #endregion

        /// <summary>Caches the locations of all uniform variables in the shader program.</summary>
        /// <param name="shaderSource">The source code of the shaders.</param>
        void CacheUniforms(string[] shaderSource)
        {
            List<string> uniformNames = ExtractUniformNames(shaderSource);

            foreach (string name in uniformNames)
            {
                int location = gl.GetUniformLocation(programID, name);

                if (location != -1)
                    uniformLocations[name] = location;
            }
        }

        /// <summary>Extracts the names of all uniform variables from the shader source code.</summary>
        /// <param name="shaderSource">The source code of the shaders.</param>
        /// <returns>A list of uniform variable names.</returns>
        List<string> ExtractUniformNames(string[] shaderSource)
        {
            List<string> uniformNames = new List<string>();

            foreach (string source in shaderSource)
            {
                string[] lines = source.Split('\n');

                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();

                    if (trimmedLine.StartsWith("uniform"))
                    {
                        string[] uniformParts = trimmedLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (uniformParts.Length > 2)
                        {
                            string uniformName = uniformParts[2].TrimEnd(';', ' ');
                            uniformNames.Add(uniformName);
                        }
                    }
                }
            }

            return uniformNames;
        }

        #endregion

        #region Debug

        /// <summary>Checks for compilation errors in a shader.</summary>
        /// <param name="shader">The shader to check.</param>
        void CheckCompilationErrors(uint shader)
        {
            gl.GetShader(shader, GLEnum.CompileStatus, out int status);
            if (status != 1)
                throw new Exception("Error compiling shader: " + gl.GetShaderInfoLog(shader));
        }

        /// <summary>Checks for linking errors in the shader program.</summary>
        void CheckLinkingErrors()
        {
            gl.GetProgram(programID, GLEnum.LinkStatus, out int status);
            if (status != 1)
                throw new Exception("Error linking shader program: " + gl.GetProgramInfoLog(programID));
        }

        #endregion

        #region Default Shaders

        const string defaultVertexShaderSource =
            @"
            #version 460 core

            layout (location = 0) in vec3 vPos;
            layout (location = 1) in vec3 vNormal;
            layout (location = 2) in vec2 vTexture;

            uniform mat4 projection;
            uniform mat4 model;
            uniform mat4 view;

            out vec3 fPos;
            out vec3 fNormal;

            void main()
            {
                // Apply transformations
                mat4 modelViewProjection = projection * view * model;
                vec4 worldPosition = model * vec4(vPos, 1.0);

                gl_Position = modelViewProjection * vec4(vPos, 1.0);

                // Pass data to fragment shader
                fPos = vec3(worldPosition);
                fNormal = mat3(transpose(inverse(model))) * vNormal;
            }
            ";

        const string defaultFragmentShaderSource = @"
            #version 460 core

            in vec3 fPos;
            in vec3 fNormal;

            uniform vec4 mColor;

            uniform vec4 lightColor; // Get from global light
            uniform vec3 lightPos; // Get from global light
            uniform float lightAmbientStrength; // Get from global light
            uniform float lightSpecularStrength; // Get from global light

            uniform vec3 viewPos; // Get from Camera

            out vec4 FragColor;

            void main()
            {
                vec3 ambient = lightAmbientStrength * vec3(lightColor.x, lightColor.y, lightColor.z);

                vec3 norm = normalize(fNormal);
                vec3 lightDirection = normalize(lightPos - fPos);

                float diff = max(dot(norm, lightDirection), 0);
                vec3 diffuse = diff * vec3(lightColor.x, lightColor.y, lightColor.z);

                vec3 viewDirection = normalize(viewPos - fPos);
                vec3 reflectDirection = reflect(-lightDirection, norm);
                float spec = pow(max(dot(viewDirection, reflectDirection), 0), 32);
                vec3 specular = lightSpecularStrength * spec * vec3(lightColor.x, lightColor.y, lightColor.z);

                vec3 result = (ambient + diffuse + specular) * vec3(mColor.x, mColor.y, mColor.z);

                FragColor = vec4(result, 1.0);
            }
            ";

        #endregion
    }
}
