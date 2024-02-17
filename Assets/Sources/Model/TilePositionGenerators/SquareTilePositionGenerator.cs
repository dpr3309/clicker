using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Clicker.Model
{
    internal class SquareTilePositionGenerator : ITilePositionGenerator
    {
        private Dictionary<FaceDirections, Vector2[]> extremeCellPositions;

        private readonly Vector2Int _launchPadSize;
        private readonly DifficultyLevel _difficultyLevel;
        private readonly float _tileSize;
        private readonly ILevelSquareTilePositionGenerator _tilePositionGenerator;

        [Inject]
        private SquareTilePositionGenerator(float tileSize, DifficultyLevel difficultyLevel,
            Vector2Int launchPadSize, ILevelSquareTilePositionGenerator tilePositionGenerator)
        {
            _difficultyLevel = difficultyLevel;
            _tileSize = tileSize;
            _launchPadSize = launchPadSize;
            _tilePositionGenerator = tilePositionGenerator;
        }

        public IReadOnlyCollection<Vector2> GenerateLaunchPadPositions(Vector2 directionModifier)
        {
            Vector2[,] generatedTilePositions = new Vector2[_launchPadSize.x, _launchPadSize.y];
            List<Vector2> result = new List<Vector2>();
            for (int y = 0; y < _launchPadSize.y; y++)
            {
                for (int x = 0; x < _launchPadSize.x; x++)
                {
                    generatedTilePositions[x, y] = new Vector2(x * (_tileSize*directionModifier.x), y * (_tileSize*directionModifier.y));
                    var tilePosition = generatedTilePositions[x, y];
                    result.Add(new Vector2(tilePosition.x, tilePosition.y));
                }
            }

            Vector2[,] generatedPositionsAccordingToDifficultyLevel =
                ConvertCoordinatesAccordingDifficultyLevel(generatedTilePositions, _difficultyLevel);
            extremeCellPositions =
                _tilePositionGenerator.GenerateExtremeCellPositions(generatedPositionsAccordingToDifficultyLevel);

            return result.AsReadOnly();
        }

        public IReadOnlyCollection<Vector2> GeneratePositions()
        {
            FaceDirections extremeFace = (FaceDirections)UnityEngine.Random.Range(0, 2);
            Vector2[,] generatedTilePositions =
                _tilePositionGenerator.GenerateTilePositions(extremeFace, extremeCellPositions);
            extremeCellPositions = _tilePositionGenerator.GenerateExtremeCellPositions(generatedTilePositions);

            List<Vector2> result = new List<Vector2>();
            for (int y = 0; y <= generatedTilePositions.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= generatedTilePositions.GetUpperBound(0); x++)
                {
                    var tilePosition = generatedTilePositions[x, y];
                    result.Add(new Vector2(tilePosition.x, tilePosition.y));
                }
            }

            return result.AsReadOnly();
        }

        private Vector2[,] ConvertCoordinatesAccordingDifficultyLevel(Vector2[,] originalCoordinates,
            DifficultyLevel targetCoordinatesDifficultyLevel)
        {
            DifficultyLevel originalCoordinatesDifficultyLevel =
                DetermineDifficultyLevelForCoordinates(originalCoordinates.GetUpperBound(0));

            if (targetCoordinatesDifficultyLevel < originalCoordinatesDifficultyLevel)
                throw new Exception(
                    $"[SquareTilePositionGenerator.ConvertCoordinatesAccordingDifficultyLevel] attempt to convert from greater difficulty to less. {targetCoordinatesDifficultyLevel} < {originalCoordinatesDifficultyLevel}");

            switch (targetCoordinatesDifficultyLevel)
            {
                // width of field 1 tile
                case DifficultyLevel.High:
                {
                    var x = originalCoordinates.GetUpperBound(0);
                    var y = originalCoordinates.GetUpperBound(1);
                    return new Vector2[1, 1] { { originalCoordinates[x, y] } };
                }
                // width of field  2х2 tiles 
                case DifficultyLevel.Middle:
                {
                    Vector2[,] result = new Vector2[2, 2];

                    int[] coordinatesIndexes = { 1, 2 };

                    for (int y = 0; y < coordinatesIndexes.Length; y++)
                    {
                        for (int x = 0; x < coordinatesIndexes.Length; x++)
                        {
                            result[x, y] = originalCoordinates[coordinatesIndexes[x], coordinatesIndexes[y]];
                        }
                    }

                    return result;
                }
                // width of field  3х3 tiles
                case DifficultyLevel.Low:
                    return originalCoordinates;

                default:
                    throw new Exception(
                        $"[SquareTilePositionGenerator.ConvertCoordinatesAccordingDifficultyLevel] unhandled difficulty level: {targetCoordinatesDifficultyLevel}");
            }
        }

        private DifficultyLevel DetermineDifficultyLevelForCoordinates(int upperBoundOfDimensions)
        {
            switch (upperBoundOfDimensions)
            {
                case 0:
                    return DifficultyLevel.High;
                case 1:
                    return DifficultyLevel.Middle;
                case 2:
                    return DifficultyLevel.Low;

                default:
                {
                    if (upperBoundOfDimensions > 2)
                        return DifficultyLevel.Low;

                    throw new Exception(
                        $"[SquareTilePositionGenerator.DetermineDifficultyLevelForCoordinates] unhandled dimensions of coordinates array. Current array dimensions = {upperBoundOfDimensions}");
                }
            }
        }
    }
}