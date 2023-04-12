using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;

namespace ClientGUI
{
    public partial class MainPage : ContentPage
    {
        private Networking channel;

        public MainPage()
        {
            channel = new Networking(NullLogger.Instance, onConnect, onDisconnect, onMessage, '\n');

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
                channel.AwaitMessagesAsync();
                LoginPage.IsVisible = false;    
            } catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }

        void onConnect(Networking channel)
        {

        }

        void onMessage(Networking channel, string message)
        {

            if(message.StartsWith(Protocols.CMD_Food))
            {
                Food[] food = JsonSerializer.Deserialize<Food[]>(message[Protocols.CMD_Food.Length..]!);

                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(food[i]);
                }
            }
            
            
        }

        void onDisconnect(Networking channel)
        {

        }

    }
}