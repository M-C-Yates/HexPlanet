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



  void Awake()
  {
    meshFilter = GetComponent<MeshFilter>();

    Generate();
  }


  public void Generate()
  {
    meshData = icoSphere.Generate(subdivisionLevel);

    meshFilter.mesh = meshData.mesh;
  }
}
