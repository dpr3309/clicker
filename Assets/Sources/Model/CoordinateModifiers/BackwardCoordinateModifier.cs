using UnityEngine;

namespace Clicker.Model
{
    internal class BackwardCoordinateModifier : ICoordinateModifier
    {
        public Vector3 TransformCoordinates(Vector3 coordinate, float modifier)
        {
            return Vector3.Lerp(coordinate, coordinate + Vector3.back, modifier);
        }

        public Vector3 TransformCoordinatesFall(Vector3 coordinate, float modifier)
        {
            return Vector3.Lerp(coordinate, coordinate + Vector3.back * 0.5f + Vector3.down * 2, modifier);
        }
    }
}