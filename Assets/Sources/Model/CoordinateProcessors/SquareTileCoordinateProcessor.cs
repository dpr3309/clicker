using Clicker.Tools;
using UnityEngine;

namespace Clicker.Model
{
    internal sealed class SquareTileCoordinateProcessor : ITileCoordinateProcessor
    {
        private readonly float tileSize;
        private readonly float halfTileSize;
        private readonly double tileArea;

        public SquareTileCoordinateProcessor(float tileSize)
        {
            this.tileSize = tileSize;
            halfTileSize = tileSize / 2f;
            tileArea = GeometricCalculator.CalculateAreaOfSquare(tileSize);
        }

        public bool ContainsCoordinates(Vector2 coordinatesCenterOfFigure, Vector2 otherCoordinates)
        {
            return GeometricCalculator.SquareContainsPoint(coordinatesCenterOfFigure, otherCoordinates, halfTileSize,
                tileArea);
        }
    }
}