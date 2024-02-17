using UnityEngine;

namespace Clicker.Model
{
    internal class ApplicationContext
    {
        private const int MIN_CRYSTALS_COUT = 8;
        private const float RELEASE_OBJECTS_OFFSET = 2.5f;
        private const int MIN_TILES_COUNT = 30;
        private const float PLAYER_CHIP_SPEED = 0.005f;
        private readonly Vector3 START_POSTITION = new (0, 0.7f, 0);

        internal int MinCrystalsCount => MIN_CRYSTALS_COUT;
        internal float ReleaseObjectsOffset => RELEASE_OBJECTS_OFFSET;
        internal int MinTilesCount => MIN_TILES_COUNT;
        internal float PlayerChipSpeed => PLAYER_CHIP_SPEED;
        internal Vector3 PlayerChipStartPosition => START_POSTITION;
    }
}