using System.Diagnostics;
using System.Timers;

namespace TowardAgarioStepOne
{
    public partial class MainPage : ContentPage
    {

        private bool initialized;
        private WorldDrawable draw;
        public MainPage()
        {
            initialized = false;
            InitializeComponent();
        }

        /// <summary>
        ///    Called when the window is resized.  
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Debug.WriteLine($"OnSizeAllocated {width} {height}");

            if (!initialized)
            {
                initialized = true;
                InitializeGameLogic();
            }
        }

        private void InitializeGameLogic()
        {
            draw = new WorldDrawable();
            PlaySurface.Drawable = draw;
            Window.Width = 500;
            System.Timers.Timer timer = new System.Timers.Timer(100);
            timer.Elapsed += GameStep;
            timer.Start();
        }

        private void GameStep(object state, ElapsedEventArgs e)
        {
            draw.model.AdvanceGameOneStep();
            PlaySurface.Invalidate();
        }
    }
}