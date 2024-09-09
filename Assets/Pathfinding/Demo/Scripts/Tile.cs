using Pathfinding.Core;
using TMPro;
using UnityEngine;

namespace Pathfinding.Demo
{
    public class Tile : MonoBehaviour, ITileData
    {
        [SerializeField, HideInInspector]
        private SpriteRenderer spriteRenderer;

        [SerializeField, HideInInspector]
        private TextMeshPro costTmp;

        public enum TileType
        {
            NotWalkable,
            Walkable,
            StartTile,
            EndTile
        }

        public Vector2Int WorldPosition => new(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        [field: SerializeField, HideInInspector]
        public bool IsWalkable { get; private set; }

        [field: SerializeField]
        public int Cost { get; private set; }

        [field: SerializeField]
        public TileType Type { get; private set; }

        private void OnValidate()
        {
            if (!spriteRenderer)
                spriteRenderer = GetComponent<SpriteRenderer>();

            if (!costTmp)
                costTmp = GetComponentInChildren<TextMeshPro>();

            IsWalkable = Type != TileType.NotWalkable;

            spriteRenderer.color = Type switch
            {
                TileType.NotWalkable => Color.black,
                TileType.Walkable => Color.white,
                TileType.StartTile => Color.green,
                TileType.EndTile => Color.red
            };

            costTmp.text = Cost.ToString();
        }
    }
}