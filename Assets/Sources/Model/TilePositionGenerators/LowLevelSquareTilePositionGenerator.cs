using UnityEngine;

namespace Clicker.Model
{
    internal class LowLevelSquareTilePositionGenerator : ALevelSquareTilePositionGenerator
    {
        private readonly Vector2Int[] _horizontalFaceCellIndices = { new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2, 2) };
        protected override Vector2Int[] TopFaceCellIndices => _horizontalFaceCellIndices;

        private readonly Vector2Int[] _verticalFaceCellIndices = { new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(2, 2) };
        protected override Vector2Int[] RightFaceCellIndices => _verticalFaceCellIndices;

        private readonly Vector2Int _generatedAreaSize = new Vector2Int(3, 3);
        protected override Vector2Int GeneratedAreaSize => _generatedAreaSize;

        public LowLevelSquareTilePositionGenerator(float tileSize)
            : base(tileSize)
        {
        }
    }
}