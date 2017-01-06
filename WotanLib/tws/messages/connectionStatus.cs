namespace Wotan
{
    public class connectionStatus : twsMessage
    {
        public connectionStatus(bool isConnected = false) : base(messageType.connectionStatus)
        {
            this.isConnected = isConnected;
        }

        public bool isConnected { get; set; }
    }
}
