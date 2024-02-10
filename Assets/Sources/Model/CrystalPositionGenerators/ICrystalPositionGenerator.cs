using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    internal interface ICrystalPositionGenerator
    {
        IReadOnlyCollection<Vector2> GenerateCrystalPositions(IEnumerable<Vector2> availablePositions);
    }
}