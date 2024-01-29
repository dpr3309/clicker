using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clicker.Model
{
    internal abstract class ALevelSquareTilePositionGenerator
    {
        private readonly float _tileSize;

        protected abstract Vector2Int[] TopFaceCellIndices { get; }
        protected abstract Vector2Int[] RightFaceCellIndices { get; }

        protected abstract Vector2Int GeneratedAreaSize { get; }

        protected ALevelSquareTilePositionGenerator(float tileSize)
        {
            _tileSize = tileSize;
        }

        public Dictionary<FaceDirections, Vector2[]> GenerateExtremeCellPositions(Vector2[,] generatedTilePositions)
        {
            return new Dictionary<FaceDirections, Vector2[]>
            {
                {
                    FaceDirections.Top,
                    TopFaceCellIndices.Select(index => generatedTilePositions[index.x, index.y]).ToArray()
                },
                {
                    FaceDirections.Right,
                    RightFaceCellIndices.Select(index => generatedTilePositions[index.x, index.y]).ToArray()
                }
            };
        }

        /// <summary>
        /// Creates tile positions.
        /// </summary>
        /// <returns>The tile positoins.</returns>
        public Vector2[,] GenerateTilePositions(FaceDirections extremeFace,
            Dictionary<FaceDirections, Vector2[]> extremeCellPositions)
        {
            Vector2[,] result = new Vector2[GeneratedAreaSize.x, GeneratedAreaSize.y];
            Vector2[] extremeCellPositionItems = extremeCellPositions[extremeFace];
            switch (extremeFace)
            {
                case FaceDirections.Top:
                    // next tiles on top
                    for (int y = 0; y < GeneratedAreaSize.y; y++)
                    {
                        var yAxisExtremeCellPositionItems = extremeCellPositionItems[y];
                        for (int x = 0; x < GeneratedAreaSize.x; x++)
                        {
                            var xAxisExtremeCellPositionItems = extremeCellPositionItems[x];
                            var tilePosition = new Vector2(xAxisExtremeCellPositionItems.x,
                                yAxisExtremeCellPositionItems.y + _tileSize * (y + 1));
                            result[x, y] = tilePosition;
                        }
                    }

                    break;
                case FaceDirections.Right:
                    // next tiles from side
                    for (int y = 0; y < GeneratedAreaSize.y; y++)
                    {
                        var yAxisExtremeCellPositionItems = extremeCellPositionItems[y];
                        for (int x = 0; x < GeneratedAreaSize.x; x++)
                        {
                            var xAxisExtremeCellPositionItems = extremeCellPositionItems[x];
                            var tilePosition = new Vector2(xAxisExtremeCellPositionItems.x + _tileSize * (x + 1),
                                yAxisExtremeCellPositionItems.y);
                            result[x, y] = tilePosition;
                        }
                    }

                    break;

                default:
                    throw new Exception(
                        $"[SquareTilePositionGenerator.GenerateTilePositions] unhandled FaceDirections: {extremeFace}");
            }

            return result;
        }
    }
}