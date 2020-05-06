using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Planet : MonoBehaviour
{
  public int subdivisionLevel = 5;

  MeshFilter meshFilter;
  Icosahedron icoSphere = new Icosahedron();

  MeshData meshData;

  List<Hexagon> hexagons;
  List<Pentagon> pentagons;

  List<Vector3> newVerts = new List<Vector3>();
  List<int> newTris = new List<int>();
  List<Vector3> adjTrisCenters = new List<Vector3>();




  void Awake()
  {
    meshFilter = GetComponent<MeshFilter>();

    Generate();
  }


  public void Generate()
  {
    meshData = icoSphere.Generate(subdivisionLevel);
    GeneratePents();


    GenerateHexs();

    meshData.mesh.Clear();

    meshData.mesh.vertices = newVerts.ToArray();
    meshData.mesh.triangles = newTris.ToArray();
    meshData.mesh.RecalculateNormals();

    meshFilter.mesh = meshData.mesh;
  }

  public void GeneratePents()
  {
    pentagons = new List<Pentagon>();

    int pentagonIndex = 0;

    for (int i = 0; i < meshData.mesh.vertices.Length; i++)
    {
      adjTrisCenters.Clear();
      for (int j = 0; j < meshData.faces.Count; j++)
      {
        if (meshData.faces[j].vertices.Contains(meshData.mesh.vertices[i]))
        {
          adjTrisCenters.Add(meshData.faces[j].center);
        }
      }

      if (adjTrisCenters.Count == 5)
      {

        pentagons.Add(new Pentagon(meshData.mesh.vertices[i].normalized, adjTrisCenters.ToArray(), pentagonIndex));
        pentagonIndex++;
      }
    }

    for (int i = 0; i < 12; i++)
    {
      newVerts.AddRange(pentagons[i].faces);
      for (int j = 0; j < pentagons[i].faces.Count; j++)
      {
        newTris.Add(newVerts.IndexOf(pentagons[i].faces[j]));
      }
    }
  }

  public void GenerateHexs()
  {
    hexagons = new List<Hexagon>();

    // TODO make edges of hexagons + pentagons have the same normals as the center
    // TODO make order of edge vertexes the same for every hexagon + pentagon
    for (int i = 0; i < meshData.mesh.vertices.Length; i++)
    {
      adjTrisCenters.Clear();
      for (int j = 0; j < meshData.faces.Count; j++)
      {
        if (meshData.faces[j].vertices.Contains(meshData.mesh.vertices[i]))
        {
          adjTrisCenters.Add(meshData.faces[j].center);
        }
      }
      if (adjTrisCenters.Count == 6)
      {
        hexagons.Add(new Hexagon(meshData.mesh.vertices[i].normalized, adjTrisCenters.ToArray()));
      }
    }

    for (int i = 0; i < hexagons.Count; i++)
    {
      newVerts.AddRange(hexagons[i].faces);
      for (int j = 0; j < hexagons[i].faces.Count; j++)
      {
        newTris.Add(newVerts.IndexOf(hexagons[i].faces[j]));
      }
    }
  }

  private void OnDrawGizmos()
  {
    if (pentagons == null)
    {
      return;
    }
    Gizmos.color = Color.black;
    for (int i = 0; i < pentagons.Count; i++)
    {
      for (int j = 0; j < pentagons[i].faces.Count; j++)
      {
        Gizmos.DrawSphere(pentagons[i].faces[j], 0.01f);
      }
    }
  }
}
