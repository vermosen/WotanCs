namespace Wotan
{
    public class managedAccounts : twsMessage
    {
        public string bla { get; private set; }

        public managedAccounts(string bla) : base(messageType.managedAccounts)
        {
            this.bla = bla;
        }
    }
}
