using System.Numerics;

namespace AgarioModels
{
    /// <summary>
    /// Authors: Mason Sansom and Druv Rachakonda
    /// Date: 10-April-2023
    /// Course:    CS 3500, University of Utah, School of Computing
    /// Copyright: CS 3500, Mason Sansom and Druv Rachakonda - This work may not 
    ///            be copied for use in Academic Coursework.
    ///
    /// We, Mason Sansom and Druve Rachakonda, certify that we wrote this code from scratch and
    /// All references used in the completion of the assignments are cited 
    /// in the README file.
    ///
    /// File Contents
    /// Contains the information pertaining to all gameobjects in the game
    /// </summary>
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

        public float Mass { get { return mass; } set { mass = value; } }

        public float X { get { return position.X; } }

        public float Y { get { return position.Y; } }

        public int ARGBColor { get { return color; } set { color = value; } }

        public float Radius { get { return (float)Math.Sqrt(mass / (Math.PI)); } }



    }
}
