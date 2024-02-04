using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    public interface ITilePositionGenerator
    {
        IReadOnlyCollection<Vector2> GenerateLaunchPadPositions();
        IReadOnlyCollection<Vector2> GeneratePositions();
    }
}