using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace Asteroids
{
    class Explosion : ParticleEffect
    {
        public Explosion() : base()
        {
            this.liveTime = 200;
            Random r = new Random();
            int nParticles = r.Next(25, 40);
            for (int i = 0; i < nParticles; i++)
            {
                particles.Add(new Particle()
                {
                    parent = this,
                    size = (float)(r.NextDouble() * 3f) + 0.5f,
                    dX = (float)((r.NextDouble() * 2) - 1)*8,
                    dY = (float)((r.NextDouble() * 2) - 1)*-6,
                    weight = 0f,
                    gravityDirection = (float)Math.PI / 2f,
                    color = (r.NextDouble() > 0.5) ? Color.Red : (r.NextDouble() > 0.5) ? Color.Yellow : Color.White
                });
            }
        }
    }
}
