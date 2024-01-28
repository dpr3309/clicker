using UnityEngine;

namespace Clicker.Model
{
    internal interface IFigureCoordinateProcessor
    {
        bool ContainsCoordinates(Vector3 coordinatesCenterOfFigure, Vector3 otherCoordinates);
    }
}