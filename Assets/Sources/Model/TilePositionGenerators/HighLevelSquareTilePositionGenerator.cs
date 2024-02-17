using UnityEngine;

namespace Clicker.Model
{
    internal class HighLevelSquareTilePositionGenerator : ALevelSquareTilePositionGenerator
    {
        private readonly Vector2Int[] _faceCellIndices = { new Vector2Int(0, 0) };

        protected override Vector2Int[] TopFaceCellIndices => _faceCellIndices;

        protected override Vector2Int[] RightFaceCellIndices => _faceCellIndices;

        private readonly Vector2Int _generatedAreaSize = new Vector2Int(1, 1);
        protected override Vector2Int GeneratedAreaSize => _generatedAreaSize;

        public HighLevelSquareTilePositionGenerator(float tileSize,
            IDirectionPositionGenerator verticalPositionGenerator,
            IDirectionPositionGenerator horizontalPositionGenerator)
            : base(tileSize, verticalPositionGenerator, horizontalPositionGenerator)
        {
        }
    }
}