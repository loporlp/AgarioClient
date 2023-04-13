using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class GameObject
    {
        private long id;
        private Vector2 position;
        private int color;
        private float mass;

        public GameObject(long id, Vector2 position, int color, float mass)
        {
            this.id = id;
            this.position = position;
            this.color = color;
            this.mass = mass;
        }

        public long ID { get { return id; } set { id = value; } }

        public Vector2 Position { get { return position; } set { position = value; } }

        public float Mass { get { return mass; } set { mass = value;} } 

        public float X { get { return position.X; } }

        public float Y { get { return position.Y; } }

        public int ARGBColor { get { return color; } set { color = value; } }



        public float Radius { get { return (float)Math.Sqrt(mass / (Math.PI)); } }



    }
}
