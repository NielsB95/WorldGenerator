using System.Collections.Generic;
using TerrainGenerator.Generators;
using TerrainGenerator.Settings;
using UnityEngine;

namespace TerrainGenerator
{
    public class ElevationCalculator : ITerrainGenerator
    {
        private readonly List<ITerrainGenerator> generators;

        public ElevationCalculator(List<NoiseSettings> settings)
        {
            // Init the list in which we keep a the geneators for each layer.
            this.generators = new List<ITerrainGenerator>();
            this.Update(settings);
        }

        public float Evaluate(Vector3 position)
        {
            var elevation = 1f;
            float? firstLayerValue = null;

            foreach (var geneator in this.generators)
            {
                var value = geneator.Evaluate(position);

                if (!firstLayerValue.HasValue)
                    firstLayerValue = value;

                //if (firstLayerValue <= 0)
                //break;
                elevation += value;
            }

            return elevation;
        }

        public void Update(List<NoiseSettings> settings)
        {
            // Clear the current list.
            this.generators.Clear();

            // Convert the settings for each layer into TerrainGenerators. 
            foreach (var layerSettings in settings)
                this.generators.Add(TerrainGeneratorFactory.Generate(layerSettings));
        }
    }
}
