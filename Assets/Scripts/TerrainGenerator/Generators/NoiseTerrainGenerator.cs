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
        }
    }
}
