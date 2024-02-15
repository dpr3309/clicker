namespace Clicker.Model.FSMComponents
{
    public interface IGameStateMachine
    {
        void Startup();
        void Update();
        void Click();
    }
}