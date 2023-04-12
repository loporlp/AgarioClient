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

        public long Id { get { return id; } }

        public Vector2 Position { get { return position; } }

        public float Mass { get { return mass; } } 

        public float Radius { get { return (float)Math.Sqrt(mass / (Math.PI)); } }



    }
}
