using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Diagnostics;
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
        private long playerID;
        private Point? pos;
        private readonly float screenWidth;
        private readonly float screenHeight;

        private World world;

        public MainPage()
        {
            channel = new Networking(NullLogger.Instance, onConnect, onDisconnect, onMessage, '\n');
            initialized = false;
            world = new World();
            screenWidth = 500;
            screenHeight = 500;
            InitializeComponent();
        }

        private void ConnectToServer(object sender, EventArgs e)
        {
            if (PlayerName.Text == null)
            {
                ErrorMessage.Text = "PLEASE ENTER NAME";
                return;
            }

            try
            {
                channel.Connect(ServerAddress.Text, 11000);
            }
            catch (Exception ex)
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

            channel.Send(String.Format(Protocols.CMD_Start_Game, PlayerName.Text));
        }

        void onMessage(Networking channel, string message)
        {

            // Updating Food
            if (message.StartsWith(Protocols.CMD_Food))
            {
                Food[] food = JsonSerializer.Deserialize<Food[]>(message[Protocols.CMD_Food.Length..]);

                foreach (Food item in food)
                {
                    lock (world)
                    {
                        world.addFood(item.ID, item);
                    }
                }
            }

            // Removing Eaten Food
            if (message.StartsWith(Protocols.CMD_Eaten_Food))
            {
                long[] food = JsonSerializer.Deserialize<long[]>(message[Protocols.CMD_Eaten_Food.Length..]);

                foreach (long id in food)
                {
                    lock (world)
                    {
                        world.removeFood(id);
                    }
                }
            }

            // Updating Players
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

            // Removing Dead Players
            if (message.StartsWith(Protocols.CMD_Dead_Players))
            {
                int[] players = JsonSerializer.Deserialize<int[]>(message[Protocols.CMD_Dead_Players.Length..]);

                foreach (int playerID in players)
                {
                    lock (world)
                    {
                        world.removePlayer(playerID);
                    }
                }
            }

            if (message.StartsWith(Protocols.CMD_Player_Object))
            {
                playerID = JsonSerializer.Deserialize<long>(message[Protocols.CMD_Player_Object.Length..]);
                draw.setPlayer(playerID);
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
            draw = new WorldDrawable(world, screenWidth, screenHeight);
            PlaySurface.Drawable = draw;
            Window.Width = 1000;
            var timer = Dispatcher.CreateTimer();
            timer.Interval = new TimeSpan(30);
            timer.Tick += GameStep;
            timer.Start();
        }

        private void GameStep(object state, EventArgs e)
        {
            if (pos != null && world.players.ContainsKey(playerID))
            {
                float xToMove = (float)(pos.Value.X / screenWidth) * world.width;
                float yToMove = (float)(pos.Value.Y / screenHeight) * world.height;

                channel.Send(String.Format(Protocols.CMD_Move, (int)xToMove, (int)yToMove));
            }
            PlaySurface.Invalidate();
        }

        private void PointerMoved(object state, PointerEventArgs e)
        {
            pos = e.GetPosition(PlaySurface);
            // Debug.WriteLine($"Pointer at {pos.Value.X},{pos.Value.Y}");
        }

    }
}