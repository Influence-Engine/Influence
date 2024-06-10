using Influence;
using Influence.Graphics;

/// <summary>
/// Represents a mesh object that can be rendered. 
/// <br/>
/// It manages the vertices, indices, and material properties required for rendering.
/// </summary>
public class Mesh : Component, IRenderable
{
    BufferObject<VertexData, uint> bufferObject;

    /// <summary>Gets the total count of vertices in the mesh.</summary>
    public uint vertexCount => (uint)bufferObject.vbo.vertexCount;

    /// <summary>Gets the total count of indices in the mesh.</summary>
    public uint indexCount => (uint)bufferObject.ebo.indexCount;

    Material _material;
    /// <summary>Gets the material assigned to the mesh.</summary>
    public Material material => _material;

    /// <summary>Initializes a new instance of the Mesh class with specified vertices, indices, and material.</summary>
    /// <param name="vertexData">The array of vertex data defining the shape of the mesh.</param>
    /// <param name="indices">The array of indices specifying how vertices connect to form triangles.</param>
    /// <param name="material">The material to apply to the mesh.</param>
    public unsafe Mesh(VertexData[] vertexData, uint[] indices, Material material)
    {
        _material = material;

        bufferObject = new BufferObject<VertexData, uint>(vertexData, indices);

        bufferObject.VertexAttribPointer(0,3, 0); // Vertices
        bufferObject.EnableVertexAttribArray(0);

        bufferObject.VertexAttribPointer(1, 3, sizeof(Vector3)); // Normal
        bufferObject.EnableVertexAttribArray(1);

        bufferObject.VertexAttribPointer(2, 2, sizeof(Vector3) * 2); // Texture Coordinate
        bufferObject.EnableVertexAttribArray(2);

        bufferObject.Unbind();
    }

    /// <summary>Initializes a new instance of the Mesh class with specified vertices and indices, defaulting to a new Material.</summary>
    /// <param name="vertexData">The array of vertex data defining the shape of the mesh.</param>
    /// <param name="indices">The array of indices specifying how vertices connect to form triangles.</param>
    public Mesh(VertexData[] vertexData, uint[] indices) : this(vertexData, indices, new Material()) { }

    /// <summary>Creates a new instance of the Mesh class with specified vertices and indices, defaulting to a new Material.</summary>
    /// <param name="vertices">The array of vertices defining the shape of the mesh.</param>
    /// <param name="indices">The array of indices specifying how vertices connect to form triangles.</param>
    public static Mesh CreateFromVertices(Vector3[] vertices, uint[] indices)
    {
        VertexData[] vertexData = new VertexData[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
            vertexData[i] = new VertexData(vertices[i]);

        return new Mesh(vertexData, indices, new Material());
    }

    /// <summary>Renders the mesh.</summary>
    public unsafe void Render()
    {
        _material.Use();

        _material.SetMatrix4("model", transform.worldMatrix);

        _material.SetMatrix4("view", Camera.mainCamera.viewMatrix);

        _material.SetMatrix4("projection", Camera.mainCamera.projectionMatrix);

        _material.SetColor("lightColor", GlobalLight.color);
        _material.SetVector3("lightPos", GlobalLight.position);
        _material.SetFloat("lightAmbientStrength", GlobalLight.ambientStrength);
        _material.SetFloat("lightSpecularStrength", GlobalLight.specularStrength);

        _material.SetVector3("viewPos", Camera.mainCamera.transform.position);

        bufferObject.Bind();
        bufferObject.DrawElements(indexCount);

        bufferObject.Unbind();
    }

    /// <summary>Disposes of the mesh resources, including the buffer object and material.</summary>
    public void Dispose()
    {
        bufferObject.Dispose();
        material.Dispose();
    }

    // Finalizes the mesh object, calling Dispose() to release resources.
    ~Mesh()
    {
        Dispose();
    }
}