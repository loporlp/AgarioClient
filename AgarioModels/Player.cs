﻿using System;
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
        public Player(string name, long id, Vector2 position, int color, float mass): base(id, position, color, mass)
        {
            this.name = name;
        }


        public string Name
        { get { return name; } }

    }
}