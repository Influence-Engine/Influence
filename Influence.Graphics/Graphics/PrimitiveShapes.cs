using System.Collections.Generic;
using System;

namespace Influence
{
    /// <summary>Provides methods to create basic geometric shapes.</summary>
    public class PrimitiveShapes
    {
        /// <summary>Creates a primitive shape based on the specified type.</summary>
        /// <param name="type">The type of the primitive shape to create.</param>
        /// <returns>A Mesh object representing the created shape.</returns>
        public static Mesh CreatePrimitive(PrimitiveType type)
        {
            switch(type)
            {
                case PrimitiveType.Quad:
                    return CreateQuad();

                case PrimitiveType.Cube: 
                default:
                    return CreateCube();

                case PrimitiveType.Sphere:
                    return CreateSphere();
            }
        }

        /// <summary>Creates a quad (rectangle).</summary>
        /// <returns>A Mesh object representing the quad.</returns>
        public static Mesh CreateQuad()
        {
            VertexData[] vertexData =
            {
                new VertexData(new Vector3(-0.5f, -0.5f, 0.0f), new Vector3(0,0, -1)),  // Bottom-left
                new VertexData(new Vector3(0.5f, -0.5f, 0.0f), new Vector3(0,0, -1)),  // Bottom-right
                new VertexData(new Vector3(0.5f, 0.5f, 0.0f), new Vector3(0,0, -1)),  // Top-right
                new VertexData(new Vector3(-0.5f, 0.5f, 0.0f), new Vector3(0,0, -1))  // Top-left
            };

            uint[] indices =
            {
                0, 1, 2,    // First triangle
                2, 3, 0     // Second triangle
            };

            return new Mesh(vertexData, indices);
        }

        /// <summary>Creates a cube.</summary>
        /// <returns>A Mesh object representing the cube.</returns>
        public static Mesh CreateCube()
        {
            VertexData[] vertexData =
            {
                // Vertices, Normals
                new VertexData(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0,0, -1)),
                new VertexData(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0,0, -1)),
                new VertexData(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0,0, -1)),
                new VertexData(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0,0, -1)),

                new VertexData(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0,0, 1)),
                new VertexData(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0,0, 1)),
                new VertexData(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0,0, 1)),
                new VertexData(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0,0, 1)),
            };

            uint[] indices =
            {
                0, 1, 2, 2, 3, 0,   // Front face
                1, 5, 6, 6, 2, 1,    // Right face
                4, 7, 6, 6, 5, 4,   // Back face
                0, 3, 7, 7, 4, 0,  // Left face
                3, 2, 6, 6, 7, 3,  // Top face
                0, 4, 5, 5, 1, 0    // Bottom face
            };

            return new Mesh(vertexData, indices);
        }

        // QUESTION do we remove the radius for the sphere creation as we handle size through transform.scale?

        /// <summary>
        /// Creates a sphere.
        /// </summary>
        /// <param name="stacks">The number of subdivisions along the vertical axis.</param>
        /// <param name="slices">The number of subdivisions around the circumference.</param>
        /// <param name="radius">The radius of the sphere.</param>
        /// <returns>A Mesh object representing the sphere.</returns>
        public static Mesh CreateSphere(int stacks = 16, int slices = 16, float radius = 1f)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<uint> indices = new List<uint>();

            for (int stack = 0; stack <= stacks; stack++)
            {
                float stackAngle = Mathq.PiOver2 - stack * Mathq.Pi / stacks;
                float y = radius * (float)Math.Sin(stackAngle);
                float scale = -radius * (float)Math.Cos(stackAngle);

                for (int slice = 0; slice <= slices; slice++)
                {
                    float sliceAngle = slice * Mathq.TwoPi / slices;
                    float x = scale * (float)Math.Sin(sliceAngle);
                    float z = scale * (float)Math.Cos(sliceAngle);

                    vertices.Add(new Vector3(x, y, z));
                }
            }

            for (uint stack = 0; stack < stacks; stack++)
            {
                for (uint slice = 0; slice < slices; slice++)
                {
                    uint topLeft = (uint)(stack * (slices + 1) + slice);
                    uint topRight = topLeft + 1;
                    uint bottomLeft = (uint)((stack + 1) * (slices + 1) + slice);
                    uint bottomRight = bottomLeft + 1;

                    indices.Add(topLeft);
                    indices.Add(bottomLeft);
                    indices.Add(topRight);

                    indices.Add(topRight);
                    indices.Add(bottomLeft);
                    indices.Add(bottomRight);
                }
            }

            return Mesh.CreateFromVertices(vertices.ToArray(), indices.ToArray());
        }
    }
}
