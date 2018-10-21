using UnityEngine;

namespace TerrainGenerator.Generators
{
    public interface ITerrainGenerator
    {
        float Evaluate(Vector3 position);
    }
}
