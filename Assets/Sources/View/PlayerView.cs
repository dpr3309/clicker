using Clicker.ViewModel;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.View
{
    public class PlayerView : MonoBehaviour
    {
        private IPlayerChipViewModel _playerChipViewModel;

        [Inject]
        private void Initialize(IPlayerChipViewModel playerChipViewModel)
        {
            _playerChipViewModel = playerChipViewModel;
        }

        private void Start()
        {
            _playerChipViewModel.Position.Subscribe(value => this.transform.position = value);
        }
    }
}