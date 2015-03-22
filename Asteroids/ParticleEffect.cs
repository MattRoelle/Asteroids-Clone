using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Asteroids
{

    class ParticleEffect : Entity
    {
        public List<Particle> particles = new List<Particle>();

        public Int64 birthTicks;
        public int liveTime;

        public ParticleEffect()
        {
            birthTicks = Asteroids.Ticks;
        }

        public override void Update()
        {
            base.Update();

            if (Asteroids.Ticks - birthTicks > liveTime)
                this.dead = true;
            else
            {
                foreach (Particle particle in particles)
                {
                    particle.Update();
                }
            }
        }

        public override void Render()
        {
            base.Render();
            foreach (Particle particle in particles)
            {
                particle.Render();
            }
        }
    }

}
