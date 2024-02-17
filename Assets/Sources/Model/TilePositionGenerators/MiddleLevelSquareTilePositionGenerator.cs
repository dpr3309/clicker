using UnityEngine;

namespace Clicker.Model
{
    internal class MiddleLevelSquareTilePositionGenerator : ALevelSquareTilePositionGenerator
    {
        private readonly Vector2Int[] _horizontalFaceCellIndices = { new Vector2Int(0, 1), new Vector2Int(1, 1) };
        protected override Vector2Int[] TopFaceCellIndices => _horizontalFaceCellIndices;

        private readonly Vector2Int[] _verticalFaceCellIndices = { new Vector2Int(1, 0), new Vector2Int(1, 1) };
        protected override Vector2Int[] RightFaceCellIndices => _verticalFaceCellIndices;

        private readonly Vector2Int _generatedAreaSize = new Vector2Int(2, 2);
        protected override Vector2Int GeneratedAreaSize => _generatedAreaSize;

        public MiddleLevelSquareTilePositionGenerator(float tileSize,
            IDirectionPositionGenerator verticalPositionGenerator,
            IDirectionPositionGenerator horizontalPositionGenerator)
            : base(tileSize, verticalPositionGenerator, horizontalPositionGenerator)
        {
        }
    }
}