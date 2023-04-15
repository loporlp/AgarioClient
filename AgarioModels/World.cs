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
    /// Represents the agario world and stores all players and all food
    /// and supports adding and removing them both
    /// </summary>
    public class World
    {
        public readonly int width = 5000;
        public readonly int height = 5000;
        public bool alive = true;
        //using ID to map the players and food
        public Dictionary<long, Player> players = new();
        public Dictionary<long, Food> food = new();

        /// <summary>
        ///     Add player to Dictionary, if player with that ID
        ///     already exists update it
        /// </summary>
        /// <param name="id"> id of player </param>
        /// <param name="player"> player object to add </param>
        public void addPlayer(long id, Player player)
        {
            if(players.ContainsKey(id))
            {
                players.Remove(id);
            }

            players.Add(id, player);
        }

        /// <summary>
        ///     Add food to Dictionary, if food with that ID
        ///     already exists update it
        /// </summary>
        /// <param name="id"> id of food </param>
        /// <param name="player"> food object to add </param>
        public void addFood(long id, Food food)
        {
            if (!players.ContainsKey(id))
            {
                this.food.Add(id, food);
            }
        }

        /// <summary>
        ///     if dictionary contains player remove them
        /// </summary>
        /// <param name="id"> id of player to be removed </param>
        public void removePlayer(long id)
        {
            if(players.ContainsKey(id))
            {
                players.Remove(id);
            }
        }

        /// <summary>
        ///     if dictionary contains food remove them
        /// </summary>
        /// <param name="id"> id of food to be removed </param>
        public void removeFood(long id)
        {
            if(food.ContainsKey(id)) { 
                 food.Remove(id);
            }
        }


        public Player GetPlayer(long id)
        {
            Player player = players[id];
            return player;
        }

    }
}
