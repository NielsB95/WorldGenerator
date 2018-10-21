using UnityEngine;

namespace UnityEngine
{
    public static class ColorExtension
    {
        public static Vector4 ColorToVector(this Color color)
        {
            return new Vector4()
            {
                x = color.r,
                y = color.g,
                z = color.b,
                w = color.a
            };
        }
    }
}
