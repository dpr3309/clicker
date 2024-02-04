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
        
        private IPlayerChipViewModel _playerChipViewModel;

        [Inject]
        private void Initialize(IPlayerChipViewModel playerChipViewModel)
        {
            _playerChipViewModel = playerChipViewModel;
        }

        private void Start()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => _playerChipViewModel.ChangeDirection());
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("SPACE");
                _playerChipViewModel.ChangeDirection();
            }
#endif
        }
    }
}

