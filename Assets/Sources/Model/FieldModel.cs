using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    internal class FieldModel : IFieldModel
    {
        public IReadOnlyList<Vector2> TileInstances { get; }

        public void ReleaseTraversedObjects(Vector2 playerChipPosition)
        {
            throw new System.NotImplementedException();
        }

        public void CheckTilesCount()
        {
            throw new System.NotImplementedException();
        }

        public void ProcessPlayerPosition(Vector2 playerChipPosition)
        {
            ReleaseTraversedObjects(playerChipPosition);
            CheckTilesCount();
        }
    }
}