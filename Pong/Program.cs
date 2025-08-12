using Raylib_cs;
using System.Numerics;

namespace Pong
{
    internal class Program
    {

       
        static void Main(string[] args)
        {
            int WindowWidth = 1000;
            int WindowHeight = 900;

            //Pelaajien tiedot
            Vector2 Pos1 = new Vector2(50, 200);
            Vector2 Pos2 = new Vector2(880, 200);
            
            //´Pisteiden tiedot
            int Points1 = 0;
            int Points2 = 0;

            Vector2 Points1Pos = new Vector2(450, 0);
            Vector2 Points2Pos = new Vector2(650, 0);

            //Mailan tiedot
            float RecSpeed = 10.0f;
            int RecWidth = 70;
            int RecHeight = 400;

            //Pallon tiedot
            Vector2 Ball = new Vector2(WindowWidth/2, WindowHeight/2);
            Vector2 BallDirection = new Vector2(1, 1);
            float BallSpeed = 100.0f;

            Raylib.InitWindow(WindowWidth, WindowHeight, "Pong");


            while (Raylib.WindowShouldClose() == false)
            {
                Ball += BallDirection * BallSpeed * Raylib.GetFrameTime();

                if (Raylib.IsKeyPressed(KeyboardKey.W))
                {
                    Pos1 += new Vector2(0,-50);
                }
                if (Raylib.IsKeyPressed(KeyboardKey.S))
                {
                    Pos1 += new Vector2(0, 50);
                }
                if (Raylib.IsKeyPressed(KeyboardKey.Up))
                {
                    Pos2 += new Vector2(0, -50);
                }
                if (Raylib.IsKeyPressed(KeyboardKey.Down))
                {
                    Pos2 += new Vector2(0, 50);
                }

                if (Ball.X + 35.0f > Pos2.X)
                {
                    BallDirection = new Vector2(BallDirection.X *-1, BallDirection.Y);
                }
                
                if(Ball.X <= RecWidth + 35)
                {
                    BallDirection = new Vector2(BallDirection.X *-1, BallDirection.Y);
                }
                if (Ball.Y + 35.0f > WindowHeight)
                {
                    BallDirection = new Vector2(BallDirection.X, BallDirection.Y*-1);
                }
                if (Ball.Y <= 35)
                {
                    BallDirection = new Vector2(BallDirection.X, BallDirection.Y * -1);
                }
                if (Ball.X >= WindowWidth - RecWidth)
                {
                    BallDirection = new Vector2(BallDirection.X*-1, BallDirection.Y);
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                //Piirretään pelaaja1 ja sen pisteet
                Raylib.DrawRectangle((int)Pos1.X, (int)Pos1.Y, RecWidth, RecHeight, Color.Lime);
                //Raylib.DrawText("moi", (int)Points1Pos.X, (int)Points1Pos.Y, 20, Color.Brown);

                //Piirretään pelaaja2
                Raylib.DrawRectangle((int)Pos2.X, (int)Pos2.Y, RecWidth, RecHeight, Color.Orange);

                //Piirretään pallo
                Raylib.DrawCircle((int)Ball.X, (int)Ball.Y, 35.0f, Color.Red);

               

                Raylib.EndDrawing();
            }

        }
    }
}
