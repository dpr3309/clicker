using Clicker.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.ViewModel
{
    public class FieldViewModel : IFieldViewModel
    {
        private IFieldModel _fieldModel;

        public IReadOnlyReactiveCollection<Vector2> TileInstances => _fieldModel.TileInstances;

        [Inject]
        private FieldViewModel(IFieldModel fieldModel)
        {
            _fieldModel = fieldModel;
        }
    }
}