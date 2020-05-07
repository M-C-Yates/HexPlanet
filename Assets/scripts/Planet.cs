using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Planet : MonoBehaviour
{
  public int subdivisionLevel = 5;

  MeshFilter meshFilter;
  [SerializeField] int radius = 2;

  Icosahedron icoSphere = new Icosahedron();

  MeshData meshData;


  void Awake()
  {
    meshFilter = GetComponent<MeshFilter>();

    Generate();
  }


  public void Generate()
  {
    meshData = icoSphere.Generate(subdivisionLevel, radius);
    // GeneratePents();


    // GenerateHexs();

    // meshData.mesh.Clear();

    // meshData.mesh.vertices = newVerts.ToArray();
    // meshData.mesh.triangles = newTris.ToArray();
    meshData.mesh.RecalculateNormals();

    meshFilter.mesh = meshData.mesh;
  }


  // private void OnDrawGizmos()
  // {
  //   if (pentagons == null)
  //   {
  //     return;
  //   }
  //   Gizmos.color = Color.black;
  //   for (int i = 0; i < pentagons.Count; i++)
  //   {
  //     for (int j = 0; j < pentagons[i].faces.Count; j++)
  //     {
  //       Gizmos.DrawSphere(pentagons[i].faces[j], 0.01f);
  //     }
  //   }
  // }
}
