using Influence.Core;

using Silk.NET.OpenGL;
using System;
using System.Runtime.InteropServices;

namespace Influence
{
    /// <summary>
    /// Represents a buffer object that encapsulates a vertex array object (VAO), a vertex buffer object (VBO), and an element buffer object (EBO).
    /// </summary>
    /// <typeparam name="T">The type of the vertices stored in the VBO.</typeparam>
    public class BufferObject<T> where T : struct
    {
        GL gl;
        /// <summary>The OpenGL context.</summary>
        public GL OpenGL => gl;

        /// <summary>The vertex array object (VAO).</summary>
        public VertexArray vao;

        /// <summary>The vertex buffer object (VBO).</summary>
        public VertexBuffer<T> vbo;

        /// <summary>The element buffer object (EBO).</summary>
        public ElementBuffer ebo;

        /// <summary>Initializes a new instance of the BufferObject class. </summary>
        /// <param name="vertices">Array of vertices to store in the VBO.</param>
        /// <param name="indices">Array of indices to store in the EBO.</param>
        public BufferObject(T[] vertices, uint[] indices)
        {
            gl = GLContext.OpenGL;

            vao = new VertexArray(gl);
            vbo = new VertexBuffer<T>(gl, vertices);
            ebo = new ElementBuffer(gl, indices);
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
        /// <param name="normalized">Specifies whether fixed-point data values should be normalized or converted directly as fixed-point values when they are accessed.</param>
        /// <param name="stride">Byte offset between consecutive vertex attributes.</param>
        public unsafe void VertexAttribPointer(uint index, int size, bool normalized, uint stride, int offset = 0)
        {
            OpenGL.VertexAttribPointer(index, size, VertexAttribPointerType.Float, normalized, stride, (void*)(offset * size));
        }

        /// <summary>Specifies the format of the vertex data based on the type parameter.</summary>
        /// <param name="index">Index of the vertex attribute array.</param>
        /// <param name="normalized">Specifies whether fixed-point data values should be normalized or converted directly as fixed-point values when they are accessed.</param>
        public unsafe void VertexAttribPointer(uint index, bool normalized, int offset = 0)
        {
            Type type = typeof(T);
            int size = Marshal.SizeOf(type);
            uint stride = (uint)size;

            VertexAttribPointer(index, (size / sizeof(float)), normalized, stride, offset);
        }

        /// <summary>Draws primitives from element data.</summary>
        /// <param name="indexCount">Number of indices to render.</param>
        public unsafe void DrawElements(uint indexCount)
        {
            OpenGL.DrawElements(PrimitiveType.Triangles, indexCount, DrawElementsType.UnsignedInt, null);
        }
    }
}
