using System;
using System.Collections.Generic;
using Clicker.Tools.FSMComponents;
using EventArgs = Clicker.Tools.FSMComponents.EventArgs;

namespace Clicker.Model.FSMComponents.States
{
    internal class EndOfGameState : AState
    {
        private readonly IGameModel _gameModel;

        protected override Dictionary<EventArgs, Type> HandledEvents { get; } = new Dictionary<EventArgs, Type>();

        public EndOfGameState(IGameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _gameModel.EndOfGame();
        }

        public override (Type stateType, object[] values) Event(EventArgs args, params object[] values)
        {
            if (args.Id == Click.Id)
            {
                _gameModel.Restart();
                return (typeof(ReadyToStartState), values);
            }

            return base.Event(args);
        }
    }
}