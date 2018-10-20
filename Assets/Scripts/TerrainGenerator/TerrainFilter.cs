using System;
using UnityEngine;

namespace Assets.Scenes.Scripts.TerrainGenerator
{
    [Serializable]
    public class TerrainFilter
    {
        [Range(0, 10)]
        public int Layers = 0;

        public float BaseRoughness = 1;
        [Range(0, 10)]
        public float Roughness = 1;

        [Range(0, 1)]
        public float Strength = 1;

        public float Persistence = .5f;
        public Vector3 Center;
        public float MinValue = 0f;
    }
}
