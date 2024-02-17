using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clicker.Model
{
    internal abstract class ALevelSquareTilePositionGenerator : ILevelSquareTilePositionGenerator
    {
        private readonly float _tileSize;
        private readonly IDirectionPositionGenerator _verticalPositionGenerator;
        private readonly IDirectionPositionGenerator _horizontalPositionGenerator;

        protected abstract Vector2Int[] TopFaceCellIndices { get; }
        protected abstract Vector2Int[] RightFaceCellIndices { get; }

        protected abstract Vector2Int GeneratedAreaSize { get; }

        protected ALevelSquareTilePositionGenerator(float tileSize,
            IDirectionPositionGenerator verticalPositionGenerator,
            IDirectionPositionGenerator horizontalPositionGenerator)
        {
            _tileSize = tileSize;
            _verticalPositionGenerator = verticalPositionGenerator;
            _horizontalPositionGenerator = horizontalPositionGenerator;
        }

        public Dictionary<FaceDirections, Vector2[]> GenerateExtremeCellPositions(Vector2[,] generatedTilePositions)
        {
            return new Dictionary<FaceDirections, Vector2[]>
            {
                {
                    FaceDirections.Vertical,
                    TopFaceCellIndices.Select(index => generatedTilePositions[index.x, index.y]).ToArray()
                },
                {
                    FaceDirections.Horizontal,
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
                case FaceDirections.Vertical:
                    result = _verticalPositionGenerator.GenerateTilePositions(GeneratedAreaSize,
                        extremeCellPositionItems, _tileSize);
                    break;
                case FaceDirections.Horizontal:
                    result = _horizontalPositionGenerator.GenerateTilePositions(GeneratedAreaSize,
                        extremeCellPositionItems, _tileSize);
                    break;

                default:
                    throw new Exception(
                        $"[SquareTilePositionGenerator.GenerateTilePositions] unhandled FaceDirections: {extremeFace}");
            }

            return result;
        }
    }
}