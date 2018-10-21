using UnityEngine;

namespace TerrainGenerator.Generators
{
    /// <summary>
    /// A generator for creating just a flat surface.
    /// </summary>
    public class FlatTerrainGenerator : ITerrainGenerator
    {
        public float Evaluate(Vector3 position)
        {
            return 1;
        }
    }
}
