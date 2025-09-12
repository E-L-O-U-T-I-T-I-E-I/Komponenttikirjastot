using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    internal class Tank
    {
        public Vector2 Position;
        public Vector2 Direction;
        public float speed;
        public Color color = Color.Blue;

        public static Vector2 Tanksize = new Vector2(100, 100); //koko
        public static Vector2 TurretSize = new Vector2(25, 25);   //piipun koko
        

        public KeyboardKey Up;
        public KeyboardKey Left;
        public KeyboardKey Right;
        public KeyboardKey Down;
        public KeyboardKey Shoot;
        
        public Tank(Vector2 StartPos)
        {
            Direction = new Vector2(0, 1);
            Position = StartPos;
        }

        public void Update(Vector2 WallPos1, Vector2 WallPos2, Vector2 WallSize)
        {
            speed = 0;
            if (Raylib.IsKeyDown(Up))
            {
                Direction.X = 0;
                Direction.Y = -1;

                speed = 70;
            }
            else if (Raylib.IsKeyDown(Down))
            {
                Direction.X = 0;
                Direction.Y = 1;
                speed = 70;
            }
            else if (Raylib.IsKeyDown(Left))
            {
                Direction.Y = 0;
                Direction.X = -1;
                speed = 70;
            }
            else if (Raylib.IsKeyDown(Right))
            {
                Direction.Y = 0;
                Direction.X = 1;
                speed = 70;
            }

            
            //Pidetään pelaaja 1 kentän sisällä
            Position.X = Math.Clamp(Position.X, Tanksize.X / 2, Raylib.GetScreenWidth() - Tanksize.X / 2);
            Position.Y = Math.Clamp(Position.Y, Tanksize.Y / 2, Raylib.GetScreenHeight() - Tanksize.Y / 2);

            if (CheckTankPos(Position, Tanksize, WallPos2, WallPos1, WallSize) == true)
            {
                Position -= Direction;
            }
            else
            {
                Position += Direction * speed * Raylib.GetFrameTime();
            }
        }
        static public bool CheckTankPos(Vector2 Position, Vector2 TankSize, Vector2 WallPos1, Vector2 WallPos2, Vector2 WallSize)
        {

            Vector2 nextPos = Position - TankSize / 2;
            Console.WriteLine(Position);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(nextPos);
            Console.ForegroundColor = ConsoleColor.White;

            bool InWall = false;

            Rectangle Tank = new Rectangle(nextPos, TankSize); //player
            Rectangle Wall1 = new Rectangle(WallPos1, WallSize);
            Rectangle Wall2 = new Rectangle(WallPos2, WallSize);

            if (Raylib.CheckCollisionRecs(Tank, Wall1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Seinässä!");
                Console.ForegroundColor = ConsoleColor.White;

                InWall = true;
            }
            else if (Raylib.CheckCollisionRecs(Tank, Wall2))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Seinässä!");
                Console.ForegroundColor = ConsoleColor.White;

                InWall = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" EI ole seinässä!");
                Console.ForegroundColor = ConsoleColor.White;

                InWall = false;
            }





            return InWall;
        }

        
        public  void DrawTank()
        {
            Vector2 TurretPos = Position + Direction * (Tanksize.X / 2 + TurretSize.X / 2);
            Vector2 TurretTopLeft = TurretPos - TurretSize / 2;

            Raylib.DrawRectangleV(Position - Tanksize / 2, Tanksize, color);//Tankki
            Raylib.DrawRectangleV(TurretTopLeft, TurretSize, Color.Black); //piippu

        }
        public Rectangle rectangle()
        {
            return new Rectangle(Position, Tanksize);
        }
    }
}
