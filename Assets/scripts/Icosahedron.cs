using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]

public class Icosahedron : MonoBehaviour
{
  // Start is called before the first frame update
  List<Vector3> vertices = new List<Vector3>();
  List<int> triangles = new List<int>();
  MeshFilter meshFilter;

  float phi = 1.618034f; // golden ratio
  Vector3 northPole;

  void Awake()
  {
    meshFilter = GetComponent<MeshFilter>();
    meshFilter.mesh = new Mesh();

    GenerateIcosahedron();

  }

  public void GenerateIcosahedron()
  {

    CreateVertices();
    CreateFaces();

    meshFilter.mesh.vertices = vertices.ToArray();
    meshFilter.mesh.triangles = triangles.ToArray();
    meshFilter.mesh.RecalculateNormals();
  }

  private void CreateVertices()
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

  private void CreateFaces()
  {
    // create 20 faces of icosahedron
    CreateTriangle(0, 11, 5);
    CreateTriangle(0, 5, 1);
    CreateTriangle(0, 1, 7);
    CreateTriangle(0, 7, 10);
    CreateTriangle(0, 10, 11);

    CreateTriangle(1, 5, 9);
    CreateTriangle(5, 11, 4);
    CreateTriangle(11, 10, 2);
    CreateTriangle(10, 7, 6);
    CreateTriangle(7, 1, 8);

    CreateTriangle(3, 9, 4);
    CreateTriangle(3, 4, 2);
    CreateTriangle(3, 2, 6);
    CreateTriangle(3, 6, 8);
    CreateTriangle(3, 8, 9);

    CreateTriangle(4, 9, 5);
    CreateTriangle(2, 4, 11);
    CreateTriangle(6, 2, 10);
    CreateTriangle(8, 6, 7);
    CreateTriangle(9, 8, 1);

  }

  private void CreateTriangle(int v1, int v2, int v3)
  {
    triangles.Add(v1);
    triangles.Add(v2);
    triangles.Add(v3);
  }

}
