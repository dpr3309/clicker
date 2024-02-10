using UniRx;
using UnityEngine;

namespace Clicker.Model
{
    public interface IFieldModel
    {
        public IReadOnlyReactiveCollection<Vector2> TileInstances { get; }
        void ProcessPlayerPosition(Vector2 playerChipPosition);
        void Startup();
    }
}