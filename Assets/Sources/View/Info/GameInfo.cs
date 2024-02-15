using Clicker.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.View
{
    public class GameInfo : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI infoLabel;

        private IGameInfoViewModel _gameInfoViewModel;

        [Inject]
        private void Initialize(IGameInfoViewModel crystalViewModel)
        {
            _gameInfoViewModel = crystalViewModel;
        }

        private void Start()
        {
            _gameInfoViewModel.Label.Subscribe(value => infoLabel.text = value);
        }
    }
}