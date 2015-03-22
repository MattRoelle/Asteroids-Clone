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
    class Particle
    {
        public float X;
        public float Y;
        public float dX;
        public float dY;
        public float weight;
        public float gravityDirection;
        public float size;

        public ParticleEffect parent;

        public Color color;

        public Particle()
        {

        }

        public void Update()
        {
            dX += (float)Math.Cos(gravityDirection) * weight/10;
            dY += (float)Math.Sin(gravityDirection) * weight/10;
            X += dX;
            Y += dY;
        }

        public void Render()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            GL.Vertex2(parent.X + X + size / 2, parent.Y + Y + size / 2);
            GL.Vertex2(parent.X + X - size / 2, parent.Y + Y + size / 2);
            GL.Vertex2(parent.X + X - size / 2, parent.Y + Y - size / 2);
            GL.Vertex2(parent.X + X + size / 2, parent.Y + Y - size / 2);
            GL.End();
        }
    }
}
