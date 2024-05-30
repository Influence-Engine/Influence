
namespace Influence
{
    /// <summary>
    /// Abstract base class for all components in Influence. <br/>
    /// Components represent reusable pieces of functionality that can be attached to GameObjects.
    /// </summary>
    public abstract class Component : Object
    {
        // Indicates whether the component is currently enabled.
        bool enabled = true;

        /// <summary>Gets whether the component is active, considering both its own and its GameObjects enabled state.</summary>
        public bool activeSelf => enabled && (!Equals(gameObject, null) && gameObject.activeSelf);

        /// <summary>Sets the active state of the component.</summary>
        /// <param name="value">True to enable the component; otherwise, false.</param>
        public void SetActive(bool value) => enabled = value;

        /// <summary>
        /// Default constructor for components. <br/>
        /// Automatically assigns the component's name based on its type.
        /// </summary>
        public Component()
        {
            name = this.GetType().Name;
        }

        /// <summary>Reference to the GameObject this component is attached to.</summary>
        internal GameObject gameObject;

        /// <summary>Gets the Transform component of the GameObject this component is attached to.</summary>
        public Transform transform => gameObject.transform;
    }
}
