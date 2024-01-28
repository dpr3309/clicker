using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Clicker.Model
{
    internal class CrystalModel : ICrystalModel
    {
        private ICoordinateProcessor _coordinateProcessor;

        private ReactiveProperty<ulong> _score = new ReactiveProperty<ulong>(0);
        public IReactiveProperty<ulong> Score => _score;

        private IReactiveCollection<Vector2> _crystalPositions = new ReactiveCollection<Vector2>();
        public IReactiveCollection<Vector2> CrystalPositions => _crystalPositions;

        public void ProcessPlayerPosition(Vector2 playerChipPosition)
        {
            List<Vector2> toRemove = new List<Vector2>();

            foreach (var crystalInstance in _crystalPositions)
            {
                if (_coordinateProcessor.PlayerChipCollisionWithOtherObject(playerChipPosition, crystalInstance))
                {
                    _score.Value++;
                    toRemove.Add(crystalInstance);
                }
            }

            for (int i = 0; i < toRemove.Count; i++)
            {
                var crystal = toRemove[i];
                _crystalPositions.Remove(crystal);
            }
        }
    }
}