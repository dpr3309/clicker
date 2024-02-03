using UniRx;
using UnityEngine;

namespace Clicker.Model
{
    internal class PlayerChipModel : IPlayerChipModel
    {
        public float Speed => 0.005f;

        private IReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>();
        public IReactiveProperty<Vector3> Position => _position;

        private readonly Vector3 _startOffset = new Vector3(0, 0.5f, 0);
        public Vector3 StartOffset => _startOffset;

        public void UpdatePosition(Vector3 newPosition)
        {
            newPosition.y = _startOffset.y;
            _position.Value = newPosition;
        }
    }
}