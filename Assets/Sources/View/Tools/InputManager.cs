using Clicker.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Clicker.Tools
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private Button button = null;

        private IGameViewModel _gameViewModel;

        [Inject]
        private void Initialize(IGameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
        }

        private void Start()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => _gameViewModel.Click());
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _gameViewModel.Click();
            }
#endif
        }
    }
}