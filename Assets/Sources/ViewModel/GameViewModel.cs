using Clicker.Model;
using Zenject;

namespace Clicker.ViewModel
{
    public interface IGameViewModel
    {
        public void Startup();
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

        public void Startup()
        {
            _gameModel.Startup();
        }
        
        public void Update()
        {
            _gameModel.Update();
        }
    }
}

