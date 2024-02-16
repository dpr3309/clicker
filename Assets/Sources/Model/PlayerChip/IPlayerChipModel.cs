using UniRx;
using UnityEngine;

namespace Clicker.Model
{
    public interface IPlayerChipModel
    {
        IReactiveProperty<Vector3> Position { get; }
        Vector2 Position2D { get; }

        void ChangeDirection();
        void StartFall();
        void StopMove();
        void StartMove();
        void Restart();
        void Update();
    }
}