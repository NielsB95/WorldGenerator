using TerrainGenerator.Generators;
using TerrainGenerator.Settings;

namespace TerrainGenerator.Generators
{
    public static class TerrainGeneratorFactory
    {
        public static ITerrainGenerator Generate(ITerrainSettings settings)
        {
            switch (settings.TerrainType)
            {
                case TerrainType.Noise:
                    return new NoiseTerrainGenerator(settings as NoiseSettings);
                case TerrainType.Ridgid:
                    return null;
            }

            return null;
        }

    }
}
