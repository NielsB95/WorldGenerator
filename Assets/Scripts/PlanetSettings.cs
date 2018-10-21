using System;
using System.Collections.Generic;
using TerrainGenerator.Settings;
using UnityEngine;

namespace Assets.Scenes.Scripts
{
    [Serializable]
    public class PlanetSettings
    {
        [Range(2, 256)]
        public int Resolution;

        [Range(1, 100)]
        public float Scale;

        public List<NoiseSettings> LayerSettings = new List<NoiseSettings>();
    }
}
