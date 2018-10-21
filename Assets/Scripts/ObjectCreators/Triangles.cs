using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public static class Triangles
    {
        /// <summary>
        /// Function to generate all the Triangles for this face.
        /// </summary>
        /// <returns></returns>
        public static int[] Create(int resolution)
        {
            // Init a list to temporarily store the triangles indices.
            var triangles = new List<int>();

            // Iterate over each vertex in the mesh.
            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    if (x != resolution - 1 && y != resolution - 1)
                    {
                        var currentIndex = x + y * resolution;

                        // 1 -- x
                        // |    |
                        // 3 -- 2
                        triangles.Add(currentIndex);
                        triangles.Add(currentIndex + resolution + 1);
                        triangles.Add(currentIndex + resolution);

                        // 1 -- 2
                        // |    |
                        // x -- 3
                        triangles.Add(currentIndex);
                        triangles.Add(currentIndex + 1);
                        triangles.Add(currentIndex + resolution + 1);
                    }
                }
            }

            if (triangles.Count != (resolution - 1) * (resolution - 1) * 6)
                throw new InvalidOperationException(string.Format("The number of triangles is wrong! It is {0} but it should be {1}", triangles.Count, (resolution - 1) * (resolution - 1) * 6));

            return triangles.ToArray();
        }
    }
}
