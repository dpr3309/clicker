using UnityEngine;

namespace Clicker.Model
{
    internal interface IDirectionPositionGenerator
    {
        Vector2[,] GenerateTilePositions(Vector2Int generatedAreaSize, Vector2[] extremeCellPositionItems, float tileSize);
    }
}