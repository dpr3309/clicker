using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    internal interface IFieldModel
    {
        public IReadOnlyList<Vector2> TileInstances { get; }
        public void ReleaseTraversedObjects(Vector2 playerChipPosition);
        public void CheckTilesCount();
        void ProcessPlayerPosition(Vector2 playerChipPosition);
    }
}