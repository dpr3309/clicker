using UniRx;
using Zenject;

namespace Clicker.Model
{
    internal class GameInfoModel : IGameInfoModel
    {
        private const string END_GAME_MESSAGE = "Loose! score: ";
        private const string START_GAME_MESSAGE = "Tap to start";

        private ReactiveProperty<string> _label = new ReactiveProperty<string>();
        public IReadOnlyReactiveProperty<string> Label => _label;

        private ICrystalModel _crystalModel;

        [Inject]
        private GameInfoModel(ICrystalModel crystalModel)
        {
            _crystalModel = crystalModel;
        }

        public void ClearMessage()
        {
            _label.Value = string.Empty;
        }

        public void EndOfGameMessage()
        {
            _label.Value = END_GAME_MESSAGE + _crystalModel.Score.Value;
        }

        public void StartMessage()
        {
            _label.Value = START_GAME_MESSAGE;
        }
    }
}