using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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
    /// This File contains the contents of the Client GUI supports a login page
    /// that allows the User to enter a name and IP and connects to an agario server
    /// once connected displays the game and supports moving with the mouse
    /// </summary>
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
        private readonly ILogger<MainPage> _logger;
        private World world;

        public MainPage(ILogger<MainPage> logger)
        {
            _logger = logger;
            channel = new Networking(_logger, onConnect, onDisconnect, onMessage, '\n');
            initialized = false;
            world = new World();
            screenWidth = 500;
            screenHeight = 500;
            InitializeComponent();
        }

        /// <summary>
        ///     Button that connects user to game
        /// </summary>
        /// <param name="sender"> unused </param>
        /// <param name="e"> unused </param>
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

        /// <summary>
        ///     Called when client connects to game
        ///     brings up the game page and hides the login page
        /// </summary>
        /// <param name="channel"> channel connected to </param>
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

        /// <summary>
        ///     Take messages from server and do 
        ///     what command is ordering according to Protocol.cs
        /// </summary>
        /// <param name="channel"> Networking object sending message </param>
        /// <param name="message"> message sent </param>
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

        /// <summary>
        ///     Called when the client disconnects from the game
        /// </summary>
        /// <param name="channel"></param>
        void onDisconnect(Networking channel)
        {

        }

        private void OnRestart(object sender, EventArgs e)
        {
            //add restart implementation here 
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

        /// <summary>
        ///     Starts the timer and sets the world 
        /// </summary>
        private void InitializeGameLogic()
        {
            _logger.LogInformation("Game Logic Has Begun Running");

            // Create the world
            draw = new WorldDrawable(world, screenWidth, screenHeight);
            PlaySurface.Drawable = draw;
            Window.Width = 1000;
            var timer = Dispatcher.CreateTimer();
            timer.Interval = new TimeSpan(30);
            timer.Tick += GameStep;
            timer.Start();
        }

        /// <summary>
        ///     Method called 30 times per second to handle
        ///     redrawing the game screen and updating movement of player
        /// </summary>
        /// <param name="state"></param>
        /// <param name="e"></param>
        private void GameStep(object state, EventArgs e)
        {

            if (!world.alive)
            {
                restart.IsVisible = true;
            }

            if (pos != null && world.players.ContainsKey(playerID))
            {
                float xToMove = (float)(pos.Value.X / screenWidth) * world.width;
                float yToMove = (float)(pos.Value.Y / screenHeight) * world.height;

                channel.Send(String.Format(Protocols.CMD_Move, (int)xToMove, (int)yToMove));
            }
            PlaySurface.Invalidate();
        }

        /// <summary>
        ///     Updates when the mouse is moved and logs its location
        /// </summary>
        /// <param name="state"> unused </param>
        /// <param name="e"> used to get the pointer posistion </param>
        private void PointerMoved(object state, PointerEventArgs e)
        {
            pos = e.GetPosition(PlaySurface);
           _logger.LogTrace($"Pointer at {pos.Value.X},{pos.Value.Y}");
        }

    }
}
