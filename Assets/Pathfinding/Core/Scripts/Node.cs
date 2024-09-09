namespace Pathfinding.Core
{
    public class Node
    {
        public readonly ITileData tile;

        public Node(ITileData tile)
        {
            this.tile = tile;
        }
        
        public int FCost => GCost + HCost;
        public Node Parent { get; set; }
        public int GCost { get; set; }
        public int HCost { get; set; }
    }
}