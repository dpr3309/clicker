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
#if UNITY_EDITOR
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => _gameViewModel.Click());
#endif
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _gameViewModel.Click();
            }
#elif UNITY_ANDROID && !UNITY_EDITOR
            if (Input.touchCount == 1)
            {
                var tough = Input.GetTouch(0);
                if(tough.phase == TouchPhase.Began)
                    _gameViewModel.Click();
            }
#endif
        }
    }
}