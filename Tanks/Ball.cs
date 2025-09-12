using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    internal class Ball
    {
        public Vector2 Position;
        public Vector2 Direction;
        public Color color = Color.Pink;

        public static Vector2 BallSize = new Vector2(20, 20);
        public static float BallSpeed = 50f;

        public Ball(Vector2 StartPos)
        {
            Position = StartPos;
            Direction = new Vector2(0,0);
        }

        public void Update()
        {
            
            Position += Direction * BallSpeed * Raylib.GetFrameTime();
        }

       

        public Rectangle rectangle()
        {
            return new Rectangle(Position, BallSize);
        }

        public void DrawBall()
        {
            Raylib.DrawCircleV(Position, BallSize.Y, color);
        }
    }
}
