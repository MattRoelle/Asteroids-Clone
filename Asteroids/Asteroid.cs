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
    class Asteroid : Entity
    {
        public Asteroid()
        {
            Random r = new Random();
            X = (float)r.NextDouble() * Asteroids.GameWidth;
            Y = (float)r.NextDouble() * Asteroids.GameHeight;
            size = ((float)r.NextDouble() * 15f) + 7.5f;
            theta = (float)(r.NextDouble() * Math.PI * 2);
            accelerating = true;
            topSpeed = (float)(r.NextDouble() * 3) + 1f;
            speed = topSpeed;
        }

        public override void Render()
        {
           base.Render();

           GL.Begin(PrimitiveType.Quads);

           GL.Color3(Color.Ivory);
           GL.Vertex2(X + size, Y + size);
           GL.Vertex2(X - size, Y + size);
           GL.Vertex2(X - size, Y - size);
           GL.Vertex2(X + size, Y - size);

           GL.End();
        }

        public override void Update()
        {
            base.Update();

            foreach(Bullet bullet in Asteroids.Entities.FindAll((e) => e is Bullet))
            {
                Rect r1 = new Rect(bullet.X, bullet.Y, 0.5f, 0.5f);
                Rect r2 = new Rect(X - size/2, Y - size/2, size, size);
                if (r1.Intersects(r2))
                {
                    this.dead = true;
                    bullet.dead = true;
                }
            }
        }
    }
}
