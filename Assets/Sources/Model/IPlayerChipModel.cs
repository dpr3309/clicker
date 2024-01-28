using UnityEngine;

namespace Clicker.Model
{
    internal interface IPlayerChipModel
    {
        public float Speed { get; }
        public Vector2 Position { get; }
        public void UpdatePosition(Vector2 newPosition);
    }
}