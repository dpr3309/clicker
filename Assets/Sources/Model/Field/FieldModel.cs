using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Clicker.Model
{
    internal class FieldModel : IFieldModel
    {
        private IReactiveCollection<Vector2> _tileInstances = new ReactiveCollection<Vector2>();
        public IReadOnlyReactiveCollection<Vector2> TileInstances => _tileInstances;
        private ITilePositionGenerator _positionGenerator;


        // todo: move it to context!
        private const int MIN_TILES_COUNT = 30;
        private const float OFFSET = -2.5f;

        [Inject]
        private FieldModel(ITilePositionGenerator positionGenerator)
        {
            _positionGenerator = positionGenerator;
        }

        public void Startup()
        {
            var tilesPositions = _positionGenerator.GenerateLaunchPadPositions().ToList().AsReadOnly();
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

        public void ReleaseTraversedObjects(Vector2 playerChipPosition)
        {
            var traversedTiles = _tileInstances.SelectTraversedObject(playerChipPosition, OFFSET);
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
            if (_tileInstances.Count < MIN_TILES_COUNT)
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