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
    class Player : Entity
    {
        private Int64 lastShot = -1000;
        private Int64 fireDelay = 25;

        public Player(GameWindow game)
        {
            size = 10f;

            game.KeyDown += (sender, e) =>
            {
                switch (e.Key)
                {
                    case Key.W:
                        accelerating = true;
                        break;
                    case Key.A:
                        dTheta = -0.1f;
                        break;
                    case Key.D:
                        dTheta = 0.1f;
                        break;
                    case Key.Space:
                        Shoot();
                        break;
                }
            };

            game.KeyUp += (sender, e) =>
            {
                switch (e.Key)
                {
                    case Key.W:
                        accelerating = false;
                        break;
                    case Key.A:
                    case Key.D:
                        dTheta = 0f;
                        break;
                }
            };
        }

        public void Shoot()
        {
            if (Asteroids.Ticks - lastShot > fireDelay)
            {
                lastShot = Asteroids.Ticks;
                Asteroids.Entities.Add(new Bullet(X, Y, theta));
            }
        }

        public override void Render()
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);

            GL.Color4(1f, 1f, 1f, 0.5f);
            GL.Vertex2(X + Math.Cos(theta) * 5, Y + Math.Sin(theta) * 5);
            GL.Vertex2(X + Math.Cos(theta - (Math.PI * 2.4) / 2) * 8, Y + Math.Sin(theta - (Math.PI * 2.4) / 2) * 8);
            GL.Vertex2(X + Math.Cos(theta + (Math.PI * 2.4) / 2) * 8, Y + Math.Sin(theta + (Math.PI * 2.4) / 2) * 8);

            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);

            GL.End();
        }
    }
}
