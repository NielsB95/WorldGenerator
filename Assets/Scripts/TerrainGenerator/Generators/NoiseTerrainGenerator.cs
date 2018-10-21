using System;
using TerrainGenerator.Settings;
using UnityEngine;

namespace TerrainGenerator.Generators
{
    public class NoiseTerrainGenerator : ITerrainGenerator
    {
        private Noise noise;
        private NoiseSettings settings;

        public NoiseTerrainGenerator(NoiseSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings", "Settings can't be null. This could be a cast error.");

            this.noise = new Noise();
            this.settings = settings;
        }

        public float Evaluate(Vector3 point)
        {
            //var elevation = (1 + noise.Evaluate(position * settings.Roughness + settings.Center)) / 2;
            if (settings.Disabled)
                return 0f;

            float noiseValue = 0;
            float frequency = settings.Roughness;
            float amplitude = 1;

            for (int i = 0; i < settings.Iterations; i++)
            {
                float v = noise.Evaluate(point * frequency + settings.Center);
                noiseValue += (v + 1) * .5f * amplitude;
                frequency *= settings.Roughness;
                amplitude *= settings.Persistence;
            }

            noiseValue = Mathf.Max(0, noiseValue - settings.MinValue);
            return noiseValue * settings.Strength;


            //foreach (var filter in filters)
            //{
            //    var filterValue = 1 + noise.Evaluate(position * filter.Roughness + filter.Center);

            //    if ((height * filterValue) < filter.MinValue && filters.IndexOf(filter) != 0)
            //        continue;

            //    // Apply strengh
            //    filterValue *= filter.Strength;

            //    // Normalize to a value between 0 and 1
            //    filterValue = (filterValue + 1) / .5f;

            //    // Don't update if we disable the filter.
            //    if (filter.Disable)
            //        continue;

            //    height *= filterValue;
            //}

            //foreach (var filter in filters)
            //{
            //    // Value betwoon 0 an 1
            //    var filterValue = noise.Evaluate(position * filter.Roughness + filter.Center);


            //}


            //return elevation;
        }
    }
}
