using UniRx;
using UnityEngine;

namespace Clicker.ViewModel
{
    public interface IPlayerChipViewModel
    {
        IReadOnlyReactiveProperty<Vector3> Position { get; }
        void ChangeDirection();
    }
}