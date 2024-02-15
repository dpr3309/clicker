using Clicker.Model;
using UniRx;
using Zenject;

namespace Clicker.ViewModel
{
    internal class ScoreViewModel : IScoreViewModel
    {
        private ICrystalModel _crystalModel;
        public IReadOnlyReactiveProperty<ulong> Score => _crystalModel.Score;

        [Inject]
        private ScoreViewModel(ICrystalModel crystalModel)
        {
            _crystalModel = crystalModel;
        }
    }
}