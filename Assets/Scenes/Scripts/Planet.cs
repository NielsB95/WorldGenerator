using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour
{
	/// <summary>
	/// The resoltuion of the object ranging between 1 and 256.
	/// </summary>
	[Range(2, 256)]
	public int Resolution = 2;

	TerrainFace[] terrainFaces;

	[SerializeField, HideInInspector]
	MeshFilter[] meshFilters;

	GameObject terrainObj;

	private Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

	private void OnValidate()
	{
		// Initialize if needed.
		this.Initialize();

		// Update all terrainFaces.
		for (var i = 0; i < directions.Length; i++)
			terrainFaces[i].UpdateResolution(Resolution);
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
		var faces = new TerrainFace[6];
		for (var i = 0; i < directions.Length; i++)
		{
			if (meshFilters[i] == null)
			{
				terrainObj = new GameObject("Mesh " + directions[i]);
				terrainObj.transform.parent = transform;
				terrainObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
				meshFilters[i] = terrainObj.AddComponent<MeshFilter>();
				meshFilters[i].sharedMesh = new Mesh();
			}

			faces[i] = new TerrainFace(meshFilters[i].sharedMesh, Resolution, directions[i]);
		}

		return faces;
	}
}
