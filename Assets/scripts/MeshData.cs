using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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