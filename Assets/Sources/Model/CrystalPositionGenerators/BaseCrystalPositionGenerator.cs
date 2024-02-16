using System.Collections.Generic;
using System.Linq;
using Clicker.Tools.SelectionAlgorithms;
using UnityEngine;

namespace Clicker.Model
{
    internal abstract class BaseCrystalPositionGenerator : ICrystalPositionGenerator
    {
        private readonly ISelector _selector;

        protected BaseCrystalPositionGenerator(ISelector selector)
        {
            _selector = selector;
        }

        public IReadOnlyCollection<Vector2> GenerateCrystalPositions(IEnumerable<Vector2> availablePositions)
        {
            return _selector.SelectItems(availablePositions).ToList().AsReadOnly();
        }
    }
}