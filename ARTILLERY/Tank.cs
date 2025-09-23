using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ARTILLERY
{
    internal class Tank
    {

        public Vector2 Position;
        public Vector2 Direction;
        public Vector2 size = new Vector2(25, 50);
        public float angle = 0.0f;
        //liikutus napit
        public KeyboardKey Left;
        public KeyboardKey Right;
       

        public Tank(Vector2 TankPos)
        {
            Direction = new Vector2(1, 0);
            Position = TankPos;
        }

        public void Update()
        {
            //kääntäminen
            float turnSpeed = 1 *Raylib.DEG2RAD;

            //Pelaaja1
            if (Raylib.IsKeyDown(Right))
            {

                angle += turnSpeed + Raylib.GetFrameTime();
            }
            else if (Raylib.IsKeyDown(Left))
            {
                angle -= turnSpeed + Raylib.GetFrameTime();
            }

           

            //Matrix
            Matrix3x2 rotationMatrix = Matrix3x2.CreateRotation(angle);
            Direction = Vector2.Transform(Vector2.UnitX, rotationMatrix);
        }
        public void DrawTank()
        {
            Vector2 TurretPos = Position;
            Vector2 TopLeft = Position - size;

            Raylib.DrawRectangleV(TopLeft, size, Color.White);
            Raylib.DrawLineV(Position, Position + Direction * 100f, Color.Magenta);
        }
    }
}
