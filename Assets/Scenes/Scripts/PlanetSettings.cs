using UnityEngine;

namespace Assets.Scenes.Scripts
{
	public class PlanetSettings
	{
		[Range(2, 256)]
		public int Resolution;

		[Range(1, 100)]
		public float Scale;
	}
}
