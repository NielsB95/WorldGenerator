using Assets.Scenes.Scripts.TerrainGenerator;
using UnityEngine;

namespace Assets.Scenes.Scripts
{
	public class PlanetSettings
	{
		[Range(2, 256)]
		public int Resolution = 2;

		[Range(1, 100)]
		public float Scale = 1;

		public ITerrainGenerator TerrainGenerator;
	}
}
