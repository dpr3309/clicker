using Clicker.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.ViewModel
{
    public class FieldViewModel : IFieldViewModel
    {
        public IReadOnlyReactiveCollection<Vector3> TileInstances => _fieldModel.TileInstances;
        
        private IFieldModel _fieldModel;

        [Inject]
        private FieldViewModel(IFieldModel fieldModel)
        {
            _fieldModel = fieldModel;
        }
    }
}