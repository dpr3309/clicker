using UnityEngine;

namespace Clicker.View
{
    public abstract class AbstractCrystal : MonoBehaviour
    {
        internal abstract void Setup(Vector2 position);
        internal abstract void Show();
        internal abstract void Hide();
    }
}