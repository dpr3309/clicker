using UnityEngine;

namespace Clicker.Model
{
    internal interface IFigureCoordinateProcessor
    {
        bool ContainsCoordinates(Vector2 coordinatesCenterOfFigure, Vector2 otherCoordinates);
    }
}