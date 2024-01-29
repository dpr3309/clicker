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

        public FieldModel(ITilePositionGenerator positionGenerator)
        {
            _positionGenerator = positionGenerator;
        }

        public void Startup()
        {
            var tilesPositions = _positionGenerator.GenerateLaunchPadPositions().ToList().AsReadOnly();
            _tileInstances.AddRange(tilesPositions);
            Debug.Log($"_tileInstances count: {_tileInstances.Count}");
            CheckTilesCount();
        }


        public void ReleaseTraversedObjects(Vector3 playerChipPosition)
        {
            throw new System.NotImplementedException();
        }

        public void CheckTilesCount()
        {
            throw new System.NotImplementedException();
        }

        public void ProcessPlayerPosition(Vector3 playerChipPosition)
        {
            ReleaseTraversedObjects(playerChipPosition);
            CheckTilesCount();
        }
    }
}