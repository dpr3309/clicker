using System;
using System.Collections.Generic;
using Clicker.Tools.FSMComponents;
using EventArgs = Clicker.Tools.FSMComponents.EventArgs;

namespace Clicker.Model.FSMComponents.States
{
    internal class ReadyToStartState : AState
    {
        private readonly IGameModel _gameModel;

        public ReadyToStartState(IGameModel gameModel)
        {
            _gameModel = gameModel;
        }

        protected override Dictionary<EventArgs, Type> HandledEvents { get; } = new Dictionary<EventArgs, Type>()
        {
            [Click] = typeof(InGameState)
        };

        public override void OnEnter()
        {
            base.OnEnter();
            _gameModel.Startup();
        }
    }
}