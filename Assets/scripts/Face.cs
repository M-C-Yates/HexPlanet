using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Face
{
  public int v1;
  public int v2;
  public int v3;
  public Vector3[] vertices;
  public Vector3 center;

  public Face(int vert1Index, int vert2Index, int vert3Index, Vector3 center, Vector3[] vertices) : this()
  {
    this.vertices = vertices;
    this.center = center;

    this.v1 = vert1Index;
    this.v2 = vert2Index;
    this.v3 = vert3Index;
  }
}