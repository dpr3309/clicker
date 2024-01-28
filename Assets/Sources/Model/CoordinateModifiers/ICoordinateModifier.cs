using UnityEngine;

namespace Clicker.Model
{
    internal interface ICoordinateModifier
    {
        Vector3 TransformCoordinates(Vector3 playerChipPosition, float modifier);
    }
}