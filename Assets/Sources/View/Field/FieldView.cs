using Clicker.Factories;
using Clicker.ViewModel;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.View
{
    public class FieldView : MonoBehaviour
    {
        private FloorFactory _floorFactory;
        private IFieldViewModel _fieldViewModel;

        [Inject]
        private void Initialize(IFieldViewModel fieldViewModel, FloorFactory floorFactory)
        {
            _fieldViewModel = fieldViewModel;
            _floorFactory = floorFactory;
        }

        private void Start()
        {
            _fieldViewModel.TileInstances.ObserveAdd()
                .Subscribe(pos => _floorFactory.GenerateItemInPosition(pos.Value));
            _fieldViewModel.TileInstances.ObserveRemove().Subscribe(pos =>
            {
                _floorFactory.OnItemRemoved(_fieldViewModel.TileInstances);
            });
        }
    }
}