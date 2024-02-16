namespace Clicker.Tools.FSMComponents
{
    public class EventArgs
    {
        public string Id { get; }

        public EventArgs(string id)
        {
            Id = id;
        }
    }
}