using Assets.Scenes.Scripts;
using System;
using System.Collections.Generic;
using TerrainGenerator;
using UnityEngine;

public class TerrainFace
{
    /// <summary>
    /// The resolution at which this TerrainFace is created.
    /// </summary>
    private PlanetSettings settings;

    /// <summary>
    /// This tells us what the local up is for this TerrainFace.
    /// </summary>
    private Vector3 localYAxis;

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

    private ElevationCalculator elevationCalculator;

    public MinMax TerrainMinMax;

    public TerrainFace(Mesh mesh, Vector3 up, PlanetSettings settings)
    {
        this.mesh = mesh;
        this.settings = settings;
        this.localYAxis = up;
        this.localXAxis = new Vector3(up.y, up.z, up.x);
        this.localZAxis = Vector3.Cross(localYAxis, localXAxis);
        this.TerrainMinMax = new MinMax();
        this.elevationCalculator = new ElevationCalculator(settings.LayerSettings);
    }

    public void UpdateSettings(PlanetSettings settings)
    {
        this.settings = settings;
        this.elevationCalculator.Update(settings.LayerSettings);
        this.UpdateMesh();
    }

    /// <summary>
    /// This function will re-calculate all the vertices and triangles.
    /// </summary>
    public void UpdateMesh()
    {
        this.mesh.Clear();
        this.mesh.vertices = this.CreateVertices();
        this.mesh.triangles = Triangles.Create(this.settings.Resolution);
        this.mesh.RecalculateNormals();
    }

    /// <summary>
    /// Function to generate all the vertices of this TerrainFace.
    /// </summary>
    /// <returns></returns>
    private Vector3[] CreateVertices()
    {
        // Reset the TerrainMinMax. We will determine the new values in this function.
        this.TerrainMinMax.Reset();

        // Create a float of the resolution. Otherwise we would get integer division.
        var floatResolution = this.settings.Resolution * 1f - 1;
        var resolution = this.settings.Resolution;
        var vertices = new List<Vector3>();

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                var pointOnUnitCube = new Vector3();
                pointOnUnitCube += this.localYAxis;
                pointOnUnitCube += this.localXAxis * (((x / floatResolution) - .5f) * 2);
                pointOnUnitCube += this.localZAxis * (((y / floatResolution) - .5f) * 2);

                // Normalize the vector so it becomes a sphere.
                pointOnUnitCube.Normalize();

                var height = elevationCalculator.Evaluate(pointOnUnitCube);

                // Add this new height value to TerrainMinMax.
                this.TerrainMinMax.AddValue(height);

                // Set the size of the planet.
                pointOnUnitCube *= settings.Scale * height;

                vertices.Add(pointOnUnitCube);
            }
        }

        // Check if we have 
        if (vertices.Count != resolution * resolution)
            throw new InvalidOperationException(string.Format("Number of vertices is wrong! It is {0} but it should be {1}", vertices.Count, resolution * resolution));

        return vertices.ToArray();
    }
}