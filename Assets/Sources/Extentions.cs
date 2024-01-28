using UnityEngine;

public static class Extentions
{
    public static void GenerateSingleItemInCell(this ParticleSystem ps, Vector2 position)
    {
        var emitParams = new ParticleSystem.EmitParams() { position = position };
        ps.Emit(emitParams, 1);
    }
}
