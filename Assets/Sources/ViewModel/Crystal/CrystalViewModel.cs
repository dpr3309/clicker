using Clicker.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.ViewModel
{
    public class CrystalViewModel : ICrystalViewModel
    {
        private readonly ICrystalModel _crystalModel;
        public IReadOnlyReactiveCollection<Vector2> CrystalPositions => _crystalModel.CrystalPositions;

        [Inject]
        private CrystalViewModel(ICrystalModel crystalModel)
        {
            _crystalModel = crystalModel;
        }
    }
}