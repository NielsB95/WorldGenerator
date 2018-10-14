using Assets.Scenes.Scripts;
using Assets.Scenes.Scripts.TerrainGenerator;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour
{
	#region Settings
	[Range(2, 256)]
	public int Resolution;

	[Range(1, 100)]
	public float Scale;
	#endregion

	/// <summary>
	/// Information about each side of the planet.
	/// </summary>
	TerrainFace[] terrainFaces;

	/// <summary>
	/// An array of objects that are responsible for the rendering. We don't
	/// want to throw these away everytime we compile. Therfore the `SerializeField`. We also
	/// don't want to see this in our Inspector.
	/// </summary>
	[SerializeField, HideInInspector]
	MeshFilter[] meshFilters;

	/// <summary>
	/// Array of all the possible directions.
	/// </summary>
	private Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

	private void OnValidate()
	{
		// Initialize if needed.
		this.Initialize();

		// Get the current settings.
		var settings = GetSettings();

		// Update all terrainFaces.
		for (var i = 0; i < directions.Length; i++)
			terrainFaces[i].UpdateSettings(settings);
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

		// Create a face for each direction.
		for (var i = 0; i < directions.Length; i++)
		{
			if (meshFilters[i] == null)
			{
				var terrainObj = new GameObject("Mesh " + directions[i]);
				terrainObj.transform.parent = transform;
				terrainObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
				meshFilters[i] = terrainObj.AddComponent<MeshFilter>();
				meshFilters[i].sharedMesh = new Mesh();
			}

			faces[i] = new TerrainFace(meshFilters[i].sharedMesh, directions[i], GetSettings());
		}

		return faces;
	}

	private PlanetSettings GetSettings()
	{
		return new PlanetSettings()
		{
			Resolution = this.Resolution,
			Scale = this.Scale,
			TerrainGenerator = new NoiseTerrainGenerator()
		};
	}
}
