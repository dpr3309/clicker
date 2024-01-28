using Clicker.ViewModel;
using UnityEngine;
using Zenject;

namespace Clicker.View
{
    public class GameView : MonoBehaviour
    {
        private IGameViewModel _gameViewModel;

        [Inject]
        private void Initialize(IGameViewModel gameViewModel)
        {
            this._gameViewModel = gameViewModel;
        }

        void Update()
        {
            _gameViewModel.Update();
        }
    }
}

