namespace Clicker.Model
{
    public interface IGameModel
    {
        bool LostGame { get; }
        void Update();
        float FallingOfPlayer();
        void StartGame();
        void WaitLost();
        void Restart();
        void EndOfGame();
        void Startup();
    }
}