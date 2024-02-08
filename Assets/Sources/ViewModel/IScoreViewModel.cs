using UniRx;

namespace Clicker.ViewModel
{
    public interface IScoreViewModel
    {
        IReadOnlyReactiveProperty<ulong> Score { get; }
    }
}