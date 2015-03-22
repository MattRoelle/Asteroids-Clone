namespace Asteroids
{
    class Rect
    {
        public float X, Y, W, H;

        public Rect(float X, float Y, float W, float H)
        {
            this.X = X;
            this.Y = Y;
            this.W = W;
            this.H = H;
        }

        public bool Intersects(Rect r2)
        {
            return !(
                   this.X > r2.X + r2.W
                || this.X + this.W < r2.X
                || this.Y > r2.Y + r2.H
                || this.Y + this.H < r2.H
           );
        }
    }
}
