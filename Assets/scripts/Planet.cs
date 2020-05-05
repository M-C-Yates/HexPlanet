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
  // Hexagon[] hexagons;
  // Pentagon[] pentagons;



  void Awake()
  {
    meshFilter = GetComponent<MeshFilter>();

    Generate();
  }


  public void Generate()
  {
    meshData = icoSphere.Generate(subdivisionLevel);
    GenerateHexs();


    meshFilter.mesh = meshData.mesh;
  }

  public void GenerateHexs()
  {
    List<Vector3> newVerts = new List<Vector3>();
    List<int> newTris = new List<int>();

    Vector3[] vertices = meshData.mesh.vertices;
    hexagons = new List<Hexagon>();
    pentagons = new List<Pentagon>();
    // hexagons = new Hexagon[vertices.Length];
    // pentagons = new Pentagon[12];
    int pentagonIndex = 0;

    for (int i = 0; i < vertices.Length; i++)
    {
      List<Vector3> adjTrisCenters = new List<Vector3>();
      for (int j = 0; j < meshData.faces.Count; j++)
      {
        if (meshData.faces[j].vertices.Contains(vertices[i]))
        {
          adjTrisCenters.Add(meshData.faces[j].center);
        }
      }
      // hexagons.Add(new Hexagon(meshData.mesh.vertices[i], adjTrisCenters.ToArray()));
      if (adjTrisCenters.Count == 6)
      {
        hexagons.Add(new Hexagon(meshData.mesh.vertices[i], adjTrisCenters.ToArray()));

        // hexagons[i] = (new Hexagon(vertices[i], adjTrisCenters.ToArray()));
      }
      else if (adjTrisCenters.Count == 5)
      {
        pentagons.Add(new Pentagon(vertices[i], adjTrisCenters.ToArray()));
        // pentagons[pentagonIndex] = new Pentagon(vertices[i], adjTrisCenters.ToArray());
        pentagonIndex++;
      }
    }

    for (int i = 0; i < pentagons.Count; i++)
    {
      newVerts.AddRange(pentagons[i].faces);
      for (int j = 0; j < pentagons[i].faces.Count; j++)
      {
        newTris.Add(newVerts.IndexOf(pentagons[i].faces[j]));
      }
    }
    for (int i = 0; i < hexagons.Count; i++)
    {
      // newVerts.AddRange(hexagons[i].faces);
      for (int j = 0; j < hexagons[i].faces.Count; j++)
      {
        // newTris.Add(newVerts.IndexOf(hexagons[i].faces[j]));
      }
    }

    meshData.mesh.vertices = newVerts.ToArray();
    meshData.mesh.triangles = newTris.ToArray();
    meshData.mesh.RecalculateNormals();
  }
}
