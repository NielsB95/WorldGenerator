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
            var height = 1f;

            foreach (var filter in filters)
            {
                var filterValue = 1 + noise.Evaluate(position * filter.Roughness + filter.Center);

                // Apply strengh
                filterValue *= filter.Strength;

                // Normalize to a value between 0 and 1
                filterValue = (filterValue + 1) / .5f;

                if (filter.Inverted)
                    filterValue *= -1;

                // Don't update if we disable the filter.
                if (filter.Disable)
                    continue;

                height *= filterValue;
            }

            height -= filters.Count;
            return height;
        }
    }
}
