using IMShapes.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace IMShapes
{
    public static class Draw
    {
        public static void Rectangle(Vector2 centre, Vector2 size, Color colour)
        {
            if (size.x == 0 || size.y == 0)
                return;
            
            DrawManager.InitDraw();
            
            var mesh = QuadMeshGenerator.GetQuadMesh();
            var matrix = Matrix4x4.TRS(centre, Quaternion.identity, new Vector3(size.x, size.y, 1));
            DrawManager.MaterialPropertyBlock.SetColor(MaterialValues.ColourID, colour);
            DrawManager.CmdBuffer.DrawMesh(mesh, matrix, MaterialValues.Rectangle, 0, 0, DrawManager.MaterialPropertyBlock);
        }

        public static void Circle(Vector2 centre, float radius, Color colour)
        {
            if (radius == 0)
                return;
            
            DrawManager.InitDraw();

            var mesh = QuadMeshGenerator.GetQuadMesh();
            var matrix = Matrix4x4.TRS(centre, Quaternion.identity, new Vector3(radius * 2, radius * 2, 1));
            DrawManager.MaterialPropertyBlock.SetColor(MaterialValues.ColourID, colour);
            DrawManager.CmdBuffer.DrawMesh(mesh, matrix, MaterialValues.Circle, 0, 0, DrawManager.MaterialPropertyBlock);
        }
    }
}
