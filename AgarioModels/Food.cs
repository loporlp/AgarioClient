using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class Food : GameObject
    {
        [JsonConstructor]
        public Food( float X, float Y, int ARGBColor, long ID, float Mass) : base(ID, new Vector2(X, Y), ARGBColor, Mass)
        {
            
        }

       
        
    }
}
