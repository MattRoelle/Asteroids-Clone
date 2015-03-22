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
        public static List<Entity> EntitiesToSpawn = new List<Entity>();

        public static float GameWidth = 400f;
        public static float GameHeight = 300f;

        public static bool NeedsToReset = false;

        private static Random random = new Random();

        private static GameWindow game;

        public static Int64 Ticks = 0;

        [STAThread]
        public static void Main()
        {
            StartGame();
            ResetGame();
        }

        public static void StartGame()
        {
            game = new GameWindow();
            ResetGame();

            Entities.Add(new Player(game));

            game.Load += (sender, e) =>
            {
                // setup settings, load textures, sounds
                game.VSync = VSyncMode.On;

                // Initialize
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(0.0, GameWidth, GameHeight, 0.0, 0.0, 4.0);

                GL.Disable(EnableCap.DepthTest);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            };

            game.Resize += (sender, e) =>
            {
                GL.Viewport(0, 0, game.Width, game.Height);
            };

            game.UpdateFrame += (sender, e) =>
            {
                Ticks++;

                if (random.NextDouble() < 0.01 && Entities.FindAll((entity) => entity is Asteroid).Count < 3)
                    Entities.Add(new Asteroid(true));

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

                foreach (Entity entity in EntitiesToSpawn)
                {
                    Entities.Add(entity);
                }


                EntitiesToSpawn.RemoveAll((entity) => true);
                Entities.RemoveAll((entity) => entity.dead);

                if (NeedsToReset)
                {
                    ResetGame();
                }
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

        public static void ResetGame()
        {
            foreach(Entity entity in Entities)
            {
                entity.Reset();
            }
            NeedsToReset = false;
        }
    }
}