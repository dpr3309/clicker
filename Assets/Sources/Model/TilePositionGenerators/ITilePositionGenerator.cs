using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Model
{
    public interface ITilePositionGenerator
    {
        IReadOnlyCollection<Vector3> GenerateLaunchPadPositions();
        IReadOnlyCollection<Vector3> GeneratePositions();
    }
}