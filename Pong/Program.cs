using Raylib_cs;
using System.Numerics;

namespace Pong
{
    internal class Program
    {
        /// <summary>
        /// Käytin AI:ta apuna tämän tehtävän tekemisessä.
        /// </summary>
        
       
        static void Main(string[] args)
        {
            const int WindowWidth = 1000;
            const int WindowHeight = 900;

            Raylib.InitWindow(WindowWidth, WindowHeight, "Pong");



            //Mailojen tiedot
            Vector2 paddle = new Vector2(50, 250);
            Vector2 paddleSpeed = new Vector2(0, 10);


            Vector2 player1 = new Vector2(20, WindowHeight / 2 - paddle.Y / 2); //Pelaaja 1 sijainti
            Vector2 player2 = new Vector2(WindowWidth - 75, WindowHeight / 2 - paddle.Y / 2); //Pelaaja 2 sijainti

            //Pisteiden tiedot
            int Points1 = 0;
            int Points2 = 0;


            //Pallon tiedot
            float ballRadius = 20; //Pallon säde.
            Vector2 ballPos = new Vector2(WindowWidth / 2, WindowHeight / 2);  //Pallo luodaan keskelle kenttää
            Vector2 ballSpeed = new Vector2(3, 4); //Pallon nopeus x ja y akselilla.


            Raylib.SetTargetFPS(60);


            while (Raylib.WindowShouldClose() == false)
            {
                
                //Pallon liike
                ballPos.X += (int)ballSpeed.X; // X-akselilla.
                ballPos.Y += (int)ballSpeed.Y; //Y-akselilla.

                //Pelaajan liikutus
                if (Raylib.IsKeyDown(KeyboardKey.W)) { player1.Y -= paddleSpeed.Y; } //Pelaaja 1 ylöspäin.
                if (Raylib.IsKeyDown(KeyboardKey.S)) { player1.Y += paddleSpeed.Y; } //Pelaaja 1 alaspäin.

                if (Raylib.IsKeyDown(KeyboardKey.Up)) { player2.Y -= paddleSpeed.Y; } //Pelaaja 2 ylöspäin.
                if (Raylib.IsKeyDown(KeyboardKey.Down)) { player2.Y += paddleSpeed.Y; } //Pelaaja 2 alaspäin.

                //Pidetään mailat kentällä.
                player1.Y = Math.Clamp(player1.Y, 0 ,WindowHeight - paddle.Y);
                player2.Y = Math.Clamp(player2.Y, 0 ,WindowHeight - paddle.Y);

                
                //Pidetään pallo kentällä.
                if (ballPos.Y <= 0 || ballPos.Y >= WindowHeight) {ballSpeed.Y *= -1; } //jos pallo on ala tai ylä reunalla niin käännetään se.
                if (ballPos.X <= 0 || ballPos.X >= WindowWidth) //jos pallo on oikealla tai vasemmalla reunalla niin käännetään se.
                { 
                    switch (ballPos.X)
                    {
                        case <=0:
                            {
                                Points2 += 1;
                                ballPos = new Vector2(WindowWidth / 2, WindowHeight / 2);
                                break;
                            }
                        case >=WindowWidth:
                            {
                                Points1 += 1;
                                ballPos = new Vector2(WindowWidth / 2, WindowHeight / 2);
                                break;
                            }
                    }
                    ballSpeed.X *= -1;
                }  

                //Luodaan esineille muodot jotta niiden törmäys on helpompi huomata.
                Rectangle player1rec = new Rectangle(player1, paddle); //Luodaan muoto mailalle 1.
                Rectangle player2rec = new Rectangle(player2, paddle); //Luodaan muoto mailalle 2.
                Rectangle ballrec = new Rectangle(ballPos.X - ballRadius, ballPos.Y - ballRadius, ballRadius * 2, ballRadius * 2); //Luodaan muoto pallolle törmäystä varten.

                if (Raylib.CheckCollisionRecs(player1rec, ballrec)) { ballSpeed.X *= -1; } //Jos pallo osuu mailaan 1 käännetään se.
                if (Raylib.CheckCollisionRecs(player2rec, ballrec)) { ballSpeed.X *= -1; } //Jos pallo osuu mailaan 2 käännetään se.

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                // Jaetaan kenttä kahteen osaan viivalla.
                Raylib.DrawLine(WindowWidth / 2, 0, WindowWidth / 2, WindowHeight, Color.White);

                //piirretään pelaajat ja pallo
                Raylib.DrawRectangle((int)player1.X, (int)player1.Y, (int)paddle.X, (int)paddle.Y, Color.Violet); //pelaaja 1
                Raylib.DrawRectangle((int)player2.X, (int)player2.Y, (int)paddle.X, (int)paddle.Y, Color.Blue); //pelaaja 2
                Raylib.DrawCircleV(ballPos, ballRadius, Color.Lime);
                
                //piirretään pisteet
                Raylib.DrawText(Points1.ToString(), WindowWidth/2 - 35, 0, 20 ,Color.White);
                Raylib.DrawText(Points2.ToString(), WindowWidth/2 + 25, 0, 20 , Color.White);

                Raylib.EndDrawing();
            }

        }
    }
}
