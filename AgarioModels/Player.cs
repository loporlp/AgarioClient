using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class Player: GameObject
    {

        private string name;
        public Player(string Name, long ID, float X, float Y, int ARGBColor, float Mass): base(ID, new Vector2(X, Y), ARGBColor, Mass)
        {
            this.name = Name;
        }


        public string Name
        { get { return name; } }

    }
}
