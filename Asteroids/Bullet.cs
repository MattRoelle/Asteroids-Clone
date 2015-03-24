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

        private Int64 birthTicks;

        public Bullet(float sX, float sY, float sTheta)
        {
            speed = 1f;
            topSpeed = 5f;
            accelerating = true;
            X = sX;
            Y = sY;
            theta = sTheta;

            birthTicks = Asteroids.Ticks;
        }

        public override void Render()
        {
            base.Render();

            GL.PushMatrix();

            GL.Translate(X, Y, 0);

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.White);
            GL.Vertex2(1.25f, 1.25f);
            GL.Vertex2(-1.25f, 1.25f);
            GL.Vertex2(-1.25f, -1.25f);
            GL.Vertex2(1.25f, -1.25f);

            GL.End();

            GL.PopMatrix();
        }

        public override void Update()
        {
            base.Update();

            if (birthTicks + 100 < Asteroids.Ticks)
            {
                this.dead = true;
            }

            Random r = new Random();
            if (r.Next(0, 100) > 70)
            {
                Asteroids.EntitiesToSpawn.Add(new Trail()
                {
                    X = X,
                    Y = Y
                });
            }
        }
    }
}
