using System.Collections.Generic;

namespace Influence.Core
{
    /// <summary>
    /// A registry for managing the lifecycle of game objects, singularities, and renderables. <br/>
    /// Provides mechanisms to push and pop items from collections and tracking their counts.
    /// </summary>
    public static class CreationRegistry
    {
        // TODO makes list get public but set internal.

        #region GameObjects

        // QUESTION Do we need to keep track of all the gameobjects?
        // All the gameObjects we need to really know about are the ones holding Components.
        // These are registered and will keep them in scope.

        /*
        public static List<GameObject> registeredGameObjects = new List<GameObject>();

        static int gameObjectsCount = 0;
        public static int GameObjectsCount => gameObjectsCount;

        public static void PushGameObject(GameObject gameObject)
        {
            registeredGameObjects.Add(gameObject);
            gameObjectsCount = registeredGameObjects.Count;
        }
        */

        /// <summary>Removes a gameObject from the registry, also removing its components.</summary>
        /// <param name="gameObject">The gameObject to remove.</param>
        public static void PopGameObject(GameObject gameObject)
        {
            for (int i = 0; i < gameObject.Components.Count; i++)
            {
                if (gameObject.Components[i] is Singularity singularity)
                {
                    PopSingularity(singularity);
                }

                if (gameObject.Components[i] is IRenderable renderable)
                {
                    PopRenderable(renderable);
                }
            }

            //registeredGameObjects.Remove(gameObject);

            //gameObjectsCount = registeredGameObjects.Count;
        }

        #endregion

        #region Singularity

        public static List<Singularity> registeredSingularitys = new List<Singularity>();

        static int singularityCount = 0;
        public static int SingularityCount => singularityCount;

        /// <summary>Adds a singularity to the registry.</summary>
        /// <param name="singularity">The singularity to add.</param>
        public static void PushSingularity(Singularity singularity)
        {
            registeredSingularitys.Add(singularity);
            singularityCount = registeredSingularitys.Count;
        }

        /// <summary>Removes a singularity from the registry. </summary>
        /// <param name="singularity">The singularity to remove.</param>
        public static void PopSingularity(Singularity singularity)
        {
            registeredSingularitys.Remove(singularity);
            singularityCount = registeredSingularitys.Count;
        }

        #endregion

        #region Renderable

        public static List<IRenderable> registeredRenderables = new List<IRenderable>();

        static int renderablesCount = 0;
        public static int RenderablesCount => renderablesCount;

        /// <summary>Adds a renderable to the registry.</summary>
        /// <param name="renderable">The renderable to add.</param>
        public static void PushRenderable(IRenderable renderable)
        {
            registeredRenderables.Add(renderable);
            renderablesCount = registeredRenderables.Count;
        }

        /// <summary>Removes a renderable from the registry.</summary>
        /// <param name="renderable">The renderable to remove.</param>
        public static void PopRenderable(IRenderable renderable)
        {
            registeredRenderables.Remove(renderable);
            renderablesCount = registeredRenderables.Count;
        }

        #endregion
    }
}
