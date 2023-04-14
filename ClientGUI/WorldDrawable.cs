using AgarioModels;
using System.Numerics;

namespace ClientGUI
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
    /// Helper class to help draw the world in the Graphics view
    /// </summary>
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

        /// <summary>
        ///     Draws all food and players when invalidated
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="dirtyRect"></param>
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

        /// <summary>
        ///     Helper method to convert world coordinates to screen coordinates
        /// </summary>
        /// <param name="X"> x position </param>
        /// <param name="Y"> y position </param>
        /// <returns></returns>
        public Vector2 getScreenPosition(float X, float Y)
        {
            float screenX = (X / world.width) * screenWidth;
            float screenY = (Y / world.height) * screenHeight;
            return new Vector2(screenX, screenY);
        }
    }
}
