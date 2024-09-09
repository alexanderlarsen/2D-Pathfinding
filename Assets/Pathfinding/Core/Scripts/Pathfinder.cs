using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Core
{
    public class Pathfinder
    {
        private Node[,] grid;
        private int width, height;

        public Pathfinder(ITileData[,] tiles)
        {
            ConstructNewGrid(tiles);
        }

        public void ConstructNewGrid(ITileData[,] tiles)
        {
            width = tiles.GetLength(0);
            height = tiles.GetLength(1);
            grid = new Node[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    grid[x, y] = new Node(tiles[x, y]);
        }

        public List<ITileData> FindPath(
            ITileData startTile,
            ITileData endTile)
        {
            Node startNode = grid[startTile.WorldPosition.x, startTile.WorldPosition.y];
            Node endNode = grid[endTile.WorldPosition.x, endTile.WorldPosition.y];

            List<Node> openList = new();
            HashSet<Node> closedList = new();
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node currentNode = GetNodeWithLowestFCost(openList);
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == endNode)
                    return RetracePath(startNode, endNode);

                foreach (Node neighbor in GetNeighbors(currentNode))
                {
                    if (!neighbor.tile.IsWalkable || closedList.Contains(neighbor))
                        continue;

                    int newCostToNeighbor = currentNode.GCost + neighbor.tile.Cost;

                    if (newCostToNeighbor < neighbor.GCost || !openList.Contains(neighbor))
                    {
                        neighbor.GCost = newCostToNeighbor;
                        neighbor.HCost = GetDistanceBetweenNodes(neighbor, endNode);
                        neighbor.Parent = currentNode;

                        if (!openList.Contains(neighbor))
                            openList.Add(neighbor);
                    }
                }
            }

            Debug.Log("No path found!");
            return null;
        }

        private Node GetNodeWithLowestFCost(List<Node> nodes)
        {
            Node currentNode = nodes[0];

            for (int i = 1; i < nodes.Count; i++)
                if (nodes[i].FCost < currentNode.FCost || (nodes[i].FCost == currentNode.FCost && nodes[i].HCost < currentNode.HCost))
                    currentNode = nodes[i];

            return currentNode;
        }

        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbours = new();

            if (node.tile.WorldPosition.x + 1 < width)
                neighbours.Add(grid[node.tile.WorldPosition.x + 1, node.tile.WorldPosition.y]);

            if (node.tile.WorldPosition.x - 1 >= 0)
                neighbours.Add(grid[node.tile.WorldPosition.x - 1, node.tile.WorldPosition.y]);

            if (node.tile.WorldPosition.y + 1 < height)
                neighbours.Add(grid[node.tile.WorldPosition.x, node.tile.WorldPosition.y + 1]);

            if (node.tile.WorldPosition.y - 1 >= 0)
                neighbours.Add(grid[node.tile.WorldPosition.x, node.tile.WorldPosition.y - 1]);

            return neighbours;
        }

        private int GetDistanceBetweenNodes(
            Node nodeA,
            Node nodeB)
        {
            int distanceX = Mathf.Abs(nodeA.tile.WorldPosition.x - nodeB.tile.WorldPosition.x);
            int distanceY = Mathf.Abs(nodeA.tile.WorldPosition.y - nodeB.tile.WorldPosition.y);

            if (distanceX > distanceY)
                return 14 * distanceY + 10 * (distanceX - distanceY);

            return 14 * distanceX + 10 * (distanceY - distanceX);
        }

        private List<ITileData> RetracePath(
            Node startNode,
            Node endNode)
        {
            List<ITileData> path = new();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode.tile);
                currentNode = currentNode.Parent;
            }

            return path;
        }
    }
}