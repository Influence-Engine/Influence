using Influence.Window;

using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace Influence.Core
{
    /// <summary>
    /// Manages an OpenGL context associated with a window. <br/>
    /// Allows for the creation and retrieval of OpenGL instances specific to each window, enabling multiple windows to utilize their own OpenGL contexts.
    /// </summary>
    public class GLContext
    {
        // QUESTION Do we need to allow multiple windows? What is the use case. Would achieving this be worth the effort?
        // TODO this should be accesible through the window itself so that every window can have their own GL to use and to allow multiple windows

        /// <summary>The OpenGL API instance managed by this context.</summary>
        static GL gl;

        /// <summary>Accessor for the OpenGL API instance.</summary>
        public static GL OpenGL => gl;

        /// <summary>Initializes a new instance of the GLContext class with a specific window context.</summary>
        /// <param name="windowContext">The window context associated with this OpenGL context.</param>
        public GLContext(WindowContext windowContext)
        {
            gl = GL.GetApi(windowContext.Window);
        }

        public void SetViewport(Vector2Int size) => gl.Viewport(new Vector2D<int>(size.x, size.y));
    }
}
