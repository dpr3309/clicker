using Clicker.FSMComponents;
using Clicker.Model.FSMComponents.States;
using Zenject;

namespace Clicker.Model.FSMComponents
{
    internal class GameStateMachine : StateMachine<AState>, IGameStateMachine
    {
        [Inject]
        private GameStateMachine(InitGameState initGameState, ReadyToStartState readyToStartState,
            InGameState inGameState, EndOfGameState endOfGameState,
            LostGameState lostGameState)
        {
            Add(initGameState);
            Add(readyToStartState);
            Add(inGameState);
            Add(endOfGameState);
            Add(lostGameState);

            Start<InitGameState>();
        }

        public void Startup()
        {
            Event(InitGameState.StartupEvent);
        }

        public void Update()
        {
            Event(AState.Update);
        }

        public void Click()
        {
            Event(AState.Click);
        }
    }
}