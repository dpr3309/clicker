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
            throw new System.NotImplementedException();
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