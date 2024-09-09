using UnityEngine;

namespace Pathfinding.Demo
{
    public class CameraController : MonoBehaviour
    {
        private void Start()
        {
            Vector2 gridCenter = FindObjectOfType<GridController>().GridSize / 2 - new Vector2(0.5f, 0.5f);
            transform.position = new Vector3(gridCenter.x, gridCenter.y, -10);
        }
    }
}