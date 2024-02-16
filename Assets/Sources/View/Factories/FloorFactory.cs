using System.Collections.Generic;
using Clicker.Tools;
using UnityEngine;

namespace Clicker.Factories
{
    public class FloorFactory : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem floorParticle;

        public void GenerateItemInPosition(Vector2 position)
        {
            floorParticle.GenerateSingleItemInCell(position);
        }

        public void OnItemRemoved(IEnumerable<Vector2> items)
        {
            floorParticle.Clear();
            foreach (var item in items)
            {
                GenerateItemInPosition(item);
            }
        }
    }
}
