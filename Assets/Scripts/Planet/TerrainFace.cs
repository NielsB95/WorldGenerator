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
    /// The mesh which stores all the information about this TerrainFace.
    /// </summary>
    private Mesh mesh;

    private ElevationCalculator elevationCalculator;

    public MinMax TerrainMinMax;

    public TerrainFace(Mesh mesh, Vector3 up, PlanetSettings settings)
    {
        this.mesh = mesh;
        this.settings = settings;
        this.TerrainMinMax = new MinMax();
        this.elevationCalculator = new ElevationCalculator(settings.LayerSettings);
        this.localYAxis = up;
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
        var resolution = this.settings.Resolution;
        var vertices = Vertices.Create(resolution, this.localYAxis, spherical: true);

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                var index = x + (y * resolution);
                var pointOnUnitCube = vertices[index];

                var height = elevationCalculator.Evaluate(pointOnUnitCube);

                // Add this new height value to TerrainMinMax.
                this.TerrainMinMax.AddValue(height);

                // Set the size of the planet.
                pointOnUnitCube *= settings.Scale * height;

                vertices[index] = pointOnUnitCube;
            }
        }

        return vertices;
    }
}