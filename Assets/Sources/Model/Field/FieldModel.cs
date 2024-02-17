using System.Collections.Generic;
using System.Linq;
using Clicker.Model.Tools;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.Model
{
    internal class FieldModel : IFieldModel
    {
        private IReactiveCollection<Vector2> _tileInstances = new ReactiveCollection<Vector2>();
        public IReadOnlyReactiveCollection<Vector2> TileInstances => _tileInstances;
        private readonly ITilePositionGenerator _positionGenerator;
        private readonly ApplicationContext _applicationContext;
        private readonly Vector2Int _directionModifiers;

        [Inject]
        private FieldModel(ITilePositionGenerator positionGenerator, ApplicationContext applicationContext,
            Vector2Int directionModifiers)
        {
            _positionGenerator = positionGenerator;
            _applicationContext = applicationContext;
            _directionModifiers = directionModifiers;
        }

        public void Startup()
        {
            var tilesPositions = _positionGenerator.GenerateLaunchPadPositions(_directionModifiers).ToList()
                .AsReadOnly();
            foreach (var position in tilesPositions)
            {
                _tileInstances.Add(position);
            }

            CheckTilesCount();
        }

        public void ReleaseAll()
        {
            ReleaseObjects(_tileInstances.ToList(), _tileInstances);
        }

        private void ReleaseTraversedObjects(Vector2 playerChipPosition)
        {
            var traversedTiles =
                TileInstances.SelectTraversedObject(playerChipPosition, _applicationContext.ReleaseObjectsOffset);
            ReleaseObjects(traversedTiles, _tileInstances);
        }

        private void ReleaseObjects(List<Vector2> itemsToFreed, IReactiveCollection<Vector2> instances)
        {
            for (int i = 0; i < itemsToFreed.Count; i++)
            {
                instances.Remove(itemsToFreed[i]);
            }
        }

        public void CheckTilesCount()
        {
            if (_tileInstances.Count < _applicationContext.MinTilesCount)
            {
                GenerationTiles();
                CheckTilesCount();
            }
        }

        private void GenerationTiles()
        {
            var tilesPositions = _positionGenerator.GeneratePositions();
            foreach (var position in tilesPositions)
            {
                _tileInstances.Add(position);
            }
        }

        public void ProcessPlayerPosition(Vector2 playerChipPosition)
        {
            ReleaseTraversedObjects(playerChipPosition);
            CheckTilesCount();
        }
    }
}