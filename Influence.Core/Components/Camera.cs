
namespace Influence
{
    /// <summary>
    /// Represents a camera component in the Influence framework. <br/>
    /// Cameras are responsible for rendering the game world from a specific perspective.
    /// </summary>
    public class Camera : Component
    {
        /// <summary>Static reference to the main camera used for rendering.</summary>
        public static Camera mainCamera;

        /// <summary>Checks if a main camera is present.</summary>
        public static bool CanRender => mainCamera != null;

        // TODO cache the viewMatrix and only update it if we need to.

        /// <summary>View matrix of the camera, based on the position, forward direction, and up vector.</summary>
        public Matrix4x4 viewMatrix => Matrix4x4.CreateLookAt(transform.position, transform.position + transform.forward, transform.up);

        Matrix4x4 _projectionMatrix = Matrix4x4.Identity;
        /// <summary>Projection matrix of the camera, calculated based on the field of view and clipping planes.</summary>
        public Matrix4x4 projectionMatrix => _projectionMatrix;

        /// <summary>Horizontal angle of view of the camera, in degrees.</summary>
        public float fieldOfView = 60f;

        /// <summary>Near clipping plane distance, beyond which objects won't be rendered.</summary>
        public float nearClippingPlane = 0.1f;

        /// <summary>Far clipping plane distance, beyond which objects won't be rendered.</summary>
        public float farClippingPlane = 1000f;

        /// <summary>Constructs a new camera component.</summary>
        public Camera()
        {
            if (mainCamera == null)
                mainCamera = this;

            UpdateProjectionMatrix();
        }

        /// <summary>Constructs a new camera component with specified field of view and clipping planes.</summary>
        /// <param name="fieldOfView">Horizontal angle of view, in degrees.</param>
        /// <param name="nearClippingPlane">Near clipping plane distance.</param>
        /// <param name="farClippingPlane">Far clipping plane distance.</param>
        public Camera(float fieldOfView = 60f,  float nearClippingPlane = 0.1f, float farClippingPlane = 1000f) : this()
        {
            if (mainCamera == null)
                mainCamera = this;

            this.fieldOfView = fieldOfView;

            this.nearClippingPlane = nearClippingPlane;
            this.farClippingPlane = farClippingPlane;
        }

        /// <summary>Updates the projection matrix based on the camera's field of view and clipping planes.</summary>
        void UpdateProjectionMatrix()
        {
            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(fieldOfView, 1f, nearClippingPlane, farClippingPlane);
        }
    }
}
