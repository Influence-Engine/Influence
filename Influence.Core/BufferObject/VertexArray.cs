using Silk.NET.OpenGL;

namespace Influence
{
    /// <summary>Represents a vertex array object in OpenGL, used for storing vertex attribute arrays.</summary>
    public class VertexArray
    {
        /// <summary>Unique identifier for the vertex array.</summary>
        public uint id { get; private set; }

        /// <summary>Initializes a new instance of the VertexArray class.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        public unsafe VertexArray(GL gl)
        {
            id = gl.GenVertexArray();
            gl.BindVertexArray(id);
        }

        /// <summary>Binds the vertex array to the specified OpenGL context.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        public void Bind(GL gl) => gl.BindVertexArray(id);

        /// <summary>Unbinds the vertex array from the specified OpenGL context.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        public void Unbind(GL gl) => gl.BindVertexArray(0);

        /// <summary>Deletes the vertex array from the specified OpenGL context.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        public void Dispose(GL gl) => gl.DeleteVertexArray(id);

    }
}
