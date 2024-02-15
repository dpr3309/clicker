using UniRx;

namespace Clicker.ViewModel
{
    public interface IGameInfoViewModel
    {
        IReadOnlyReactiveProperty<string> Label { get; }
    }
}