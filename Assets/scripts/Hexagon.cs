using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Hexagon
{
  List<Vector3> corners;
  Vector3 center;
  //   Face[] faces;
  public List<Vector3> faces;

  public Hexagon(Vector3 center, Vector3[] triCenters)
  {
    //   faces = new Face[6];
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



  //   private struct HexFace
  //   {
  //     Vector3 v1;
  //     Vector3 v2;
  //     Vector3 v3;

  //     public HexFace(Vector3 v1, Vector3 v2, Vector3 v3)
  //     {
  //       this.v1 = v1;
  //       this.v2 = v2;
  //       this.v3 = v3;
  //     }
  //   }
}
