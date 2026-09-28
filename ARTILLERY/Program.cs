using Raylib_cs;
using System.Numerics;

namespace ARTILLERY
{
    internal class Program
    {
        enum GameState
        {
            Turn1,
            Turn2,
            Winner1,
            Winner2,

        }
        static void Main(string[] args)
        {
            const int WindowHeight = 1000;
            const int WindowWidth = 1000;

            Raylib.InitWindow(WindowWidth, WindowHeight, "ARTILLERY");

            Terrain maasto = new Terrain();


           

            //Kääntö testiin
            Vector2 TerrainEnd = maasto.GenerateTerrain();
            Tank player1 = new Tank(maasto.Maasto[maasto.Maasto.Count-2].Position + new Vector2(Tank.size.X / 2 + maasto.Maasto[1].Width / 2, 0));
            Tank player2 = new Tank(maasto.Maasto[1].Position + new Vector2(Tank.size.X / 2 + maasto.Maasto[1].Width / 2, 0));

            Ball ball1 = new Ball(player1.Position, player2.Position);
            
            Ball ball2 = new Ball(player2.Position, player1.Position);

            GameState state = GameState.Turn1;

            //Terrain maasto = new Terrain(50, new Vector2(0, WindowHeight-100));
            //Pelaaja1
            player1.Left = KeyboardKey.Left;
            player1.Right = KeyboardKey.Right;

            //Pelaaja2
            player2.Left = KeyboardKey.A;
            player2.Right = KeyboardKey.D;

            Raylib.SetTargetFPS(60);
            
            
            while (Raylib.WindowShouldClose() == false)
            {
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.SkyBlue);

                maasto.DrawTerrain();

                //Tykit
                player1.DrawTank();
                player2.DrawTank();
                if (state == GameState.Turn1)
                {
                    ball1.DrawBall();

                }
                else
                {
                    ball2.DrawBall();

                }


                if (state == GameState.Winner1)
                {
                    Raylib.DrawRectangle(0,0,WindowWidth, WindowHeight ,Color.DarkPurple);
                    Raylib.DrawText("Player 1 won the game\nPress 'enter' to continue", WindowWidth/4, WindowHeight/2, 50, Color.White);
                    if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                    {
                        TerrainEnd = maasto.GenerateTerrain();
                        player1 = new Tank(maasto.Maasto[maasto.Maasto.Count - 2].Position + new Vector2(Tank.size.X / 2 + maasto.Maasto[1].Width / 2, 0));
                        player2 = new Tank(maasto.Maasto[1].Position+new Vector2(Tank.size.X / 2 + maasto.Maasto[1].Width/2, 0));
                        state = GameState.Turn2;
                    }
                }
                else if (state == GameState.Winner2)
                {
                    Raylib.DrawRectangle(0, 0, WindowWidth, WindowHeight, Color.DarkPurple);
                    Raylib.DrawText("Player 2 won the game\n/nPress 'enter' to continue", WindowWidth/4, WindowHeight/2, 50, Color.White);

                    if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                    {
                        TerrainEnd = maasto.GenerateTerrain();
                        player1 = new Tank(maasto.Maasto[maasto.Maasto.Count - 2].Position + new Vector2(Tank.size.X / 2 + maasto.Maasto[1].Width / 2, 0));
                        player2 = new Tank(maasto.Maasto[1].Position + new Vector2(Tank.size.X / 2 + maasto.Maasto[1].Width / 2, 0));
                        state = GameState.Turn1;
                    }
                }
                Raylib.EndDrawing();

                if (Raylib.IsKeyPressed(KeyboardKey.D))
                {
                    state = GameState.Winner1;
                }

                if (state== GameState.Turn1)
                {
                    player1.Update();
                    Ball.Ballstate result = ball1.Update(player1.Direction, maasto);
                    if (result== Ball.Ballstate.Hit)
                    {
                        state = GameState.Turn2;

                    }
                    else if (result == Ball.Ballstate.HitPlayer)
                    {
                        Console.WriteLine("Player hit");
                        state = GameState.Winner1;
                    }
                    
                }
                else if (state == GameState.Turn2) 
                {
                    player2.Update();
                    Ball.Ballstate result2 = ball2.Update(player2.Direction, maasto);
                    if (result2 == Ball.Ballstate.Hit)
                    {
                        state = GameState.Turn1;

                    }
                    else if (result2 == Ball.Ballstate.HitPlayer)
                    {
                        Console.WriteLine("Hit player");
                        state = GameState.Winner2;
                    }
                    
                }
                
                

                
            }
            Raylib.CloseWindow();
        }
        
        
    }
}
