using AgarioModels;
using System.Numerics;

namespace ClientGUI
{
    public class WorldDrawable : IDrawable
    {
        private World world;
        private readonly float screenHeight;
        private readonly float screenWidth;

        public WorldDrawable(World world, float width, float height)
        {
            this.world = world;
            screenHeight = width;
            screenWidth = height;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.DarkBlue;
            canvas.FontColor = Colors.HotPink;
            canvas.FillRectangle(0, 0, screenHeight, screenWidth);

            //Draw Food
            lock(world)
            {
                foreach (Food food in world.food.Values)
                {
                    canvas.FillColor = Color.FromInt(food.ARGBColor);
                    Vector2 pos = getScreenPosition(food.X, food.Y);
                    canvas.FillCircle(pos, food.Radius);
                }
            }

            //Draw Players
            lock (world)
            {
                foreach (Player player in world.players.Values)
                {
                    canvas.FillColor = Color.FromInt(player.ARGBColor);
                    Vector2 pos = getScreenPosition(player.X, player.Y);
                    canvas.FillCircle(pos, player.Radius);             
                    canvas.DrawString(player.Name, pos.X, pos.Y + player.Radius, HorizontalAlignment.Center);
                }
            }
            
        }

        public Vector2 getScreenPosition(float X, float Y)
        {
            float screenX = (X / world.width) * screenWidth;
            float screenY = (Y / world.height) * screenHeight;
            return new Vector2(screenX, screenY);
        }
    }
}
