
namespace Influence
{
    /// <summary>
    /// Interface for components that require rendering capabilities. <br/>
    /// Implementations of this interface must provide a way to draw or display themselves, through the implementation of a Render() method.
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Renders the component according to its rendering requirements. <br/>
        /// This method should contain the logic necessary to visually represent the component.
        /// </summary>
        void Render();
    }
}
