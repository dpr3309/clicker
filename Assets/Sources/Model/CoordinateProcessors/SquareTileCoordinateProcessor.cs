using Clicker.Tools;
using UnityEngine;

namespace Clicker.Model
{
    internal sealed class SquareTileCoordinateProcessor : ITileCoordinateProcessor
    {
        private readonly float _halfTileSize;
        private readonly double _tileArea;

        public SquareTileCoordinateProcessor(float tileSize)
        {
            _halfTileSize = tileSize / 2f;
            _tileArea = GeometricCalculator.CalculateAreaOfSquare(tileSize);
        }

        public bool ContainsCoordinates(Vector2 coordinatesCenterOfFigure, Vector2 otherCoordinates)
        {
            return GeometricCalculator.SquareContainsPoint(coordinatesCenterOfFigure, otherCoordinates, _halfTileSize,
                _tileArea);
        }
    }
}