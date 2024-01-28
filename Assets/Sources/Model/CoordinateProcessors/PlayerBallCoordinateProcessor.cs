using Clicker.Tools;
using UnityEngine;

namespace Clicker.Model
{
    internal sealed class PlayerBallCoordinateProcessor : IPlayerChipCoordinateProcessor
    {
        private readonly float radius;

        public PlayerBallCoordinateProcessor(float radius)
        {
            this.radius = radius;
        }

        public bool ContainsCoordinates(Vector3 coordinatesCenterOfFigure, Vector3 otherCoordinates)
        {
            return GeometricCalculator.CircleContainsPoint(coordinatesCenterOfFigure, otherCoordinates, radius);
        }
    }
}