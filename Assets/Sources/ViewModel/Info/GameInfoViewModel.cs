using Clicker.Model;
using UniRx;
using Zenject;

namespace Clicker.ViewModel
{
    internal class GameInfoViewModel : IGameInfoViewModel
    {
        private IGameInfoModel _gameInfoModel;
        public IReadOnlyReactiveProperty<string> Label => _gameInfoModel.Label;

        [Inject]
        private GameInfoViewModel(IGameInfoModel gameInfoModel)
        {
            _gameInfoModel = gameInfoModel;
        }
    }
}