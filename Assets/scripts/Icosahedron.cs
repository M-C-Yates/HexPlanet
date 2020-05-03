using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class Icosahedron : MonoBehaviour
{
  public enum Shape
  {
    Icosahedron,
    Octahedron
  };

  public Shape sphereShape;

  public int subdivisionLevel = 0;

  List<Vector3> vertices = new List<Vector3>();
  List<int> triangles = new List<int>();
  List<Face> faces = new List<Face>();

  MeshFilter meshFilter;

  Mesh mesh;


  float phi = 1.618034f; // golden ratio
  Vector3 northPole;

  void Awake()
  {
    meshFilter = GetComponent<MeshFilter>();
    mesh = new Mesh();
    mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

    Generate();
  }

  private void Generate()
  {
    vertices.Clear();
    triangles.Clear();
    faces.Clear();

    if (sphereShape == Shape.Icosahedron)
    {
      GenerateIcosahedron();
    }
    else if (sphereShape == Shape.Octahedron)
    {
      GenerateOctahedron();
    }

    mesh.vertices = vertices.ToArray();
    mesh.triangles = triangles.ToArray();
    mesh.RecalculateNormals();

    meshFilter.mesh = mesh;
  }

  private void GenerateIcosahedron()
  {
    CreateIcoVertices();
    CreateIcoFaces();
    SubDivideMesh(subdivisionLevel);
  }

  private void CreateIcoVertices()
  {
    // create the 12 vertices of the icosahedron
    northPole = new Vector3(0, phi, 0);

    vertices.Add(new Vector3(-1, phi, 0));
    vertices.Add(new Vector3(1, phi, 0));
    vertices.Add(new Vector3(-1, -phi, 0));
    vertices.Add(new Vector3(1, -phi, 0));

    vertices.Add(new Vector3(0, -1, phi));
    vertices.Add(new Vector3(0, 1, phi));
    vertices.Add(new Vector3(0, -1, -phi));
    vertices.Add(new Vector3(0, 1, -phi));

    vertices.Add(new Vector3(phi, 0, -1));
    vertices.Add(new Vector3(phi, 0, 1));
    vertices.Add(new Vector3(-phi, 0, -1));
    vertices.Add(new Vector3(-phi, 0, 1));
  }

  private void CreateIcoFaces()
  {
    // create 20 faces of icosahedron
    CreateFace(0, 11, 5);
    CreateFace(0, 5, 1);
    CreateFace(0, 1, 7);
    CreateFace(0, 7, 10);
    CreateFace(0, 10, 11);

    CreateFace(1, 5, 9);
    CreateFace(5, 11, 4);
    CreateFace(11, 10, 2);
    CreateFace(10, 7, 6);
    CreateFace(7, 1, 8);

    CreateFace(3, 9, 4);
    CreateFace(3, 4, 2);
    CreateFace(3, 2, 6);
    CreateFace(3, 6, 8);
    CreateFace(3, 8, 9);

    CreateFace(4, 9, 5);
    CreateFace(2, 4, 11);
    CreateFace(6, 2, 10);
    CreateFace(8, 6, 7);
    CreateFace(9, 8, 1);

  }

  private void GenerateOctahedron()
  {
    CreateOctaVertices();
    CreateOctaFaces();
  }

  private void CreateOctaVertices()
  {
    vertices.Add(Vector3.down);
    vertices.Add(Vector3.forward);
    vertices.Add(Vector3.left);
    vertices.Add(Vector3.back);
    vertices.Add(Vector3.right);
    vertices.Add(Vector3.up);
  }

  private void CreateOctaFaces()
  {
    CreateFace(0, 1, 2);
    CreateFace(0, 2, 3);
    CreateFace(0, 3, 4);
    CreateFace(0, 4, 1);

    CreateFace(5, 2, 1);
    CreateFace(5, 3, 2);
    CreateFace(5, 4, 3);
    CreateFace(5, 1, 4);
  }

  private void CreateFace(int v1, int v2, int v3)
  {
    triangles.Add(v1);
    triangles.Add(v2);
    triangles.Add(v3);
    faces.Add(new Face(v1, v2, v3));
  }

  private void SubDivideMesh(int subdivisionLevel)
  {
    if (subdivisionLevel == 0)
    {
      return;
    }


  }

  private struct Face
  {
    int v1;
    int v2;
    int v3;

    public Face(int v1, int v2, int v3)
    {
      this.v1 = v1;
      this.v2 = v2;
      this.v3 = v3;
    }
  }

}