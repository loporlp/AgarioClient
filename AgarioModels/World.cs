using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class World
    {
        private readonly int width = 5000;
        private readonly int height = 5000;
        //using ID to map the players and food
        private Dictionary<long, Player> players = new();
        private Dictionary<long, Food> food = new();
       

        public World(int width, int height)
        {
            this.width = width;
            this.height = height;   
        }

        public void addPlayer(long id, Player player)
        {
            players.Add(id, player);
        }


        public void addFood(long id, Food food)
        {
            this.food.Add(id, food);    
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
