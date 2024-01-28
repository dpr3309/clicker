using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    internal class FieldModel : IFieldModel
    {
        public IReadOnlyList<Vector3> TileInstances { get; private set; }

        public void ReleaseTraversedObjects(Vector3 playerChipPosition)
        {
            throw new System.NotImplementedException();
        }

        public void CheckTilesCount()
        {
            throw new System.NotImplementedException();
        }

        public void ProcessPlayerPosition(Vector3 playerChipPosition)
        {
            ReleaseTraversedObjects(playerChipPosition);
            CheckTilesCount();
        }
    }
}