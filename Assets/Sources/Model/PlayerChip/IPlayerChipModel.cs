using UniRx;
using UnityEngine;

namespace Clicker.Model
{
    public interface IPlayerChipModel
    {
        float Speed { get; }
        IReactiveProperty<Vector3> Position { get; }
        Vector2 Position2D { get; }
        void UpdatePosition(Vector3 newPosition);

        void ChangeDirection();
        void StartFall();
        void StopMove();
        void StartMove();
        void Restart();
        void Update();
    }
}