using Silk.NET.OpenGL;
using System;

namespace Influence
{
    /// <summary>Represents an element buffer object in OpenGL, used for storing index data.</summary>
    ///     /// <typeparam name="T">The type of the indices stored in the buffer.</typeparam>
    public class ElementBuffer<T> where T : struct
    {
        /// <summary>Unique identifier for the element buffer.</summary>
        public uint id { get; private set; }

        /// <summary>Number of indices in the buffer.</summary>
        public int indexCount { get; private set; }

        /// <summary>Initializes a new instance of the ElementBuffer class.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        /// <param name="indices">Span of indices to store in the buffer.</param>
        public unsafe ElementBuffer(GL gl, Span<T> indices)
        {
            id = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, id);

            fixed (void* i = indices)
            {
                gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(indices.Length * sizeof(T)), i, BufferUsageARB.StaticDraw);
            }

            indexCount = indices.Length;
        }

        /// <summary>Binds the element buffer to the specified OpenGL context.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        public void Bind(GL gl) => gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, id);

        /// <summary>Unbinds the element buffer from the specified OpenGL context.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        public void Unbind(GL gl) => gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);

        /// <summary>Deletes the element buffer from the specified OpenGL context.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        public void Dispose(GL gl) => gl.DeleteBuffer(id);

    }
}
