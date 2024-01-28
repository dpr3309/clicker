using UnityEngine;

namespace Clicker.Model
{
    internal class PlayerChipModel : IPlayerChipModel
    {
        public float Speed { get; }
        public Vector2 Position { get; }
        public void UpdatePosition(Vector2 newPosition)
        {
            throw new System.NotImplementedException();
        }
    }
}