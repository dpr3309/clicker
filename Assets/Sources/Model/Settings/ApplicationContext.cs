namespace Clicker.Model
{
    internal class ApplicationContext
    {
        private const int MIN_CRYSTALS_COUT = 8;
        private const float RELEASE_OBJECTS_OFFSET = -2.5f;
        private const int MIN_TILES_COUNT = 30;

        internal int MinCrystalsCount => MIN_CRYSTALS_COUT;
        internal float ReleaseObjectsOffset => RELEASE_OBJECTS_OFFSET;
        internal int MinTilesCount => MIN_TILES_COUNT;
    }
}