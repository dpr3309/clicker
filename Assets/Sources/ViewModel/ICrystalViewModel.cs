using UniRx;
using UnityEngine;

namespace Clicker.ViewModel
{
    public interface ICrystalViewModel
    {
        IReadOnlyReactiveCollection<Vector2> CrystalPositions { get; }
    }
}