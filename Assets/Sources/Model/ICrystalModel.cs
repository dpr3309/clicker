using UniRx;
using UnityEngine;

namespace Clicker.Model
{
    internal interface ICrystalModel
    {
        public IReactiveProperty<ulong> Score { get; }
        public IReactiveCollection<Vector2> CrystalPositions { get; }
        public void ProcessPlayerPosition(Vector2 playerChipPosition);
    }
}