using Influence.Core;

using Silk.NET.OpenGL;
using System.Runtime.InteropServices;

namespace Influence
{
    /// <summary>
    /// Represents a buffer object that encapsulates a vertex array object (VAO), a vertex buffer object (VBO), and an element buffer object (EBO).
    /// </summary>
    /// <typeparam name="TVertexType">The type of the vertices stored in the VBO.</typeparam>
    /// <typeparam name="TIndexType">The type of the indices stored in the EBO.</typeparam>
    public class BufferObject<TVertexType, TIndexType> where TVertexType : struct where TIndexType : struct
    {
        GL gl;
        /// <summary>The OpenGL context.</summary>
        public GL OpenGL => gl;

        /// <summary>The vertex array object (VAO).</summary>
        public VertexArray vao;

        /// <summary>The vertex buffer object (VBO).</summary>
        public VertexBuffer<TVertexType> vbo;

        /// <summary>The element buffer object (EBO).</summary>
        public ElementBuffer<TIndexType> ebo;

        /// <summary>Initializes a new instance of the BufferObject class. </summary>
        /// <param name="vertices">Array of vertices to store in the VBO.</param>
        /// <param name="indices">Array of indices to store in the EBO.</param>
        public BufferObject(TVertexType[] vertices, TIndexType[] indices)
        {
            gl = GLContext.OpenGL;

            vao = new VertexArray(gl);
            vbo = new VertexBuffer<TVertexType>(gl, vertices);
            ebo = new ElementBuffer<TIndexType>(gl, indices);
        }

        /// <summary>Binds the VAO, VBO, and EBO to the OpenGL context.</summary>
        public void Bind()
        {
            vao.Bind(gl);
            vbo.Bind(gl);
            ebo.Bind(gl);
        }

        /// <summary>Unbinds the VAO, VBO, and EBO from the OpenGL context.</summary>
        public void Unbind()
        {
            vao.Unbind(gl);
            vbo.Unbind(gl);
            ebo.Unbind(gl);
        }

        /// <summary>Disposes of the VAO, VBO, and EBO from the OpenGL context.</summary>
        public void Dispose()
        {
            vao.Dispose(gl);
            vbo.Dispose(gl);
            ebo.Dispose(gl);
        }

        /// <summary>Enables a vertex attribute array.</summary>
        /// <param name="index">Index of the vertex attribute array to enable.</param>
        public void EnableVertexAttribArray(uint index) => OpenGL.EnableVertexAttribArray(index);

        /// <summary>Disables a vertex attribute array.</summary>
        /// <param name="index">Index of the vertex attribute array to disable.</param>
        public void DisableVertexAttribArray(uint index) => OpenGL.DisableVertexAttribArray(index);

        /// <summary>Specifies the format of the vertex data.</summary>
        /// <param name="index">Index of the vertex attribute array.</param>
        /// <param name="size">Number of components per vertex attribute.</param>
        /// <param name="offset"></param>
        public unsafe void VertexAttribPointer(uint index, int size, int offset = 0)
        {
            OpenGL.VertexAttribPointer(index, size, VertexAttribPointerType.Float, false, (uint)sizeof(TVertexType), (void*)offset);
        }

        /// <summary>Draws primitives from element data.</summary>
        /// <param name="indexCount">Number of indices to render.</param>
        public unsafe void DrawElements(uint indexCount)
        {
            OpenGL.DrawElements(PrimitiveType.Triangles, indexCount, DrawElementsType.UnsignedInt, null);
        }
    }
}
