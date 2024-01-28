using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    internal interface IFieldModel
    {
        public IReadOnlyList<Vector3> TileInstances { get; }
        public void ReleaseTraversedObjects(Vector3 playerChipPosition);
        public void CheckTilesCount();
        void ProcessPlayerPosition(Vector3 playerChipPosition);
    }
}