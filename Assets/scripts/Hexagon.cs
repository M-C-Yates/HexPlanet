using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Hexagon
{
  List<Vector3> corners;
  public Vector3 center;
  public List<Vector3> faces;

  public Hexagon(Vector3 center, Vector3[] triCenters)
  {
    corners = new List<Vector3>();
    faces = new List<Vector3>();
    this.center = center;
    this.corners.AddRange(triCenters);

    corners.Add(corners[0]);
    Triangulate();
  }

  private void Triangulate()
  {
    for (int i = 0; i < 6; i++)
    {
      //   faces.Add(new HexFace(center, corners[i], corners[i + 1]));
      faces.Add(center);
      faces.Add(corners[i]);
      faces.Add(corners[i + 1]);
    }
  }
}
