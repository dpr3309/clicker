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
            _gameViewModel = gameViewModel;
        }

        private void Start()
        {
            _gameViewModel.Startup();
            Screen.sleepTimeout = int.MaxValue;
        }

        void Update()
        {
            _gameViewModel.Update();
        }
    }
}