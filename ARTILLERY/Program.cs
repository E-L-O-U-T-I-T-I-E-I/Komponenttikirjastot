using Raylib_cs;
using System.Numerics;

namespace ARTILLERY
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int WindowHeight = 1000;
            const int WindowWidth = 1000;

            //Kääntö testiin
            Tank player1 = new Tank(new Vector2(WindowWidth / 10, WindowHeight - 100));
            Tank player2 = new Tank(new Vector2(WindowWidth-100, WindowHeight - 100));

            Ball ball1 = new Ball(player1.Position);
            Ball ball2 = new Ball(player2.Position);

            //Pelaaja1
            player1.Left = KeyboardKey.Left;
            player1.Right = KeyboardKey.Right;

            //Pelaaja2
            player2.Left = KeyboardKey.A;
            player2.Right = KeyboardKey.D;

            Raylib.SetTargetFPS(60);
            Raylib.InitWindow(WindowWidth, WindowHeight, "ARTILLERY");

            while (Raylib.WindowShouldClose() == false)
            {
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.SkyBlue);

                //Tykit
                player1.DrawTank();
                player2.DrawTank();
                ball1.DrawBall();
                ball2.DrawBall();

                Raylib.EndDrawing();

                player1.Update();
                player2.Update();
                ball1.Update(player1.Direction);
                ball2.Update(player2.Direction);

                
            }
            Raylib.CloseWindow();
        }
        
        
    }
}
