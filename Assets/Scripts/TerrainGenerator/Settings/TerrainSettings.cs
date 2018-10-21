using System;

namespace TerrainGenerator.Settings
{
    [Serializable]
    public abstract class TerrainSettings : ITerrainSettings
    {
        public TerrainType TerrainType { get; set; }
        public bool Disabled { get; set; }
    }
}
