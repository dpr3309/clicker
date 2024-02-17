using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    internal interface ILevelSquareTilePositionGenerator
    {
        Dictionary<FaceDirections, Vector2[]> GenerateExtremeCellPositions(Vector2[,] generatedTilePositions);

        Vector2[,] GenerateTilePositions(FaceDirections extremeFace,
            Dictionary<FaceDirections, Vector2[]> extremeCellPositions);
    }
}