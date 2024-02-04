using Clicker.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.ViewModel
{
    public class PlayerChipViewModel : IPlayerChipViewModel
    {
        private IPlayerChipModel _playerChipModel;

        public IReadOnlyReactiveProperty<Vector3> Position => _playerChipModel.Position;

        [Inject]
        private PlayerChipViewModel(IPlayerChipModel playerChipModel)
        {
            _playerChipModel = playerChipModel;
        }

        public void ChangeDirection()
        {
            _playerChipModel.ChangeDirection();
        }
    }
}