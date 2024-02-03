using UniRx;
using UnityEngine;

namespace Clicker.ViewModel
{
    public interface IPlayerChipViewModel
    {
        public IReadOnlyReactiveProperty<Vector3> Position { get; }
    }
}