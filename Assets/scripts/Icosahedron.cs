using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Icosahedron
{

  List<Vector3> vertices = new List<Vector3>();
  List<int> triangles = new List<int>();
  List<Face> faces = new List<Face>();
  Vector3[] pentagonVerts = new Vector3[12];

  float phi = 1.618034f; // golden ratio

  Dictionary<int, int> cache = new Dictionary<int, int>();

  Mesh mesh;

  Vector3 northPole;


  public MeshData Generate(int subdivisionLevel)
  {
    vertices.Clear();
    triangles.Clear();
    faces.Clear();

    mesh = new Mesh();
    mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

    GenerateIcosahedron(subdivisionLevel);

    mesh.vertices = vertices.ToArray();

    mesh.triangles = triangles.ToArray();
    mesh.RecalculateNormals();
    mesh.Optimize();

    MeshData meshData = new MeshData(faces, pentagonVerts, mesh);
    return meshData;
  }

  private void GenerateIcosahedron(int subdivisionLevel)
  {
    CreateIcoVertices();
    CreateIcoFaces();

    SubDivideMesh(subdivisionLevel);

    PopulateTriangles(faces);
  }

  private void CreateIcoVertices()
  {
    // create the 12 vertices of the icosahedron
    northPole = new Vector3(0, phi, 0).normalized;

    vertices.Add(new Vector3(-1, phi, 0).normalized);
    vertices.Add(new Vector3(1, phi, 0).normalized);
    vertices.Add(new Vector3(-1, -phi, 0).normalized);
    vertices.Add(new Vector3(1, -phi, 0).normalized);

    vertices.Add(new Vector3(0, -1, phi).normalized);
    vertices.Add(new Vector3(0, 1, phi).normalized);
    vertices.Add(new Vector3(0, -1, -phi).normalized);
    vertices.Add(new Vector3(0, 1, -phi).normalized);

    vertices.Add(new Vector3(phi, 0, -1).normalized);
    vertices.Add(new Vector3(phi, 0, 1).normalized);
    vertices.Add(new Vector3(-phi, 0, -1).normalized);
    vertices.Add(new Vector3(-phi, 0, 1).normalized);

    pentagonVerts = vertices.ToArray();
  }

  private void CreateIcoFaces()
  {
    //   create 20 faces of icosahedron
    CreateFace(0, 11, 5, faces);
    CreateFace(0, 5, 1, faces);
    CreateFace(0, 1, 7, faces);
    CreateFace(0, 7, 10, faces);
    CreateFace(0, 10, 11, faces);

    CreateFace(1, 5, 9, faces);
    CreateFace(5, 11, 4, faces);
    CreateFace(11, 10, 2, faces);
    CreateFace(10, 7, 6, faces);
    CreateFace(7, 1, 8, faces);

    CreateFace(3, 9, 4, faces);
    CreateFace(3, 4, 2, faces);
    CreateFace(3, 2, 6, faces);
    CreateFace(3, 6, 8, faces);
    CreateFace(3, 8, 9, faces);

    CreateFace(4, 9, 5, faces);
    CreateFace(2, 4, 11, faces);
    CreateFace(6, 2, 10, faces);
    CreateFace(8, 6, 7, faces);
    CreateFace(9, 8, 1, faces);

  }

  private void SubDivideMesh(int subdivisionLevel)
  {
    if (subdivisionLevel == 0)
    {
      return;
    }

    for (int i = 0; i < subdivisionLevel; i++)
    {
      List<Face> faces2 = new List<Face>();
      foreach (var tri in faces)
      {
        int a = tri.v1;
        int b = tri.v2;
        int c = tri.v3;

        // use GetMidPointIndex to either create or retrieve a vertex between the two old vertices
        int ab = GetMidPointIndex(cache, a, b);
        int bc = GetMidPointIndex(cache, b, c);
        int ca = GetMidPointIndex(cache, c, a);

        // create the four new polygons using new vertices
        CreateFace(a, ab, ca, faces2);
        CreateFace(b, bc, ab, faces2);
        CreateFace(c, ca, bc, faces2);
        CreateFace(ab, bc, ca, faces2);
      }
      faces = faces2;
    }
  }

  private void CreateFace(int v1, int v2, int v3, List<Face> facesList)
  {
    Vector3 center = (vertices[v1] + vertices[v2] + vertices[v3]) / 3f;

    facesList.Add(new Face(v1, v2, v3, center.normalized));
  }

  private int GetMidPointIndex(Dictionary<int, int> cache, int v1, int v2)
  {
    /* 
    create a key out of two original vertice indices,
    by storing the smaller index in the upper two bytes of an int,
    and the larger index in the lower 2 bytes  of the same int
    */
    int smallerIndex = Mathf.Min(v1, v2);
    int greaterIndex = Mathf.Max(v1, v2);
    int key = (smallerIndex << 16) + greaterIndex;
    // return midpoint if it already exists
    int cachedIndex;
    if (cache.TryGetValue(key, out cachedIndex))
    {
      return cachedIndex;
    }

    // calculate midpoint if it doesn't already exist
    Vector3 pointA = vertices[v1];
    Vector3 pointB = vertices[v2];

    Vector3 middle = Vector3.Lerp(pointA, pointB, 0.5f).normalized;

    cachedIndex = vertices.Count;

    vertices.Add(middle);
    cache.Add(key, cachedIndex);
    return cachedIndex;
  }

  private void PopulateTriangles(List<Face> faceList)
  {
    triangles.Clear();
    for (int i = 0; i < faceList.Count; i++)
    {
      triangles.Add(faceList[i].v1);
      triangles.Add(faceList[i].v2);
      triangles.Add(faceList[i].v3);
    }
  }
}

public struct Face
{
  public int v1;
  public int v2;
  public int v3;
  public Vector3 center;

  public Face(int v1, int v2, int v3, Vector3 center) : this()
  {
    this.v1 = v1;
    this.v2 = v2;
    this.v3 = v3;
    this.center = center;
  }
}
public struct MeshData
{
  public List<Face> faces;
  public Vector3[] pentagons;
  public Mesh mesh;

  public MeshData(List<Face> faces, Vector3[] pentagons, Mesh mesh)
  {
    this.faces = faces;
    this.pentagons = pentagons;
    this.mesh = mesh;
  }
}