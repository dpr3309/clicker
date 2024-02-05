using Clicker.Factories;
using Clicker.ViewModel;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.View
{
    public class FieldView : MonoBehaviour
    {
        //todo: inject this
        [SerializeField]
        private FloorFactory _ff;

        private IFieldViewModel _fieldViewModel;

        [Inject]
        private void Initialize(IFieldViewModel fieldViewModel)
        {
            _fieldViewModel = fieldViewModel;
        }

        private void Start()
        {
            _fieldViewModel.TileInstances.ObserveAdd().Subscribe(pos => _ff.GenerateItemInPosition(pos.Value));
            _fieldViewModel.TileInstances.ObserveRemove().Subscribe(pos =>
            {
                _ff.OnItemRemoved(_fieldViewModel.TileInstances);
            });
        }

        private void OnDestroy()
        {
            //??? unsubscribe ???
        }
    }
}
