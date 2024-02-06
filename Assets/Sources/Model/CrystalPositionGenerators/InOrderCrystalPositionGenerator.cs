using System.Collections.Generic;
using System.Linq;
using Clicker.Tools.SelectionAlgorithms;
using UnityEngine;

namespace Clicker.Model
{
    internal sealed class InOrderCrystalPositionGenerator : ICrystalPositionGenerator
    {
        private InOrderItemSelector selector;

        public InOrderCrystalPositionGenerator()
        {
            selector = new InOrderItemSelector(5);
        }

        public IReadOnlyCollection<Vector2> GenerateCrystalPositions(IEnumerable<Vector2> availablePositions)
        {
            return selector.SelectItems(availablePositions).ToList().AsReadOnly();
        }
    }
}