using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public static class Extentions
{
    public static void GenerateSingleItemInCell(this ParticleSystem ps, Vector2 position)
    {
        var emitParams = new ParticleSystem.EmitParams() { position = position };
        emitParams.rotation3D = new Vector3(90, 0, 0);
        ps.Emit(emitParams, 1);
    }

    public static void HideSingleItemInCell(this ParticleSystem ps, Vector2 position)
    {
        // todo: implement this
        var emitParams = new ParticleSystem.EmitParams() { position = position };
        emitParams.startColor = new Color32(255, 255, 255, 0);
        ps.Emit(emitParams, 1);
    }

    public static List<Vector2> SelectTraversedObject(this IReactiveCollection<Vector2> instances,
        Vector2 playerChipCoordinates, float offset)
    {
        return instances.Where(i => i.x < playerChipCoordinates.x + offset || i.y < playerChipCoordinates.y + offset)
            .ToList();
    }
}