using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Clicker.Model.Tools
{
    internal static class ModelExtensions
    {
        public static List<Vector2> SelectTraversedObject(this IReadOnlyReactiveCollection<Vector2> instances,
            Vector2 playerChipCoordinates, float offset = 0)
        {
            return instances
                .Where(i => Mathf.Abs(i.x) + offset < Mathf.Abs(playerChipCoordinates.x) || Mathf.Abs(i.y) + offset < Mathf.Abs(playerChipCoordinates.y))
                .ToList();
        }

        public static IEnumerable<Vector2> GetNeighbourhood(this Vector2 origin, int radius = 1)
        {
            /*
            [x-1,y+1]  [x,y+1]  [x+1,y+1]
            [x-1,y]    [x,y]    [x+1,y]
            [x-1,y-1]  [x,y-1]  [x+1,y-1]
            */
            for (int i = 1; i < radius + 1; i++)
            {
                yield return new Vector2(origin.x - i, origin.y + i);
                yield return new Vector2(origin.x, origin.y + i);
                yield return new Vector2(origin.x + i, origin.y + i);
                yield return new Vector2(origin.x - i, origin.y);
                yield return new Vector2(origin.x + i, origin.y);
                yield return new Vector2(origin.x - i, origin.y - i);
                yield return new Vector2(origin.x, origin.y - i);
                yield return new Vector2(origin.x + i, origin.y - i);
            }
        }
    }
}