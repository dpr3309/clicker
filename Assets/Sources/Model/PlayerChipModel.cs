using UniRx;
using UnityEngine;

namespace Clicker.Model
{
    internal class PlayerChipModel : IPlayerChipModel
    {
        private readonly ICoordinateProcessor _coordinateProcessor;
        public float Speed => 0.005f;

        private IReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>();
        public IReactiveProperty<Vector3> Position => _position;

        private readonly Vector3 _startOffset = new Vector3(0, 0.5f, 0);

        public PlayerChipModel(ICoordinateProcessor coordinateProcessor)
        {
            _coordinateProcessor = coordinateProcessor;
        }

        public void UpdatePosition(Vector3 newPosition)
        {
            newPosition.y = _startOffset.y;
            _position.Value = newPosition;
        }

        public void ChangeDirection()
        {
            _coordinateProcessor.ChangeDirection();
        }
    }
}