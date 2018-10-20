using System;
using UnityEngine;

namespace Assets.Scenes.Scripts.TerrainGenerator
{
    [Serializable]
    public class TerrainFilter
    {
        [Range(0, 10)]
        public float Roughness = 1;

        [Range(0, 1)]
        public float Strength = 1;
        public Vector3 Center;

        public bool Inverted = false;

        public bool Disable = false;
    }
}
