using AgarioModels;
using System.Numerics;

namespace ClientGUI
{
    public class WorldDrawable : IDrawable
    {
        private World world;
        private readonly float screenHeight;
        private readonly float screenWidth;
        private long playerID;
        private bool playerSet;
        private Player activePlayer;
        public WorldDrawable(World world, float width, float height)
        {
            this.world = world;
            screenHeight = width;
            screenWidth = height;
            playerID = 0;
            playerSet = false;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {

            canvas.FillColor = Colors.DarkBlue;
            canvas.FontColor = Colors.HotPink;
            canvas.FillRectangle(0, 0, screenHeight, screenWidth);

            if (!playerSet)
            {
                return;
            }

            try
            {
                this.activePlayer = world.players[playerID];
            }

            catch(KeyNotFoundException)
            {
                world.alive = false;
            }




            canvas.FillColor = Color.FromInt(activePlayer.ARGBColor);
            canvas.FillCircle(250-activePlayer.Radius, 250-activePlayer.Radius, activePlayer.Mass/10);
            canvas.FontColor = Colors.White;
            canvas.DrawString(activePlayer.Name, 250-activePlayer.Radius, 250, HorizontalAlignment.Center);


            Vector2 playerPosition = activePlayer.Position;
            Vector2 topLeftCorner = playerPosition - new Vector2(250, 250);




            //Draw Food
            lock (world)
            {
                foreach (Food food in world.food.Values)
                {
                   if((food.X < playerPosition.X + 250 && food.X > playerPosition.X - 250) && (food.Y < playerPosition.Y + 250 && food.Y > playerPosition.Y - 250))
                   {
                        canvas.FillColor = Color.FromInt(food.ARGBColor);
                        canvas.FillCircle(food.Position.X - topLeftCorner.X, food.Position.Y - topLeftCorner.Y, food.Mass / 10);
                   }
                    
                }
            }

            //Draw Players
            lock (world)
            {
                foreach (Player player in world.players.Values)
                {
                    if ((player.X < playerPosition.X + 250 && player.X > playerPosition.X - 250) && (player.Y < playerPosition.Y + 250 && player.Y > playerPosition.Y - 250) && player.ID != activePlayer.ID)
                    {
                        canvas.FillColor = Color.FromInt(player.ARGBColor);
                        canvas.FillCircle(player.Position.X - topLeftCorner.X, player.Position.Y - topLeftCorner.Y, player.Mass/10);
                        canvas.FontColor = Colors.White;
                        canvas.DrawString(player.Name, player.Position.X - topLeftCorner.X, player.Position.Y - topLeftCorner.Y + player.Radius, HorizontalAlignment.Center);
                    }
                }
            }
        }

        public Vector2 getScreenPosition(float X, float Y)
        {
            float screenX = (X / world.width) * screenWidth;
            float screenY = (Y / world.height) * screenHeight;
            return new Vector2(screenX, screenY);
        }

        public void setPlayer(long id)
        {
            playerID = id; 
            playerSet = true;

        }
    }
}