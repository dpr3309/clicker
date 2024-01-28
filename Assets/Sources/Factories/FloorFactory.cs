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

        private bool t;
        private float x = 0;
        private int counter;

        void Start()
        {
            SwitchParticle();
        }
        
        private void Update()
        {
            if (!t)
            {
                for (int y = 0; y < 30; y++)
                {
                    var itemPos = new Vector2(x, y);
                    currentParticleSystem.GenerateSingleItemInCell(itemPos);
                }

                if (currentParticleSystem.particleCount > 60)
                {
                    SwitchParticle();
                }

                
                t = true;
                x++;
            }
            else
            {
                if (counter++ > 300)
                {
                    t = !t;
                    counter = 0;
                }
            }

        }

        private void SwitchParticle()
        {
            currentParticleSystem = (currentParticleSystem == floorParticle) ? floorParticle2 : floorParticle;
            currentParticleSystem.Clear();
        }

        
    }

}
