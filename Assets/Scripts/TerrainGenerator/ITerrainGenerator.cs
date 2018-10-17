using UnityEngine;

namespace Assets.Scenes.Scripts.TerrainGenerator
{
	public interface ITerrainGenerator
	{
		float Evaluate(Vector3 position);
	}
}
