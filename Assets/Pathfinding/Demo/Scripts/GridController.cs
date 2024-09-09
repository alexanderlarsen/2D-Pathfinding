using UnityEditor;
using UnityEngine;

namespace Pathfinding.Demo
{
    public class GridController : MonoBehaviour
    {
        public Tile[,] tiles;

        [SerializeField]
        private GameObject gridTilePrefab;

        [field: SerializeField]
        public Vector2Int GridSize { get; private set; }

        private void Awake()
        {
            tiles = new Tile[GridSize.x, GridSize.y];

            foreach (Tile tile in GetComponentsInChildren<Tile>())
                tiles[tile.WorldPosition.x, tile.WorldPosition.y] = tile;
        }

        [ContextMenu(nameof(SpawnGrid))]
        private void SpawnGrid()
        {
            DestroyGrid();

            for (int x = 0; x < GridSize.x; x++)
                for (int y = 0; y < GridSize.y; y++)
                {
                    var obj = (GameObject)PrefabUtility.InstantiatePrefab(gridTilePrefab, transform);
                    obj.transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
                    obj.transform.SetParent(transform);
                }
        }

        [ContextMenu(nameof(DestroyGrid))]
        private void DestroyGrid()
        {
            while (transform.childCount > 0)
                if (Application.isPlaying)
                    Destroy(transform.GetChild(0).gameObject);
                else
                    DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}