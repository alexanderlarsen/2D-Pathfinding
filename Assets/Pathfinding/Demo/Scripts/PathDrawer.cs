using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Core;
using UnityEngine;

namespace Pathfinding.Demo
{
    public class PathDrawer : MonoBehaviour
    {
        private Pathfinder pathfinder;
        private LineRenderer line;

        [SerializeField]
        private GridController grid;

        private Tile startTile;
        private Tile endTile;

        private void Awake()
        {
            line = GetComponent<LineRenderer>();
            startTile = grid.tiles.Cast<Tile>().FirstOrDefault(tile => tile.Type == Tile.TileType.StartTile);
            endTile = grid.tiles.Cast<Tile>().FirstOrDefault(tile => tile.Type == Tile.TileType.EndTile);
        }

        private IEnumerator Start()
        {
            pathfinder = new Pathfinder(grid.tiles);

            while (true)
            {
                List<ITileData> path = pathfinder.FindPath(startTile, endTile);

                if (path == null)
                {
                    Debug.Log("Path not found");
                    yield return new WaitForEndOfFrame();
                }

                path.Reverse();
                yield return DrawPath(path);
            }
        }

        private IEnumerator DrawPath(List<ITileData> path)
        {
            if (path == null)
                yield break;

            line.positionCount = 0;

            if (path.Count > 0)
                path.Insert(0, startTile);

            for (int i = 0; i < path.Count; i++)
            {
                line.positionCount++;

                var position = (Vector3Int)path[i].WorldPosition;
                position.z = -1;

                line.SetPosition(i, position);
                yield return new WaitForSeconds(.05f);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}