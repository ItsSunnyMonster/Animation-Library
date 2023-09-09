using IMShapes;
using UnityEngine;

[ExecuteAlways]
public class Quad : MonoBehaviour
{
    private void Update()
    {
        Draw.Circle(new Vector2(0, 3), 0.5f, Color.cyan);
        Draw.Rectangle(new Vector2(0, 0), new Vector2(1, 1), Color.white);
    }
}
