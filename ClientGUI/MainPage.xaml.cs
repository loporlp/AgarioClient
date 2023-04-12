using Communications;
using Microsoft.Extensions.Logging.Abstractions;

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

        }

        void onDisconnect(Networking channel)
        {

        }

    }
}