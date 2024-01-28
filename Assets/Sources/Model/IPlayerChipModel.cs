using UnityEngine;

namespace Clicker.Model
{
    internal interface IPlayerChipModel
    {
        public float Speed { get; }
        public Vector3 Position { get; }
        public void UpdatePosition(Vector3 newPosition);
    }
}