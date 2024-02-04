using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    internal interface ICoordinateProcessor
    {
        Vector3 TransformCoordinates(Vector3 playerChipPosition, float speed);
        bool CoordinatesAreWithinTiles(Vector2 playerChipPosition, IEnumerable<Vector2> tilePositions);
        bool PlayerChipCollisionWithOtherObject(Vector2 playerChipPosition, Vector2 crystalInstance);

        void ChangeDirection();
    }
}