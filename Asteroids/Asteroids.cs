using System;
using System.Drawing;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Asteroids
{
    class Asteroids
    {
        public static List<Entity> Entities = new List<Entity>();

        public static float GameWidth = 400f;
        public static float GameHeight = 300f;

        private static Random random = new Random();

        public static Int64 Ticks = 0;

        [STAThread]
        public static void Main()
        {
            using (var game = new GameWindow())
            {
                Entities.Add(new Player(game));

                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;

                    // Initialize
                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    GL.Ortho(0.0, 400.0, 300.0, 0.0, 0.0, 4.0);
                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                game.UpdateFrame += (sender, e) =>
                {
                    Ticks++;

                    if (random.NextDouble() < 0.01)
                        Entities.Add(new Asteroid());

                    // add game logic, input handling
                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }

                    foreach (Entity entity in Entities)
                    {
                        if (!entity.dead)
                            entity.Update();
                    }


                    Entities.RemoveAll((entity) => entity.dead);
                };

                game.RenderFrame += (sender, e) =>
                {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    foreach (Entity entity in Entities)
                    {
                        entity.Render();
                    }

                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}