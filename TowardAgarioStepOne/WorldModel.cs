using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TowardAgarioStepOne 
{
    public class WorldModel
    {
        public float x = 100;
        public float y = 100;
        public Vector2 direction = new Vector2(50, 25);

        public void AdvanceGameOneStep()
        {
            if(x + direction.X > 500 || x + direction.X < 0)
                direction.X *= -1;

            if (y + direction.Y > 500 || y + direction.Y < 0)
                direction.Y *= -1;

            x += direction.X;
            y += direction.Y;
        }
    }
}
