using System;
using System.Collections.Generic;
using Influence.Core;

namespace Influence
{
    /// <summary>Base class for all objects that interact with Influence.</summary>
    public class GameObject : Object
    {
        /// <summary>Gets or sets the tag of the GameObject.</summary>
        public string tag = "Default";
        /// <summary>Gets or sets the layer of the GameObject.</summary>
        public int layer = 0;

        // Indicates whether the GameObject is enabled.
        bool enabled = true;

        /// <summary>Returns true of the GameObject is active.</summary>
        public bool activeSelf => enabled;

        /// <summary>Sets the active state of the GameObject.</summary>
        /// <param name="value">True to enable the GameObject; otherwise, false.</param>
        public void SetActive(bool value) => enabled = value;

        // Publicly accesible transform component
        Transform _transform;
        /// <summary>The transform of the gameObject.</summary>
        public Transform transform => _transform;

        // Component List
        List<Component> components = new List<Component>();
        internal List<Component> Components => components;

        /// <summary>Initializes the gameObject.</summary>
        public GameObject()
        {
            _transform = AddComponent<Transform>();
        }

        /// <summary>Initializes the gameObject with a given name.</summary>
        public GameObject(string name) : this()
        {
            this.name = name;
        }

        #region Components

        /// <summary>Adds a component to the gameObject.</summary>
        public Component AddComponent(Component component)
        {
            component.gameObject = this;
            components.Add(component);

            // Call Start On Component
            if (component is Singularity singularity)
            {
                singularity.Start(); // First call Start then add it to the registry
                CreationRegistry.PushSingularity(singularity);
            }

            if (component is IRenderable renderable)
            {
                CreationRegistry.PushRenderable(renderable);
            }

            return component;
        }

        /// <summary>Adds a component to the gameObject.</summary>
        public T AddComponent<T>() where T : Component
        {
            T comp = Activator.CreateInstance<T>();
           AddComponent(comp);

            return comp;
        }

        /// <summary>Searches for a component of a specific type and returns it if found.</summary>
        public T GetComponent<T>() where T : Component
        {
            foreach(Component comp in components)
            {
                if (comp is T type)
                    return type;
            }

            return default;
        }

        #endregion

        /// <summary>Destroys the gameObject.</summary>
        public void DestroySelf() => CreationRegistry.PopGameObject(this);

        ~GameObject()
        {
            DestroySelf();
        }
    }
}
