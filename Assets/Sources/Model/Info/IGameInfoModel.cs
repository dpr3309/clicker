using UniRx;

namespace Clicker.Model
{
    public interface IGameInfoModel
    {
        IReadOnlyReactiveProperty<string> Label { get; }
        void StartMessage();
        void EndOfGameMessage();
        void ClearMessage();
    }
}