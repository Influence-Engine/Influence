
namespace Influence
{
    /// <summary>
    /// Represents a run component for gameObjects in Influence. <br/>
    /// Provides the usability of lifecycle methods.
    /// </summary>
    public class Singularity : Component
    {
        /// <summary>
        /// Start is called once when the component is added to a gameObject. <br/>
        /// Used for one-time initialization tasks.
        /// </summary>
        public virtual void Start() { }

        /// <summary>Update is called every frame.</summary>
        public virtual void Update() { }

        /// <summary>FixedUpdate is called every fixed frame-rate frame. <br/>
        /// Use FixedUpdate for Physics Calculations.
        /// </summary>
        public virtual void FixedUpdate() { }
    }
}
