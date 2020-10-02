using System;

namespace GravityBalls
{
	public class WorldModel
	{
        public double BallX;
        public double BallY;
        public double BallVx = 200;
        public double BallVy = 200;
        public double Resistance = 0.2; // сисла сопротивления п.п. 3
        public double G = 500; // сила притяжения п.п. 5
        public double Force = 50000; // сила отталкивания п.п. 6
        public double BallRadius;
        public double WorldWidth;
        public double WorldHeight;
        public double CursorX;
        public double CursorY;

        public void SimulateTimeframe(double dt)
        {
            ApplyGravity(dt);
            ApplyCursorRepulsion(dt);
            ApplyAirResistance(dt);
            MoveBall(dt);
            ApplyWallsBouncing();
        }

        private void ApplyGravity(double dt)
        {
            BallVy += G * dt;
        }

        private void ApplyCursorRepulsion(double dt)
        {
            var dx = BallX - CursorX;
            var dy = BallY - CursorY;
            var d = Math.Sqrt(dx * dx + dy * dy);
            var f = Force / (d * d);

            BallVx += dx * f * dt;
            BallVy += dy * f * dt;
        }

        private void ApplyAirResistance(double dt)
        {
            BallVx = BallVx - BallVx * Resistance * dt;
            BallVy = BallVy - BallVy * Resistance * dt;
        }

        private void MoveBall(double dt)
        {
            BallX += BallVx * dt;
            BallY += BallVy * dt;

            BallX = Math.Max(BallRadius, Math.Min(BallX, WorldWidth - BallRadius));
            BallY = Math.Max(BallRadius, Math.Min(BallY, WorldHeight - BallRadius));
        }

        private void ApplyWallsBouncing()
        {
            if (BallY + BallRadius >= WorldHeight || BallY - BallRadius <= 0)
                BallVy = -BallVy;
            if (BallX + BallRadius >= WorldWidth || BallX - BallRadius <= 0)
                BallVx = -BallVx;
        }
    }
}