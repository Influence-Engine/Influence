using Silk.NET.OpenGL;
using System.Runtime.InteropServices;

namespace Influence
{
    /// <summary>A generic vertex buffer class for OpenGL.</summary>
    /// <typeparam name="T">The type of the vertices stored in the buffer.</typeparam>
    public class VertexBuffer<T> where T : struct
    {
        /// <summary>Unique identifier for the vertex buffer.</summary>
        public uint id { get; private set; }

        /// <summary>Size of each vertex element in bytes.</summary>
        public int elementSize { get; private set;  }

        /// <summary>Number of vertices in the buffer.</summary>
        public int vertexCount { get; private set; }

        /// <summary>Initializes a new instance of the VertexBuffer class.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        /// <param name="vertices">Array of vertices to store in the buffer.</param>
        public unsafe VertexBuffer(GL gl, T[] vertices)
        {
            id = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ArrayBuffer, id);

            vertexCount = vertices.Length;
            elementSize = Marshal.SizeOf<T>();
            fixed (void* v = &vertices[0])
            {
                gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * elementSize), v, BufferUsageARB.StaticDraw);
            }
        }

        /// <summary>Binds the vertex buffer to the specified OpenGL context.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        public void Bind(GL gl) => gl.BindBuffer(BufferTargetARB.ArrayBuffer, id);

        /// <summary>Unbinds the vertex buffer from the specified OpenGL context.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        public void Unbind(GL gl) => gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);

        /// <summary>Deletes the vertex buffer from the specified OpenGL context..</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        public void Dispose(GL gl) => gl.DeleteBuffer(id);

    }
}
