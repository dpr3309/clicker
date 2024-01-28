using Clicker.Model;
using Zenject;

namespace Clicker.ViewModel
{
    public interface IGameViewModel
    {
        public void Update();
    }

    public class GameViewModel : IGameViewModel
    {
        private IGameModel _gameModel;

        [Inject]
        private GameViewModel(IGameModel gameModel)
        {
            this._gameModel = gameModel;
        }
        
        public void Update()
        {
            _gameModel.Update();
        }
    }
}

