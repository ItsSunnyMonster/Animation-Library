using UnityEngine;

namespace IMShapes.Utils
{
    public static class QuadMeshGenerator
    {
        public static Mesh GetQuadMesh()
        {
            var vertices = new Vector3[]
            {
                new(-0.5f, 0.5f), // Top left
                new(0.5f, 0.5f), // Top right
                new(0.5f, -0.5f), // Bottom right
                new(-0.5f, -0.5f) // Bottom left
            };
            var indices = new[] { 0, 1, 2, 2, 3, 0 };
            var uvs = new Vector2[]
            {
                new(0, 1),
                new(1, 1),
                new(1, 0),
                new(0, 0)
            };

            var mesh = new Mesh();
            mesh.SetVertices(vertices);
            mesh.SetTriangles(indices, 0, true);
            mesh.SetUVs(0, uvs);
            return mesh;
        }
    }
}
