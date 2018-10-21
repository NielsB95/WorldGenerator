using System;
using UnityEngine;

namespace TerrainGenerator.Settings
{
    [Serializable]
    public class NoiseSettings : TerrainSettings
    {
        public float Roughness = 0;

        [Range(0, 1)]
        public float Persistence = 0;

        [Range(1, 10)]
        public int Iterations = 1;

        [Range(0.001f, 10)]
        public float Strength = 1;

        public float MinValue = 0;

        public Vector3 Center;

        public new bool Disabled;
    }
}
