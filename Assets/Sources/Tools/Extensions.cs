using UnityEngine;

namespace Clicker.Tools
{
    public static class Extensions
    {
        public static void GenerateSingleItemInCell(this ParticleSystem ps, Vector2 position)
        {
            var emitParams = new ParticleSystem.EmitParams() { position = position };
            emitParams.rotation3D = new Vector3(90, 0, 0);
            ps.Emit(emitParams, 1);
        }

        public static T Clone<T>(this T origin, bool setAcitve = true) where T : Component
        {
            T item = GameObject.Instantiate(origin, origin.transform.parent) as T;
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
            item.gameObject.SetActive(setAcitve);
            return item;
        }
    }
}