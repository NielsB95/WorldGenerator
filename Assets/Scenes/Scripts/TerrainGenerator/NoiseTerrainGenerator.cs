using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.Scripts.TerrainGenerator
{
	public class NoiseTerrainGenerator : ITerrainGenerator
	{
		private Noise noise;
		private List<TerrainFilter> filters;

		public NoiseTerrainGenerator(List<TerrainFilter> filters)
		{
			this.noise = new Noise();
			this.filters = filters;
		}

		public float Evaluate(Vector3 position)
		{
			var height = 0f;

			foreach (var filter in filters)
			{
				var filterValue = noise.Evaluate(position * filter.Roughness + filter.Center);

				// Normalize to a value between 0 and 1
				filterValue = (filterValue + 1) / .5f;

				// Apply strengh
				filterValue *= filter.Strength;

				// Check if it passes the threshold
				if (height >= filter.Threshold)
					height += filterValue;
			}

			return height;
		}
	}
}
