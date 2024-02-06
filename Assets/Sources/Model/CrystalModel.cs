using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Clicker.Model
{
    internal class CrystalModel : ICrystalModel
    {
        // todo: move it to context!
        private const int MIN_CRYSTALS_COUT = 10;
        private const float OFFSET = -2.5f;

        private ICoordinateProcessor _coordinateProcessor;
        private ICrystalPositionGenerator _crystalPositionGenerator;
        private IFieldModel _fieldModel;

        private ReactiveProperty<ulong> _score = new ReactiveProperty<ulong>(0);
        public IReadOnlyReactiveProperty<ulong> Score => _score;


        private IReactiveCollection<Vector2> _crystalPositions = new ReactiveCollection<Vector2>();
        public IReadOnlyReactiveCollection<Vector2> CrystalPositions => _crystalPositions;

        public CrystalModel(ICoordinateProcessor coordinateProcessor,
            ICrystalPositionGenerator crystalPositionGenerator, IFieldModel fieldModel)
        {
            _coordinateProcessor = coordinateProcessor;
            _crystalPositionGenerator = crystalPositionGenerator;
            _fieldModel = fieldModel;
        }

        public void Startup()
        {
            CheckCrystalsCount();
        }

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

            ReleaseTraversedObjects(playerChipPosition);
            CheckCrystalsCount();
        }

        public void ReleaseTraversedObjects(Vector2 playerChipPosition)
        {
            var traversedCrystals = _crystalPositions.SelectTraversedObject(playerChipPosition, OFFSET);
            ReleaseObjects(traversedCrystals, _crystalPositions);
        }

        private void ReleaseObjects(List<Vector2> itemsToFreed, IReactiveCollection<Vector2> instances)
        {
            for (int i = 0; i < itemsToFreed.Count; i++)
            {
                instances.Remove(itemsToFreed[i]);
            }
        }

        private void CheckCrystalsCount()
        {
            if (_crystalPositions.Count < MIN_CRYSTALS_COUT)
            {
                if (GenerationCrystals())
                    CheckCrystalsCount();
            }
        }

        private bool GenerationCrystals()
        {
            var availablePositions = _fieldModel.TileInstances.Except(_crystalPositions).ToList();

            if (!availablePositions.Any())
                return false;

            var tilesPositions = _crystalPositionGenerator.GenerateCrystalPositions(availablePositions);
            foreach (var position in tilesPositions)
            {
                _crystalPositions.Add(position);
            }

            return true;
        }
    }
}