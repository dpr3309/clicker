using UnityEngine;

namespace Clicker.Model
{
    internal interface ICoordinateModifierManager
    {
        Vector3 TransformCoordinates(Vector3 coordinate, float modifier);
        void ChangeDirection();
    }
}