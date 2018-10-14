using UnityEngine;

namespace Assets.Scenes.Scripts.TerrainGenerator
{
	public class FlatTerrainGenerator : ITerrainGenerator
	{
		public float Evaluate(Vector3 position)
		{
			return 1;
		}
	}
}
