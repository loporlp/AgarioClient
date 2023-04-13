using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class World
    {
        public readonly int width = 5000;
        public readonly int height = 5000;
        //using ID to map the players and food
        public Dictionary<long, Player> players = new();
        public Dictionary<long, Food> food = new();

        public void addPlayer(long id, Player player)
        {
            if(players.ContainsKey(id))
            {
                players.Remove(id);
            }

            players.Add(id, player);
        }


        public void addFood(long id, Food food)
        {
            if (!players.ContainsKey(id))
            {
                this.food.Add(id, food);
            }
        }


        public void removePlayer(long id)
        {
            if(players.ContainsKey(id))
            {
                players.Remove(id);
            }
        }

        public void removeFood(long id)
        {
            if(food.ContainsKey(id)) { 
                 food.Remove(id);
            }
        }

    }
}
