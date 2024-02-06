using UniRx;
using UnityEngine;

namespace Clicker.ViewModel
{
    public interface ICrystalViewModel
    {
        IReadOnlyReactiveProperty<ulong> Score { get; }
        IReadOnlyReactiveCollection<Vector2> CrystalPositions { get; }
    }
}