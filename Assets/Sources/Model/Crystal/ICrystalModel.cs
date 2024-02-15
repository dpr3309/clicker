using UniRx;
using UnityEngine;

namespace Clicker.Model
{
    public interface ICrystalModel
    {
        IReadOnlyReactiveProperty<ulong> Score { get; }
        IReadOnlyReactiveCollection<Vector2> CrystalPositions { get; }
        void ProcessPlayerPosition(Vector2 playerChipPosition);
        void Startup(Vector2 playerChipPosition);
        void ReleaseAll();
        void ResetScore();
    }
}