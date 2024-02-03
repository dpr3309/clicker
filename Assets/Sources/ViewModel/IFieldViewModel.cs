using UniRx;
using UnityEngine;

namespace Clicker.ViewModel
{
    public interface IFieldViewModel
    {
        public IReadOnlyReactiveCollection<Vector3> TileInstances { get; }
    }
}