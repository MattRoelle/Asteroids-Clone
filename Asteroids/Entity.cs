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
    abstract class Entity
    {
        public float X = Asteroids.GameWidth/2;
        public float Y = Asteroids.GameHeight/2;

        public float speed = 0f;
        public float topSpeed = 2.3f;
        public float acceleration = 0.2f;
        public float decelleration = 0.94f;

        public float size = 3f;

        public float theta = 0f;
        public float dTheta = 0f;

        public bool wrapped = true;
        public bool accelerating = false;
        public bool dead = false;

        private void UpdateMovement()
        {
            if (accelerating)
            {
                speed += acceleration;

                if (speed > topSpeed)
                {
                    speed = topSpeed;
                }
            }
            else
            {
                if (speed != 0)
                {
                    speed *= decelleration;
                    if (speed < 0.1f)
                    {
                        speed = 0f;
                    }
                }
            }

            if (speed != 0)
            {
                X += (float)Math.Cos(theta) * speed;
                Y += (float)Math.Sin(theta) * speed;
            }

            // Wrap movement around the screen
            if (wrapped)
            {
                if (X < -size*2) { X = Asteroids.GameWidth; }
                if (X > Asteroids.GameWidth + size*2) { X = 0f; }
                if (Y < -size*2) { Y = Asteroids.GameHeight; }
                if (Y > Asteroids.GameHeight + size*2) { Y = 0f; }
            }
        }

        private void UpdateAngle()
        {
            theta += dTheta;
        }

        public virtual void Update()
        {
            UpdateAngle();
            UpdateMovement();
        }

        public virtual void Render()
        {

        }

        public virtual void Reset()
        {
            dead = true;
        }
    }
}
