using System;
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
        this.mesh.vertices = Vertices.Create(this.settings.Resolution, this.localYAxis, spherical: true);
        this.mesh.triangles = Triangles.Create(this.settings.Resolution);
        this.mesh.RecalculateNormals();
    }
}
