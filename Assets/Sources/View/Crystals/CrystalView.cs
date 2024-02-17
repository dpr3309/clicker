using UnityEngine;

namespace Clicker.View
{
    public sealed class CrystalView : AbstractCrystal
    {
        internal override void Setup(Vector2 position)
        {
            transform.position = new Vector3(position.x, 0.75f, position.y);
        }

        internal override void Show()
        {
            gameObject.SetActive(true);
        }

        internal override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}