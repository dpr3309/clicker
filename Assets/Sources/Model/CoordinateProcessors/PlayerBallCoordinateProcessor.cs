using Clicker.Tools;
using UnityEngine;

namespace Clicker.Model
{
    internal sealed class PlayerBallCoordinateProcessor : IPlayerChipCoordinateProcessor
    {
        private readonly float _radius;

        public PlayerBallCoordinateProcessor(float radius)
        {
            _radius = radius;
        }

        public bool ContainsCoordinates(Vector2 coordinatesCenterOfFigure, Vector2 otherCoordinates)
        {
            return GeometricCalculator.CircleContainsPoint(coordinatesCenterOfFigure, otherCoordinates, _radius);
        }
    }
}