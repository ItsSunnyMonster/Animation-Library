using UnityEngine;

namespace IMShapes.Utils
{
    public static class MaterialValues
    {
        public static readonly int ColourID = Shader.PropertyToID("_Colour");

        public static Material Rectangle;
        public static Material Circle;

        public static void Init()
        {
            if (Rectangle == null)
                Rectangle = new Material(Shader.Find("IMShapes/Rectangle"));
            if (Circle == null)
                Circle = new Material(Shader.Find("IMShapes/Circle"));
        }
    }
}
