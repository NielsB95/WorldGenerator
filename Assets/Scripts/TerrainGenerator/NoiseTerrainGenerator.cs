using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.Scripts.TerrainGenerator
{
    public class NoiseTerrainGenerator : ITerrainGenerator
    {
        private readonly Noise noise;
        private readonly TerrainFilter filter;

        public NoiseTerrainGenerator(TerrainFilter filter)
        {
            this.noise = new Noise();
            this.filter = filter;
        }

        public float Evaluate(Vector3 position)
        {
            float noiseValue = 0;
            float frequency = filter.BaseRoughness;
            float amplitude = 1;

            for (int i = 0; i < filter.Layers; i++)
            {
                float v = noise.Evaluate(position * frequency + filter.Center);
                noiseValue += (v + 1) * .5f * amplitude;
                frequency *= filter.Roughness;
                amplitude *= filter.Persistence;
            }

            noiseValue = Mathf.Max(0, noiseValue - filter.MinValue);
            return noiseValue * filter.Strength;
        }
    }
}
