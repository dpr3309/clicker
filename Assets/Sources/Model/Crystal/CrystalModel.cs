using System.Collections.Generic;
using System.Linq;
using Clicker.Tools;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.Model
{
    internal class CrystalModel : ICrystalModel
    {
        private readonly ICoordinateProcessor _coordinateProcessor;
        private readonly ICrystalPositionGenerator _crystalPositionGenerator;
        private readonly IFieldModel _fieldModel;
        private readonly ApplicationContext _applicationContext;

        private ReactiveProperty<ulong> _score = new ReactiveProperty<ulong>(0);
        public IReadOnlyReactiveProperty<ulong> Score => _score;

        private IReactiveCollection<Vector2> _crystalPositions = new ReactiveCollection<Vector2>();
        public IReadOnlyReactiveCollection<Vector2> CrystalPositions => _crystalPositions;

        [Inject]
        private CrystalModel(ICoordinateProcessor coordinateProcessor,
            ICrystalPositionGenerator crystalPositionGenerator, IFieldModel fieldModel,
            ApplicationContext applicationContext)
        {
            _coordinateProcessor = coordinateProcessor;
            _crystalPositionGenerator = crystalPositionGenerator;
            _fieldModel = fieldModel;
            _applicationContext = applicationContext;
        }

        public void Startup(Vector2 playerChipPosition)
        {
            CheckCrystalsCount(playerChipPosition);
        }

        public void ReleaseAll()
        {
            ReleaseObjects(_crystalPositions.ToList(), _crystalPositions);
        }

        public void ResetScore()
        {
            _score.Value = 0;
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
            CheckCrystalsCount(playerChipPosition);
        }

        public void ReleaseTraversedObjects(Vector2 playerChipPosition)
        {
            var traversedCrystals =
                _crystalPositions.SelectTraversedObject(playerChipPosition, _applicationContext.ReleaseObjectsOffset);
            ReleaseObjects(traversedCrystals, _crystalPositions);
        }

        private void ReleaseObjects(List<Vector2> itemsToFreed, IReactiveCollection<Vector2> instances)
        {
            for (int i = 0; i < itemsToFreed.Count; i++)
            {
                instances.Remove(itemsToFreed[i]);
            }
        }

        private void CheckCrystalsCount(Vector2 playerChipPosition)
        {
            if (_crystalPositions.Count < _applicationContext.MinCrystalsCount)
            {
                if (GenerationCrystals(playerChipPosition))
                    CheckCrystalsCount(playerChipPosition);
            }
        }

        private bool GenerationCrystals(Vector2 playerChipPosition)
        {
            var notAvailablePositions = _fieldModel.TileInstances.SelectTraversedObject(playerChipPosition);
            notAvailablePositions.Add(playerChipPosition);

            var availablePositions = _fieldModel.TileInstances.Except(_crystalPositions).Except(notAvailablePositions)
                .ToList();

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