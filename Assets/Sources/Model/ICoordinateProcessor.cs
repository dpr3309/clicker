using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    internal interface ICoordinateProcessor
    {
        public Vector2 TransformCoordinates(Vector2 playerChipPosition, float speed);
        public bool CoordinatesAreWithinTiles(Vector2 playerChipPosition, IEnumerable<Vector2> tilePositions);
        bool PlayerChipCollisionWithOtherObject(Vector2 playerChipPosition, Vector2 crystalInstance);
    }
}