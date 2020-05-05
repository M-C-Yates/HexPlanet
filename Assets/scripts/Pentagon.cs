using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Pentagon
{
  List<Vector3> corners;
  Vector3 center;
  //   Face[] faces;
  public List<Vector3> faces;
  public Vector3[] normals;
  public int pentagonIndex;

  public Pentagon(Vector3 center, Vector3[] triCenters, int pentagonIndex)
  {
    //   faces = new Face[6];
    normals = new Vector3[6];
    corners = new List<Vector3>();
    faces = new List<Vector3>();
    this.center = center;
    this.corners.AddRange(triCenters);
    this.pentagonIndex = pentagonIndex;

    corners.Add(corners[0]);
    Triangulate();
  }

  //used 0,1,2,3,4,5,6,7,8,9,10,11

  private void Triangulate()
  {
    if (pentagonIndex == 0 || pentagonIndex == 9)
    {
      for (int i = 0; i < 5; i++)
      {

        faces.Add(center);
        faces.Add(corners[i]);
        faces.Add(corners[i + 1]);
      }
    }

    if (pentagonIndex == 1 || pentagonIndex == 5)
    {
      faces.Add(center.normalized);
      faces.Add(corners[1].normalized);
      faces.Add(corners[0].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[0].normalized);
      faces.Add(corners[3].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[3].normalized);
      faces.Add(corners[4].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[4].normalized);
      faces.Add(corners[2].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[2].normalized);
      faces.Add(corners[1].normalized);
    }

    if (pentagonIndex == 2)
    {
      faces.Add(center.normalized);
      faces.Add(corners[0].normalized);
      faces.Add(corners[1].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[1].normalized);
      faces.Add(corners[3].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[3].normalized);
      faces.Add(corners[4].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[4].normalized);
      faces.Add(corners[2].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[2].normalized);
      faces.Add(corners[0].normalized);
    }

    if (pentagonIndex == 3 || pentagonIndex == 8 || pentagonIndex == 11 || pentagonIndex == 10)
    {
      faces.Add(center.normalized);
      faces.Add(corners[0].normalized);
      faces.Add(corners[4].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[4].normalized);
      faces.Add(corners[2].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[2].normalized);
      faces.Add(corners[1].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[1].normalized);
      faces.Add(corners[3].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[3].normalized);
      faces.Add(corners[0].normalized);
    }

    if (pentagonIndex == 4)
    {
      faces.Add(center.normalized);
      faces.Add(corners[0].normalized);
      faces.Add(corners[3].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[3].normalized);
      faces.Add(corners[4].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[4].normalized);
      faces.Add(corners[2].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[2].normalized);
      faces.Add(corners[1].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[1].normalized);
      faces.Add(corners[0].normalized);
    }
    if (pentagonIndex == 6)
    {
      faces.Add(center.normalized);
      faces.Add(corners[0].normalized);
      faces.Add(corners[2].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[2].normalized);
      faces.Add(corners[4].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[4].normalized);
      faces.Add(corners[3].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[3].normalized);
      faces.Add(corners[1].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[1].normalized);
      faces.Add(corners[0].normalized);
    }
    // || pentagonIndex == 7 inverse of pentagonIndex == 3
    if (pentagonIndex == 7)
    {
      faces.Add(center.normalized);
      faces.Add(corners[4].normalized);
      faces.Add(corners[0].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[0].normalized);
      faces.Add(corners[3].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[3].normalized);
      faces.Add(corners[1].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[1].normalized);
      faces.Add(corners[2].normalized);

      faces.Add(center.normalized);
      faces.Add(corners[2].normalized);
      faces.Add(corners[4].normalized);
    }
  }
}
