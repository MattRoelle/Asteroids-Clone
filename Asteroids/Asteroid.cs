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

        public bool spawning;

        public Int64 spawnTime = 95;
        public Int64 birthTicks;

        public Asteroid(bool initialSpawn)
        {
            birthTicks = Asteroids.Ticks;

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
            topSpeed = (float)(r.NextDouble() * 3) + 1f;
            speed = 0f;

            if (initialSpawn)
            {
                spawning = true;
            }
            else
            {
                spawning = false;
                accelerating = true;
                speed = topSpeed;
            }
        }

        public override void Render()
        {
           base.Render();

           GL.PushMatrix();
           GL.Translate(X, Y, 0);
           GL.Rotate(Asteroids.Ticks / (size / 4), 0, 0, 1);
           if (spawning)
           {
               float x = ((float)(Asteroids.Ticks - birthTicks) / (float)spawnTime) * 3;
               float scale = 2.2f * (float)((Math.Pow(x, 3) + Math.Pow(x, 2)) / (Math.Pow(x, 4) + x));
               GL.Scale(scale, scale, 1);
           }

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

            if (spawning && Asteroids.Ticks - birthTicks > spawnTime)
            {
                spawning = false;
                accelerating = true;
            }

            if (!spawning)
            {
                foreach (Bullet bullet in Asteroids.Entities.FindAll((e) => e is Bullet))
                {
                    if (Math.Sqrt(Math.Pow(bullet.X - X, 2) + Math.Pow(bullet.Y - Y, 2)) < size*1.35f)
                    {
                        this.dead = true;
                        bullet.dead = true;
                        Asteroids.EntitiesToSpawn.Add(new Explosion()
                        {
                            X = X,
                            Y = Y
                        });

                        Random r = new Random();
                        if (size > 7f)
                        {
                            for (int i = 0; i < r.Next(2, 3); i++)
                            {
                                Asteroids.EntitiesToSpawn.Add(new Asteroid(false)
                                {
                                    X = X,
                                    Y = Y,
                                    size = 7f - (float)(r.NextDouble() * 2),
                                    theta = (float)(r.NextDouble() * Math.PI * 2)
                                });
                            }
                        }
                    }
                } 
            }
        }
    }
}
