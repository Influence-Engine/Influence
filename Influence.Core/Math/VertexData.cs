using System.Runtime.InteropServices;

namespace Influence
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexData
    {
        public Vector3 vertices = Vector3.Zero;
        public Vector3 normals = Vector3.Zero;
        public Vector2 textureCoordinates = Vector2.Zero;

        public VertexData(Vector3 vertices)
        {
            this.vertices = vertices;
        }

        public VertexData(Vector3 vertices, Vector3 normals)
        {
            this.vertices = vertices;
            this.normals = normals;
        }

        public VertexData(Vector3 vertices, Vector3 normals, Vector2 textureCoordinate)
        {
            this.vertices = vertices;
            this.normals = normals;
            this.textureCoordinates = textureCoordinate;
        }
    }
}
