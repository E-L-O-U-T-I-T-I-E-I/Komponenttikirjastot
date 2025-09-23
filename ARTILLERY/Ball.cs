using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ARTILLERY
{
    internal class Ball
    {
        public Vector2 Position;
        private Vector2 startPos;
        public Vector2 Velocity;
        public Vector2 Direction;
        public Vector2 Size;
        public float Acceleration;

        public Ball(Vector2 startPos)
        {
            this.startPos = startPos;
            Velocity = new Vector2(0,0);
            Direction = new Vector2(0,-1);
            Acceleration = 10.0f;
            Size = new Vector2(25,25);
        }

        public void Update(Vector2 playerDirect)
        {

            float deltaTime = Raylib.GetFrameTime();
            //Pysyy samana ja osottaa alaspäin
            Vector2 gravity = new Vector2(0,1);

            

            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                Direction = playerDirect;
                Velocity = Direction * 100;
                Position = startPos;
            }

            Vector2 BallAcceleration =gravity*25;

            Velocity += BallAcceleration * deltaTime;
            Position += Velocity * deltaTime;
        }

        public void DrawBall()
        {
            Raylib.DrawCircleV(Position, Size.X, Color.Violet);
        }

    }
}
