
using Influence;

/// <summary>
/// Represents a mesh object that can be rendered. 
/// <br/>
/// It manages the vertices, indices, and material properties required for rendering.
/// </summary>
public class Mesh : Component, IRenderable
{
    BufferObject<Vector3> bufferObject;

    /// <summary>Gets the total count of vertices in the mesh.</summary>
    public uint vertexCount => (uint)bufferObject.vbo.vertexCount;

    /// <summary>Gets the total count of indices in the mesh.</summary>
    public uint indexCount => (uint)bufferObject.ebo.indexCount;

    Material _material;
    /// <summary>Gets the material assigned to the mesh.</summary>
    public Material material => _material;

    /// <summary>Initializes a new instance of the Mesh class with specified vertices, indices, and material.</summary>
    /// <param name="vertices">The array of vertices defining the shape of the mesh.</param>
    /// <param name="indices">The array of indices specifying how vertices connect to form triangles.</param>
    /// <param name="material">The material to apply to the mesh.</param>
    public unsafe Mesh(Vector3[] vertices, uint[] indices, Material material)
    {
        _material = material;

        bufferObject = new BufferObject<Vector3>(vertices, indices);

        bufferObject.VertexAttribPointer(0, false);
        bufferObject.EnableVertexAttribArray(0);

        bufferObject.Unbind();
    }

    /// <summary>Initializes a new instance of the Mesh class with specified vertices and indices, defaulting to a new Material.</summary>
    /// <param name="vertices">The array of vertices defining the shape of the mesh.</param>
    /// <param name="indices">The array of indices specifying how vertices connect to form triangles.</param>
    public Mesh(Vector3[] vertices, uint[] indices) : this(vertices, indices, new Material()) { }

    /// <summary>Renders the mesh.</summary>
    public unsafe void Render()
    {
        if (!Camera.CanRender)
            return;

        _material.Use();

        _material.SetMatrix4("model", transform.worldMatrix);

        _material.SetMatrix4("view", Camera.mainCamera.viewMatrix);

        _material.SetMatrix4("projection", Camera.mainCamera.projectionMatrix);

        // TODO use Light class (Sun)
        _material.SetColor("lightColor", Color.White);
        _material.SetVector3("lightPos", new Vector3(6, 6, 0));

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