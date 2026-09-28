using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace ARTILLERY
{
    internal class Terrain
    {



        public List<Rectangle> Maasto = new List<Rectangle> ();

        public int PositionX = 0;
        public int Width = 80;

        public Terrain()
        {
            
        }

        public Vector2 GenerateTerrain()
        {
            Vector2 pos = new Vector2();
            PositionX = 0;
            Maasto.Clear();
            //int TopLeft;
            while (PositionX < Raylib.GetScreenWidth())
            {
                Random random = new Random();
                int Height = random.Next(100, 200);

                int PositionY = Raylib.GetScreenHeight() - Height;
                Rectangle maasto = new Rectangle(PositionX, PositionY, Width, Height);
                PositionX += Width;
                Maasto.Add(maasto);
                pos = new Vector2(PositionX, PositionY);
                //TopLeft = (int)pos.X - Height;
            }
            pos.X -= Width;
            return pos;
        }
        public void DrawTerrain()
        {
           

            //Vector2 TopLeft = pos - Height;

            foreach (Rectangle rec in Maasto)
            {
                Raylib.DrawRectangle((int)rec.X, (int)rec.Y, (int)rec.Width, (int)rec.Height, Color.Black);
            }
            
        }
    }
}
