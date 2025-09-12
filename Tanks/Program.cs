using Raylib_cs;
using System.Net.Http.Headers;
using System.Numerics;

namespace Tanks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int Windowheight = 1000;
            const int Windowwidth = 1000;

            Raylib.InitWindow(Windowwidth, Windowheight, "Tanks!");

            //Pelaajat
            Tank player1 = new Tank(new Vector2(Windowwidth - 100, Windowheight/2)); //Pelaaja 1
            Tank player2 = new Tank(new Vector2(100, Windowheight / 2)); //Pelaaja 2

            //Pelaaja 1 liikutus napit
            player1.Up = KeyboardKey.Up;
            player1.Down = KeyboardKey.Down;
            player1.Left = KeyboardKey.Left;
            player1.Right = KeyboardKey.Right;
            player1.Shoot = KeyboardKey.Space;

            //Pelaaja 2 liikutus napit
            player2.Up = KeyboardKey.W;
            player2.Down = KeyboardKey.S;
            player2.Left = KeyboardKey.A;
            player2.Right = KeyboardKey.D;
            player2.Shoot = KeyboardKey.E;

            //Seinät
            Vector2 WallSize = new Vector2(75, Windowheight/3);
            Vector2 WallPos1 = new Vector2 (Windowwidth/4, 300);
            Vector2 WallPos2 = new Vector2(Windowwidth - 350, 300);


            Rectangle Wall1 = new Rectangle(WallPos1, WallSize);
            Rectangle Wall2 = new Rectangle(WallPos2, WallSize);

            //Listat seinille
            List<Rectangle> Walls = new List<Rectangle>() { Wall1, Wall2 };
            List<Vector2> WallPositions = new List<Vector2>() { WallPos1, WallPos2 };
            
            //Ammus 1
            Ball Ball1 = new Ball(player1.Position); 
            //Ammus 2
            Ball Ball2 = new Ball(player2.Position);

            List<Ball> pallot = new List<Ball>() { Ball1, Ball2};

            Raylib.SetTargetFPS(60);
            
            while(Raylib.WindowShouldClose() == false)
            {

                
                player1.Update(WallPos1, WallPos2, WallSize);
                player2.Update(WallPos1, WallPos2, WallSize);

                
                if (Raylib.IsKeyPressed(player1.Shoot))
                {
                    Ball1.Position = player1.Position;
                    Ball1.Direction = player1.Direction * 10;
                }
                Ball1.Update();
                if (Raylib.IsKeyPressed(player2.Shoot))
                {
                    Ball2.Position = player2.Position;
                    Ball2.Direction = player2.Direction * 10;
                }
                Ball2.Update();



                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Brown);

                foreach (Rectangle Wall in Walls)
                {
                    for (int i = 0; i < Walls.Count; i++)
                    {
                        if (Raylib.CheckCollisionRecs(Walls[i], Ball2.rectangle())) { Ball2.Direction *= -1; } //pallo2
                        if (Raylib.CheckCollisionRecs(Walls[i], Ball1.rectangle())) { Ball1.Direction *= -1; } //pallo1
                    }
                    break;
                }

                //Osuvatko pallot toisiinsa tai tankkeihin
                foreach(Ball Ammukset in pallot)
                {
                    if (Raylib.CheckCollisionRecs(Ball1.rectangle(), Ball2.rectangle()))
                    {
                        Ball1.Position = player1.Position;
                        Ball1.Direction = new Vector2(0, 0);
                        Ball2.Position = player2.Position;
                        Ball2.Direction = new Vector2(0, 0);
                    }
                    for (int i = 0; i < pallot.Count; i++)
                    {

                        if (Raylib.CheckCollisionRecs(Ball1.rectangle(), player2.rectangle()))
                        {
                            Ball1.Position = player1.Position;
                            Ball1.Direction = new Vector2(0, 0);
                        }
                        if (Raylib.CheckCollisionRecs(Ball2.rectangle(), player1.rectangle()))
                        {
                            Ball2.Position = player2.Position;
                            Ball2.Direction = new Vector2(0, 0);
                            
                        }
                    }
                    
                    break;
                }

                player1.DrawTank();
                Ball1.DrawBall();
                player2.DrawTank();
                Ball2.DrawBall();
                for (int i = 0;i < Walls.Count;i++)
                {
                    Raylib.DrawRectangleV(WallPositions[i], WallSize, Color.Black);
                }
                

                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();

            


        }

        
    }
}
