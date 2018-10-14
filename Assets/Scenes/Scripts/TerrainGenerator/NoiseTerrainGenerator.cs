using UnityEngine;

namespace Assets.Scenes.Scripts.TerrainGenerator
{
	public class NoiseTerrainGenerator : ITerrainGenerator
	{
		Noise noise = new Noise();

		public float Evaluate(Vector3 position)
		{
			var height = noise.Evaluate(position);

			// Normailize
			height = (height + 1) / .5f;

			return height;
		}
	}
}
