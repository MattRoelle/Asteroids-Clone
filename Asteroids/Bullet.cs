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
    class Bullet : Entity
    {

        private Int64 BirthTicks;

        public Bullet(float sX, float sY, float sTheta)
        {
            speed = 1f;
            topSpeed = 5f;
            accelerating = true;
            X = sX;
            Y = sY;
            theta = sTheta;

            BirthTicks = Asteroids.Ticks;
        }

        public override void Render()
        {
            base.Render();

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.White);
            GL.Vertex2(X + 1.25f, Y + 1.25f);
            GL.Vertex2(X - 1.25f, Y + 1.25f);
            GL.Vertex2(X - 1.25f, Y - 1.25f);
            GL.Vertex2(X + 1.25f, Y - 1.25f);

            GL.End();
        }

        public override void Update()
        {
            base.Update();

            if (BirthTicks + 100 < Asteroids.Ticks)
            {
                this.dead = true;
            }
        }
    }
}
