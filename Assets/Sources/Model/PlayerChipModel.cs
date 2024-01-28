using UnityEngine;

namespace Clicker.Model
{
    internal class PlayerChipModel : IPlayerChipModel
    {
        public float Speed { get; }
        public Vector3 Position { get; private set; }

        public void UpdatePosition(Vector3 newPosition)
        {
            Position = newPosition;
        }
    }
}