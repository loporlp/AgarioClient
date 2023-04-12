using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class Food : GameObject
    {
        private string name { get; set; }
        public Food(string name, long id, Vector2 position, int color, float mass) : base(id, position, color, mass)
        {
            this.name = name;
        }

       
        
    }
}
