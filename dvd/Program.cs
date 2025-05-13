using Raylib_cs;
using System.Net.Http.Headers;
using System.Numerics;

namespace dvd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Ikkunan luominen.
            int ScreenWidth = 1200;
            int ScreenHeight = 800;
            Raylib.InitWindow(ScreenWidth, ScreenHeight, "dvd");

            //Tekstin paikka. suunta ja nopeus.
            Vector2 Position = new Vector2(ScreenWidth/2, ScreenHeight/2);
            Vector2 Direction = new Vector2(1, 1);
            float Speed = 100.0f;

            Color color = Color.White;
            Font TextFont = Raylib.GetFontDefault();
            Vector2 TextSize = Raylib.MeasureTextEx(TextFont, "DVD", 20, 2);
            

            while (Raylib.WindowShouldClose() == false)
            {
                
                Position += Direction * Speed * Raylib.GetFrameTime();
                

                if (Position.X + TextSize.X > ScreenWidth)
                {
                    Direction = new Vector2(Direction.X * -1, Direction.Y);
                    color = Color.Green;
                    Speed += 10.0f;
                }
                if (Position.X <= 0)
                {
                    Direction = new Vector2(Direction.X * -1, Direction.Y);
                    color = Color.Blue;
                    Speed += 10.0f;
                }
                if (Position.Y + TextSize.Y > ScreenHeight)
                {
                    Direction = new Vector2(Direction.X, Direction.Y * -1);
                    color = Color.Red;
                    Speed += 10.0f;
                }
                if (Position.Y <= 0)
                {
                    Direction = new Vector2(Direction.X, Direction.Y * -1);
                    color = Color.Pink;
                    Speed += 10.0f;
                }




                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                Raylib.DrawText("DVD", (int)Position.X,(int)Position.Y, 20, color);
                Raylib.EndDrawing();

                
            }
        }
    }
}
