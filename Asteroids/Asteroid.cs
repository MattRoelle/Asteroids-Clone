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
        public int grain;
        public float[][] points;

        public Asteroid()
        {
            Random r = new Random();
            // Initial Position
            X = (float)r.NextDouble() * Asteroids.GameWidth;
            Y = (float)r.NextDouble() * Asteroids.GameHeight;

            // Size
            size = ((float)r.NextDouble() * 15f) + 7.5f;
            theta = (float)(r.NextDouble() * Math.PI * 2);
            grain = r.Next(6, 10);
            points = new float[grain][];
            for (int i = 0; i < grain; i++)
            {
                float angle = (((float)i + 1f) / grain) * (float)Math.PI * 2f;
                float alteredSize = size + (float)((r.NextDouble() * 6) - 2);
                points[i] = new float[2] { (float)Math.Cos(angle)*alteredSize, (float)Math.Sin(angle)*alteredSize };
            }

                // Speed
                accelerating = true;
            topSpeed = (float)(r.NextDouble() * 3) + 1f;
            speed = topSpeed;
        }

        public override void Render()
        {
           base.Render();

           GL.PushMatrix();
           GL.Translate(X, Y, 0);
           GL.Rotate(Asteroids.Ticks / (size / 4), 0, 0, 1);

           GL.Begin(PrimitiveType.LineStrip);
           GL.Color3(Color.Ivory);
           for (int i = 0; i < grain; i++)
           {
               GL.Vertex2(points[i][0], points[i][1]);
           }
           GL.Vertex2(points[0][0], points[0][1]);
           GL.End();

           GL.PopMatrix();
        }

        public override void Update()
        {
            base.Update();

            foreach(Bullet bullet in Asteroids.Entities.FindAll((e) => e is Bullet))
            {
                if (Math.Sqrt(Math.Pow(bullet.X - X, 2) + Math.Pow(bullet.Y - Y, 2)) < size)
                {
                    this.dead = true;
                    bullet.dead = true;
                    Asteroids.EntitiesToSpawn.Add(new Explosion()
                    {
                        X = X,
                        Y = Y
                    });
                }
            }
        }
    }
}
