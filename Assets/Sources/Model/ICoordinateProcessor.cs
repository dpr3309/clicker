using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    internal interface ICoordinateProcessor
    {
        public Vector3 TransformCoordinates(Vector3 playerChipPosition, float speed);
        public bool CoordinatesAreWithinTiles(Vector3 playerChipPosition, IEnumerable<Vector3> tilePositions);
        bool PlayerChipCollisionWithOtherObject(Vector3 playerChipPosition, Vector3 crystalInstance);
    }
}