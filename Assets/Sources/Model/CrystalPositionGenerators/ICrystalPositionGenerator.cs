using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    public interface ICrystalPositionGenerator
    {
        IReadOnlyCollection<Vector2> GenerateCrystalPositions(IEnumerable<Vector2> availablePositions);
    }
}