using UnityEngine;

namespace Clicker.Factories
{
    public class FloorFactory : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem floorParticle;
        [SerializeField] 
        private ParticleSystem floorParticle2;

        private ParticleSystem currentParticleSystem;

        void Start()
        {
            SwitchParticle();
        }

        private void SwitchParticle()
        {
            currentParticleSystem = (currentParticleSystem == floorParticle) ? floorParticle2 : floorParticle;
            currentParticleSystem.Clear();
        }

        public void GenerateItemInPosition(Vector3 position)
        {
            currentParticleSystem.GenerateSingleItemInCell(position);
            if (currentParticleSystem.particleCount > 60)
            {
                SwitchParticle();
            }
        }

        public void RemoveItemInPosition(Vector3 position)
        {
            throw new System.NotImplementedException();
        }
    }

}
