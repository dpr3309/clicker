using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    internal class CoordinateProcessor : ICoordinateProcessor
    {
        public Vector2 TransformCoordinates(Vector2 playerChipPosition, float speed)
        {
            throw new System.NotImplementedException();
        }

        public bool CoordinatesAreWithinTiles(Vector2 playerChipPosition, IEnumerable<Vector2> tilePositions)
        {
            throw new System.NotImplementedException();
        }

        public bool PlayerChipCollisionWithOtherObject(Vector2 playerChipPosition, Vector2 crystalInstance)
        {
            throw new System.NotImplementedException();
        }
    }
}