using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.Model
{
    internal class PlayerChipModel : IPlayerChipModel
    {
        private readonly ICoordinateProcessor _coordinateProcessor;
        public float Speed => 0.005f;

        private IReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>();
        public IReactiveProperty<Vector3> Position => _position;

        // not good.
        // todo: try send x & z coordinates without Vector2?
        public Vector2 Position2D => new(_position.Value.x, _position.Value.z);

        [Inject]
        private PlayerChipModel(ICoordinateProcessor coordinateProcessor)
        {
            _coordinateProcessor = coordinateProcessor;
        }

        public void UpdatePosition(Vector3 newPosition)
        {
            _position.Value = newPosition;
        }

        public void ChangeDirection()
        {
            _coordinateProcessor.ChangeDirection();
        }
    }
}