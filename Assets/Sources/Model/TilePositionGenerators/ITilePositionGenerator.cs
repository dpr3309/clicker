using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    internal interface ITilePositionGenerator
    {
        IReadOnlyCollection<Vector2> GenerateLaunchPadPositions(Vector2 directionModifier);
        IReadOnlyCollection<Vector2> GeneratePositions();
    }
}