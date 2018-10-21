using Assets.Scenes.Scripts;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour
{
    /// <summary>
    /// Settings of the planet that specify the shape and color.
    /// </summary>
    public PlanetSettings settings;

    /// <summary>
    /// Material for this planet.
    /// </summary>
    public Material PlanetMaterial;

    /// <summary>
    /// Information about each side of the planet.
    /// </summary>
    private TerrainFace[] terrainFaces;

    /// <summary>
    /// An array of objects that are responsible for the rendering. We don't
    /// want to throw these away everytime we compile. Therfore the `SerializeField`. We also
    /// don't want to see this in our Inspector.
    /// </summary>
    [SerializeField, HideInInspector]
    private MeshFilter[] meshFilters;

    /// <summary>
    /// The terrain minimum max.
    /// </summary>
    private MinMax PlanetMinMax;

    /// <summary>
    /// Array of all the possible directions.
    /// </summary>
    private Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    public Planet()
    {
        PlanetMinMax = new MinMax();
    }

    private void OnValidate()
    {
        // Initialize if needed.
        this.Initialize();

        // Reset the PlanetMinMax values. We will set them again when we update
        // the TerrainFaces.
        PlanetMinMax.Reset();

        // Update all terrainFaces.
        for (var i = 0; i < directions.Length; i++)
        {
            terrainFaces[i].UpdateSettings(settings);

            // Add the MinMax values of this face.
            PlanetMinMax.AddValue(terrainFaces[0].TerrainMinMax.Min);
            PlanetMinMax.AddValue(terrainFaces[0].TerrainMinMax.Max);
        }

        this.SetShaderValues();
    }

    public void Initialize()
    {
        if (meshFilters == null || meshFilters.Length == 0)
            meshFilters = new MeshFilter[6];

        if (terrainFaces == null || terrainFaces.Length == 0 || meshFilters.Any(x => x == null))
            terrainFaces = this.CreateTerrainFaces();
    }

    private TerrainFace[] CreateTerrainFaces()
    {
        // Create a local variable for the new faces.
        var faces = new TerrainFace[6];

        // Set the center of this planet in the shader.
        var center = transform.position;

        // Create a face for each direction.
        for (var i = 0; i < directions.Length; i++)
        {
            if (meshFilters[i] == null)
            {
                var terrainObj = new GameObject("Mesh " + directions[i]);
                terrainObj.transform.parent = transform;
                terrainObj.AddComponent<MeshRenderer>().sharedMaterial = this.PlanetMaterial;
                meshFilters[i] = terrainObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            faces[i] = new TerrainFace(meshFilters[i].sharedMesh, directions[i], settings);
        }

        return faces;
    }

    private void SetShaderValues()
    {
        this.PlanetMaterial.SetFloat("_Min", PlanetMinMax.Min);
        this.PlanetMaterial.SetFloat("_Max", PlanetMinMax.Max);
        this.PlanetMaterial.SetFloat("_Scale", settings.Scale);
        this.PlanetMaterial.SetVectorArray("_Colors", settings.WorldColours.Select(x => x.Colour.ColorToVector()).ToArray());
        this.PlanetMaterial.SetFloatArray("_ColorThresholds", settings.WorldColours.Select(x => x.Height).ToArray());
        this.PlanetMaterial.SetInt("_ColorCount", settings.WorldColours.Count());
    }
}