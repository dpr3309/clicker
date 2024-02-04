using UniRx;
using UnityEngine;

namespace Clicker.Model
{
    internal interface IPlayerChipModel
    {
        float Speed { get; }
        IReactiveProperty<Vector3> Position { get; }
        void UpdatePosition(Vector3 newPosition);

        void ChangeDirection();
    }
}