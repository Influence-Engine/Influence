
namespace Influence
{
    /// <summary>Transforms define the position, rotation, and scale of gameObjects in the world.</summary>
    public partial class Transform : Component
    {
        Vector3 _position = Vector3.Zero;
        /// <summary>Position of the transform in world space.</summary>
        public Vector3 position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position = value;
                    UpdateModelMatrix();
                }
            }
        }

        Quaternion _rotation = Quaternion.Identity;
        /// <summary>Rotation of the transform.</summary>
        public Quaternion rotation
        {
            get => _rotation;
            set
            {
                if (_rotation != value)
                {
                    _rotation = value;
                    UpdateModelMatrix();
                }
            }
        }

        Vector3 _scale = Vector3.One;
        /// <summary>Scale of the transform.</summary>
        public Vector3 scale
        {
            get => _scale;
            set
            {
                if (_scale != value)
                {
                    _scale = value;
                    UpdateModelMatrix();
                }
            }
        }

        Matrix4x4 modelMatrix = Matrix4x4.Identity;
        /// <summary>World matrix of the transform, combining position, rotation, and scale.</summary>
        public Matrix4x4 worldMatrix => modelMatrix;

        /// <summary>Constructs a new transform component.</summary>
        public Transform()
        {
            UpdateModelMatrix();
        }

        #region Quick Returns

        /// <summary>Euler angles representation of the rotation.</summary>
        public Vector3 eulerAngles
        {
            get => rotation.eulerAngles;
            set => rotation = Quaternion.Euler(value);
        }

        /// <summary>Right vector of the transform.</summary>
        public Vector3 right
        {
            get => rotation * Vector3.Right;
            set => rotation = Quaternion.FromToRotation(Vector3.Right, value);
        }

        /// <summary>Up vector of the transform.</summary>
        public Vector3 up
        {
            get => rotation * Vector3.Up;
            set => rotation = Quaternion.FromToRotation(Vector3.Up, value);
        }

        /// <summary>Forward vector of the transform.</summary>
        public Vector3 forward
        {
            get => rotation * Vector3.Forward;
            set => rotation = Quaternion.FromToRotation(Vector3.Forward, value);
        }

        #endregion

        #region Functions

        /// <summary>Moves the transform by a given translation vector.</summary>
        /// <param name="translation">Translation vector.</param>
        public void Translate(Vector3 translation) => position += translation;
        public void Translate(Vector2 translation) => Translate(new Vector3(translation));
        public void Translate(float x, float y) => Translate(new Vector3(x, y));
        public void Translate(float x, float y, float z) => Translate(new Vector3(x, y, z));

        /// <summary>Rotates the transform by a given Euler angle.</summary>
        /// <param name="euler">Euler angle.</param>
        public void Rotate(Vector3 euler)
        {
            Quaternion eulerRot = Quaternion.Euler(euler);
            this.rotation *= eulerRot;
        }

        /// <summary>Rotates the transform by a given Euler angle.</summary>
        /// <param name="x">Euler X</param>
        /// <param name="y">Euler Y</param>
        /// <param name="z">Euler Z</param>
        public void Rotate(float x, float y, float z) => Rotate(new Vector3(x, y, z));

        /// <summary>Updates the model matrix based on the current position, rotation, and scale.</summary>
        void UpdateModelMatrix()
        {
            Matrix4x4 scaleMatrix = Matrix4x4.Scale(_scale);
            Matrix4x4 rotationMatrix = _rotation.ToRotationMatrix();
            Matrix4x4 translationMatrix = Matrix4x4.Translate(_position);

            modelMatrix = translationMatrix * rotationMatrix * scaleMatrix;
        }

        #endregion
    }
}
