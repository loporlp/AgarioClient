using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowardAgarioStepOne
{
    public class WorldDrawable : IDrawable
    {
        public WorldModel model = new WorldModel();

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.DarkBlue;
            canvas.FillRectangle(0, 0, 500, 500);

            canvas.FillColor = Colors.Red;
            canvas.FillCircle(model.x, model.y, 10);

          //  canvas.FillCircle(500, 500, 50);
            
        }
    }
}
