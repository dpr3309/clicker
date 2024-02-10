using System;
using Zenject;

namespace Clicker.Model
{
    [Serializable]
    internal class GameSettings
    {
        public TileType TileType { get; }
        public float TileSize { get; }
        public PlayerChipType PlayerChipType { get; }
        public float PlayerChipRadius { get; }

        [Inject]
        public GameSettings(TileType tileType, float tileSize, PlayerChipType playerChipType, float playerChipRadius)
        {
            TileType = tileType;
            TileSize = tileSize;
            PlayerChipType = playerChipType;
            PlayerChipRadius = playerChipRadius;
        }
    }
}