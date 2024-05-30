using Silk.NET.OpenGL;

namespace Influence
{
    /// <summary>Represents an element buffer object in OpenGL, used for storing index data.</summary>
    public class ElementBuffer
    {
        /// <summary>Unique identifier for the element buffer.</summary>
        public uint id { get; private set; }

        /// <summary>Number of indices in the buffer.</summary>
        public int indexCount { get; private set; }

        /// <summary>Initializes a new instance of the ElementBuffer class.</summary>
        /// <param name="gl">An instance of GL representing the OpenGL context.</param>
        /// <param name="indices">Array of indices to store in the buffer.</param>
        public unsafe ElementBuffer(GL gl, uint[] indices)
        {
            id = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, id);

            fixed (void* i = &indices[0])
            {
                gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(indices.Length * sizeof(uint)), i, BufferUsageARB.StaticDraw);
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
