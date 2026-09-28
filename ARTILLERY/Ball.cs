using Raylib_cs;
using System.Numerics;

namespace ARTILLERY
{
    internal class Ball
    {
        public Vector2 Position;
        private Vector2 startPos;
        private Vector2 EnemyPos;
        public Vector2 Velocity;
        public Vector2 Direction;
        public Vector2 Size;
        public float Acceleration;
        public float Power;
        

        public enum Ballstate
        {
            Idle,
            Shot,
            Hit,
            HitPlayer,
        }
        Ballstate state;

        public Ball(Vector2 startPos, Vector2 EnemyPos)
        {
            this.startPos = startPos;
            this.EnemyPos = EnemyPos;
            Velocity = new Vector2(0, 0);
            Direction = new Vector2(0, -1);
            Acceleration = 100.0f;
            Size = new Vector2(25, 25);
            state = Ballstate.Idle;
        }

        public Ballstate Update(Vector2 playerDirect, Terrain maasto)
        {

            float deltaTime = Raylib.GetFrameTime();
            //Pysyy samana ja osottaa alaspäin
            Vector2 gravity = new Vector2(0, 10);
            bool shot = false;

            if (state == Ballstate.Idle)
            {
                if (Raylib.IsKeyDown(KeyboardKey.Space))
                {
                    Power += deltaTime;
                 

                }
                else if (Raylib.IsKeyReleased(KeyboardKey.Space))
                {
                    Direction = playerDirect;
                    Velocity = Direction * (MathF.Min(Power, 5)/5) * 4000;
                    Position = startPos+Direction*100;
                    shot = true;
                    state = Ballstate.Shot;
                    Power = 0;
                }

            }
            else if (state == Ballstate.Shot)
            {
                Vector2 BallAcceleration = gravity * 100;

                Velocity += BallAcceleration * deltaTime;
                Position += Velocity * deltaTime;
                for (int i = 0; i < maasto.Maasto.Count; i++)
                {
                    if (Raylib.CheckCollisionCircleRec(Position, Size.X, maasto.Maasto[i]))
                    {
                        state = Ballstate.Hit;
                    }
                }

                if (Raylib.CheckCollisionCircleRec(Position, Size.X, new Rectangle(EnemyPos-new Vector2(25, 50), new Vector2(25, 50))))
                {
                    state = Ballstate.HitPlayer;
                }
                if (Position.X < 0 || Position.X > Raylib.GetScreenWidth() || Position.Y > Raylib.GetScreenHeight())
                {
                    state = Ballstate.Hit;
                    
                }
                

            }

            bool WasHit = state == Ballstate.Hit;
            if (WasHit)
            {
                state = Ballstate.Idle;
                return Ballstate.Hit;
            }
            return state;

        }

        public void DrawBall()
        {
            if (state != Ballstate.Idle)
            {
                Raylib.DrawCircleV(Position, Size.X, Color.Violet);
            }

            float Fill = (MathF.Min(Power, 5) / 5);
           
            //Player1
            Vector2 PowerBarPos1 = new Vector2(startPos.X-50, 300);
            Vector2 PowerAmount1 = new Vector2(PowerBarPos1.X, PowerBarPos1.Y+125-125*Fill);



            //Player 1
            Raylib.DrawRectangleV(PowerBarPos1, new Vector2(75, 125), Color.Red);
            Raylib.DrawRectangleV(PowerAmount1, new Vector2(75, 125*Fill), Color.Green);


        }



    }
}
