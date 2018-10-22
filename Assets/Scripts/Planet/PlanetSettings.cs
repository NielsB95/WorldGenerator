using System;
using System.Collections.Generic;
using TerrainGenerator.LayerStyling;
using TerrainGenerator.Settings;
using UnityEngine;

namespace Assets.Scenes.Scripts
{
    [Serializable]
    public class PlanetSettings
    {
        public ViewType ViewType;

        [Range(2, 256)]
        public int Resolution;

        [Range(1, 100)]
        public float Scale;

        public float WaterHeight = 0f;

        public List<LayerColour> WorldColours = new List<LayerColour>();

        public List<NoiseSettings> LayerSettings = new List<NoiseSettings>();
    }

    public enum ViewType
    {
        All,
        Water,
        Terrain
    }
}
