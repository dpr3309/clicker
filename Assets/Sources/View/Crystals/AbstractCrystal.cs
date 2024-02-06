using UnityEngine;

namespace Clicker.View
{
    public abstract class AbstractCrystal : MonoBehaviour
    {
        public abstract Vector2 Position { get; protected set; }

        internal abstract void Setup(Vector2 position);
        internal abstract void Show();
        internal abstract void Hide();
    }
}