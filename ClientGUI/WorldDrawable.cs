using AgarioModels;
using System.Numerics;

namespace ClientGUI
{
    public class WorldDrawable : IDrawable
    {
        private World world;
        private readonly float screenHeight;
        private readonly float screenWidth;

        public WorldDrawable(World world)
        {
            this.world = world;
            screenHeight = 500;
            screenWidth = 500;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.DarkBlue;
            canvas.FillRectangle(0, 0, screenHeight, screenWidth);

            int foodDrawn = 0;
            foreach (Food food in world.food.Values)
            {
                canvas.FillColor = Color.FromInt(food.ARGBColor);
                Vector2 pos = getScreenPosition(food.X, food.Y);
                canvas.FillCircle(pos.X, pos.Y, food.Radius);
                foodDrawn++;
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
