using Clicker.Model.FSMComponents;
using Zenject;

namespace Clicker.ViewModel
{
    public class GameViewModel : IGameViewModel
    {
        private IGameStateMachine _fsm;

        [Inject]
        private GameViewModel(IGameStateMachine fsm)
        {
            _fsm = fsm;
        }

        public void Startup()
        {
            _fsm.Startup();
        }

        public void Update()
        {
            _fsm.Update();
        }

        public void Click()
        {
            _fsm.Click();
        }
    }
}