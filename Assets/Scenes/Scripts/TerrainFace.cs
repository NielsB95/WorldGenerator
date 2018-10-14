using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
	/// <summary>
	/// The resolution at which this TerrainFace is created.
	/// </summary>
	private int resolution;

	/// <summary>
	/// This tells us what the local up is for this TerrainFace.
	/// </summary>
	private Vector3 localUp;

	/// <summary>
	/// The TerrainFace can be rotated and therfore the x axis can be different. For normalization
	/// we determine what the local x axis is.
	/// </summary>
	private Vector3 localXAxis;

	/// <summary>
	/// Due to the rotation of the TerrainFace the Z axis can be altered. For normalization
	/// we determine what the local z axis is.
	/// </summary>
	private Vector3 localZAxis;

	/// <summary>
	/// The mesh which stores all the information about this TerrainFace.
	/// </summary>
	private Mesh mesh;

	public TerrainFace(Mesh mesh, int resolution, Vector3 up)
	{
		this.mesh = mesh;
		this.resolution = resolution;
		this.localUp = up;
		this.localXAxis = new Vector3(up.y, up.z, up.x);
		this.localZAxis = Vector3.Cross(localUp, localXAxis);
	}

	/// <summary>
	/// This function will re-calculate all the vertices and triangles.
	/// </summary>
	public void UpdateMesh()
	{
		this.mesh.Clear();
		this.mesh.vertices = this.CreateVertices();
		this.mesh.triangles = this.CreateTriangles();
		this.mesh.RecalculateNormals();
	}

	/// <summary>
	/// Function to set the resolution of the terrainface. This will also
	/// trigger the function to update the mesh.
	/// </summary>
	/// <param name="resolution"></param>
	public void UpdateResolution(int resolution)
	{
		this.resolution = resolution;
		this.UpdateMesh();
	}

	/// <summary>
	/// Function to generate all the vertices of this TerrainFace.
	/// </summary>
	/// <returns></returns>
	private Vector3[] CreateVertices()
	{
		// Create a float of the resolution. Otherwise we would get integer division.
		var floatResolution = this.resolution * 1f - 1;
		var vertices = new List<Vector3>();

		for (int y = 0; y < resolution; y++)
		{
			for (int x = 0; x < resolution; x++)
			{
				var pointOnUnitCube = new Vector3();
				pointOnUnitCube += this.localUp;
				pointOnUnitCube += this.localXAxis * (((x / floatResolution) - .5f) * 2);
				pointOnUnitCube += this.localZAxis * (((y / floatResolution) - .5f) * 2);

				// Normalize the vector so it becomes a sphere.
				pointOnUnitCube.Normalize();
				vertices.Add(pointOnUnitCube);
			}
		}

		// Check if we have 
		if (vertices.Count != resolution * resolution)
			throw new InvalidOperationException(string.Format("Number of vertices is wrong! It is {0} but it should be", vertices.Count, resolution * resolution));

		return vertices.ToArray();
	}

	/// <summary>
	/// Function to generate all the Triangles for this face.
	/// </summary>
	/// <returns></returns>
	private int[] CreateTriangles()
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
