using UnityEngine;

namespace Clicker.Model
{
    internal interface ICoordinateModifier
    {
        Vector3 TransformCoordinates(Vector3 playerChipPosition, float modifier);
        Vector3 TransformCoordinatesFall(Vector3 playerChipPosition, float modifier);
    }
}