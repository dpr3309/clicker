using UnityEngine;

namespace Clicker.Model
{
    internal class ForwardPositionGenerator : IDirectionPositionGenerator
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
                    var tilePosition = new Vector2(xAxisExtremeCellPositionItems.x,
                        yAxisExtremeCellPositionItems.y + tileSize * (y + 1));
                    result[x, y] = tilePosition;
                }
            }

            return result;
        }
    }
}