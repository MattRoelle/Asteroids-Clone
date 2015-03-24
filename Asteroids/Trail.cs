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
    class Trail : ParticleEffect
    {
        public Trail() : base()
        {
            this.liveTime = 400;
            this.accelerating = false;

            Random r = new Random();

            particles.Add(new Particle()
            {
                parent = this,
                color = Color.Ivory,
                weight = (float)(r.NextDouble()*2),
                gravityDirection = (float)Math.PI / 2,
                size = (float)(r.NextDouble())
            });
        }
    }
}
