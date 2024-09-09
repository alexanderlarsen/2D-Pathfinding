using UnityEngine;

namespace Pathfinding.Core
{
    public interface ITileData
    {
        bool IsWalkable { get; }
        int Cost { get; }
        Vector2Int WorldPosition { get; }
    }
}