using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace Clicker.Model
{
    internal class FieldModel : IFieldModel
    {
        private IReactiveCollection<Vector3> _tileInstances = new ReactiveCollection<Vector3>();
        public IReadOnlyReactiveCollection<Vector3> TileInstances => _tileInstances;
        private ITilePositionGenerator _positionGenerator;

        private const float OFFSET = -2.5f;

        // todo: move it to context!
        private const int MIN_TILES_COUNT = 30;

        public FieldModel(ITilePositionGenerator positionGenerator)
        {
            _positionGenerator = positionGenerator;
        }

        public void Startup()
        {
            var tilesPositions = _positionGenerator.GenerateLaunchPadPositions().ToList().AsReadOnly();
            _tileInstances.AddRange(tilesPositions);
            CheckTilesCount();
        }


        public void ReleaseTraversedObjects(Vector3 playerChipPosition)
        {
            var traversedTiles = _tileInstances.SelectTraversedObject(playerChipPosition, OFFSET);
            ReleaseObjects(traversedTiles, _tileInstances);
        }

        private void ReleaseObjects(List<Vector3> itemsToFreed, IReactiveCollection<Vector3> instances)
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
            _tileInstances.AddRange(tilesPositions);
        }

        public void ProcessPlayerPosition(Vector3 playerChipPosition)
        {
            ReleaseTraversedObjects(playerChipPosition);
            CheckTilesCount();
        }
    }
}