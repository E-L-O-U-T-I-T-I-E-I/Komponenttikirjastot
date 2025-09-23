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
                    //Osuvatko pallot toisiinsa tai tankkeihin
                    foreach (Ball Ammukset in pallot)
                    {
                        if (Raylib.CheckCollisionRecs(Wall, Ammukset.rectangle()))
                        {
                            Ammukset.Direction *= -1;
                        }

                    }
                    
                }
                
                //Osuuko pallot pelaajiin tai toisiinsa.
                if (Raylib.CheckCollisionRecs(Ball1.rectangle(), Ball2.rectangle()))
                {
                    MoveBallOut(Ball1);
                    MoveBallOut(Ball2);
                    //Pallot osuivat toisiinsa
                }
                if (Raylib.CheckCollisionRecs(Ball1.rectangle(), player2.rectangle()))
                {
                    MoveBallOut(Ball1);
                    //Pallo osui pelaajaan 2
                }
                if (Raylib.CheckCollisionRecs(Ball2.rectangle(), player1.rectangle()))
                {
                    MoveBallOut(Ball2);
                    //Pallo osui pelaajaan 1
                }

                if (Raylib.CheckCollisionRecs(player1.rectangle(), player2.rectangle()))
                {
                    player1.Position -= player1.Direction;
                    player2.Position -= player2.Direction;
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

        public static void MoveBallOut(Ball pallo)
        {
            pallo.Position.X = -1000;
            pallo.Direction = Vector2.Zero;
        }
    }
}
