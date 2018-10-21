using System;

namespace TerrainGenerator.Settings
{
    [Serializable]
    public enum TerrainType
    {
        Noise,
        Ridgid
    }

    public interface ITerrainSettings
    {
        TerrainType TerrainType { get; }
    }
}
