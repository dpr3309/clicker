using UnityEngine;

namespace Clicker.Model
{
    internal class LeftPositionGenerator : IDirectionPositionGenerator
    {
        public Vector2[,] GenerateTilePositions(Vector2Int generatedAreaSize, Vector2[] extremeCellPositionItems,
            float tileSize)
        {
            Vector2[,] result = new Vector2[generatedAreaSize.x, generatedAreaSize.y];
            for (int y = 0; y < generatedAreaSize.y; y++)
            {
                var yAxisExtremeCellPositionItems = extremeCellPositionItems[y];
                for (int x = 0; x < generatedAreaSize.x; x++)
                {
                    var xAxisExtremeCellPositionItems = extremeCellPositionItems[x];
                    var tilePosition = new Vector2(xAxisExtremeCellPositionItems.x - tileSize * (x + 1),
                        yAxisExtremeCellPositionItems.y);
                    result[x, y] = tilePosition;
                }
            }

            return result;
        }
    }
}