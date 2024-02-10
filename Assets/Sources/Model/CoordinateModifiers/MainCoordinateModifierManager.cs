using UnityEngine;

namespace Clicker.Model
{
    internal sealed class MainCoordinateModifierManager : ICoordinateModifierManager
    {
        private int index;

        private ICoordinateModifier _currentCoordinateModifier;
        private readonly ICoordinateModifier[] _coordinateModifiers;

        public MainCoordinateModifierManager(params ICoordinateModifier[] coordinateModifiers)
        {
            if (coordinateModifiers.Length == 0)
                throw new System.Exception("[MainCoordinateModifierManager.ctor] coordinateModifiers.Length == 0!");

            _coordinateModifiers = coordinateModifiers;
            _currentCoordinateModifier = _coordinateModifiers[0];
        }

        public Vector3 TransformCoordinates(Vector3 coordinate, float modifier)
        {
            return _currentCoordinateModifier.TransformCoordinates(coordinate, modifier);
        }

        public Vector3 TransformCoordinatesFall(Vector3 coordinate, float modifier)
        {
            return _currentCoordinateModifier.TransformCoordinatesFall(coordinate, modifier);
        }

        public void ChangeDirection()
        {
            index = (++index < _coordinateModifiers.Length) ? index : 0;
            _currentCoordinateModifier = _coordinateModifiers[index];
        }
    }
}