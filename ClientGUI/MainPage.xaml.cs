using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;
using System.Timers;

namespace ClientGUI
{
    public partial class MainPage : ContentPage
    {
        private bool initialized;
        private bool connected;
        private Networking channel;
        private WorldDrawable draw;

        private World world;

        public MainPage()
        {
            channel = new Networking(NullLogger.Instance, onConnect, onDisconnect, onMessage, '\n');
            initialized = false;
            world = new World();

            InitializeComponent();
        }

        private void ConnectToServer(object sender, EventArgs e)
        {
            if(PlayerName.Text == null)
            {
                ErrorMessage.Text = "PLEASE ENTER NAME";
                return;
            }

            try
            {
                channel.Connect(ServerAddress.Text, 11000);   
            } catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }

        void onConnect(Networking channel)
        {
            connected = true;
            channel.AwaitMessagesAsync();
            LoginPage.IsVisible = false;
            Game.IsVisible = true;
            if (!initialized)
            {
                initialized = true;
                InitializeGameLogic();
            }
        }

        void onMessage(Networking channel, string message)
        {

            if(message.StartsWith(Protocols.CMD_Food))
            {
                Food[] food = JsonSerializer.Deserialize<Food[]>(message[Protocols.CMD_Food.Length..]);

                foreach(Food item in food) 
                {
                    lock (world)
                    {
                        world.addFood(item.ID, item);
                    }
                }
            }

            if (message.StartsWith(Protocols.CMD_Update_Players))
            {
                Player[] players = JsonSerializer.Deserialize<Player[]>(message[Protocols.CMD_Update_Players.Length..]);

                foreach (Player player in players)
                {
                    lock (world)
                    {
                        world.addPlayer(player.ID, player);
                    }
                }
            }

        }

        void onDisconnect(Networking channel)
        {

        }


        /// <summary>
        ///    Called when the window is resized.  
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            if (connected)
            {
                base.OnSizeAllocated(width, height);

                if (!initialized)
                {
                    initialized = true;
                    InitializeGameLogic();
                }
            }
        }

        private void InitializeGameLogic()
        {
            draw = new WorldDrawable(world);
            PlaySurface.Drawable = draw;
            Window.Width = 500;
            System.Timers.Timer timer = new System.Timers.Timer(100);
            timer.Elapsed += GameStep;
            timer.Start();
        }

        private void GameStep(object state, ElapsedEventArgs e)
        {
            PlaySurface.Invalidate();
        }

    }
}