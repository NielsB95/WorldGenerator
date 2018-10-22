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

    public Material WaterMaterial;

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
    private MeshFilter[] terrainMeshFilters;


    private WaterFace[] waterFaces;

    [SerializeField, HideInInspector]
    private MeshFilter[] waterMeshFilters;

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
            waterFaces[i].UpdateSettings(settings);

            // Add the MinMax values of this face.
            PlanetMinMax.AddValue(terrainFaces[0].TerrainMinMax.Min);
            PlanetMinMax.AddValue(terrainFaces[0].TerrainMinMax.Max);
        }

        this.SetShaderValues();

        this.ToggleObjects(settings.ViewType);
    }

    public void Initialize()
    {
        // Terrainfaces
        if (terrainMeshFilters == null || terrainMeshFilters.Length == 0)
            terrainMeshFilters = new MeshFilter[6];

        if (terrainFaces == null || terrainFaces.Length == 0 || terrainMeshFilters.Any(x => x == null))
            terrainFaces = this.CreateTerrainFaces();

        // Waterfaces
        if (waterMeshFilters == null || waterMeshFilters.Length == 0)
            waterMeshFilters = new MeshFilter[6];

        if (waterFaces == null || waterFaces.Length == 0 || waterMeshFilters.Any(x => x == null))
            waterFaces = this.CreateWaterFaces();

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
            if (terrainMeshFilters[i] == null)
            {
                var terrainObj = new GameObject("terrain mesh " + directions[i]);
                terrainObj.transform.parent = transform;
                terrainObj.AddComponent<MeshRenderer>().sharedMaterial = this.PlanetMaterial;
                terrainMeshFilters[i] = terrainObj.AddComponent<MeshFilter>();
                terrainMeshFilters[i].sharedMesh = new Mesh();
            }

            faces[i] = new TerrainFace(terrainMeshFilters[i].sharedMesh, directions[i], settings);
        }

        return faces;
    }

    private WaterFace[] CreateWaterFaces()
    {
        // Create a local variable for the new faces.
        var faces = new WaterFace[6];

        // Set the center of this planet in the shader.
        var center = transform.position;

        // Create a face for each direction.
        for (var i = 0; i < directions.Length; i++)
        {
            if (waterMeshFilters[i] == null)
            {
                var waterObj = new GameObject("water mesh " + directions[i]);
                waterObj.transform.parent = transform;
                waterObj.AddComponent<MeshRenderer>().sharedMaterial = this.WaterMaterial;
                waterMeshFilters[i] = waterObj.AddComponent<MeshFilter>();
                waterMeshFilters[i].sharedMesh = new Mesh();
            }

            faces[i] = new WaterFace(waterMeshFilters[i].sharedMesh, directions[i], settings);
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

        this.WaterMaterial.SetFloat("_Min", PlanetMinMax.Min);
        this.WaterMaterial.SetFloat("_Max", PlanetMinMax.Max);
        this.WaterMaterial.SetFloat("_Scale", settings.Scale);
    }

    private void ToggleObjects(ViewType type)
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("water"))
                child.gameObject.SetActive(type == ViewType.All || type == ViewType.Water);

            if (child.name.Contains("terrain"))
                child.gameObject.SetActive(type == ViewType.All || type == ViewType.Terrain);
        }
    }
}