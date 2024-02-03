using UniRx;
using UnityEngine;

namespace Clicker.Model
{
    internal interface IPlayerChipModel
    {
        public float Speed { get; }
        public IReactiveProperty<Vector3> Position { get; }
        Vector3 StartOffset { get; }
        public void UpdatePosition(Vector3 newPosition);
    }
}