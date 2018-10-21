using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public static class Vertices
    {
        public static Vector3[] Create(int resolution, Vector3 up, bool spherical = false)
        {
            var vertices = new List<Vector3>();
            var floatResolution = resolution * 1f - 1;
            var localYAxis = up;
            var localXAxis = new Vector3(up.y, up.z, up.x);
            var localZAxis = Vector3.Cross(localYAxis, localXAxis);

            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    var pointOnUnitCube = new Vector3();
                    pointOnUnitCube += localYAxis;
                    pointOnUnitCube += localXAxis * (((x / floatResolution) - .5f) * 2);
                    pointOnUnitCube += localZAxis * (((y / floatResolution) - .5f) * 2);

                    // Normalize the vector so it becomes a sphere.
                    if (spherical)
                        pointOnUnitCube.Normalize();

                    vertices.Add(pointOnUnitCube);
                }
            }

            // Check if we have 
            if (vertices.Count != resolution * resolution)
                throw new InvalidOperationException(string.Format("Number of vertices is wrong! It is {0} but it should be {1}", vertices.Count, resolution * resolution));

            return vertices.ToArray();
        }
    }
}
