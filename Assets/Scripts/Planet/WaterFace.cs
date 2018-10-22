using System;
using System.Collections.Generic;
using Assets.Scenes.Scripts;
using UnityEngine;

public class WaterFace
{
    private PlanetSettings settings;

    private Mesh mesh;

    private Vector3 localYAxis;

    public WaterFace(Mesh mesh, Vector3 up, PlanetSettings settings)
    {
        this.mesh = mesh;
        this.localYAxis = up;
        this.settings = settings;
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

    public void UpdateSettings(PlanetSettings settings)
    {
        this.settings = settings;
        this.UpdateMesh();
    }

    private Vector3[] CreateVertices()
    {
        var resolution = this.settings.Resolution;
        var vertices = Vertices.Create(resolution, this.localYAxis, spherical: true);

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                var index = x + (y * resolution);
                var pointOnUnitSphere = vertices[index];

                pointOnUnitSphere *= this.settings.Scale * settings.WaterHeight;

                vertices[index] = pointOnUnitSphere;
            }
        }

        return vertices;
    }
}
