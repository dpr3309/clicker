using System;
using System.Collections.Generic;
using Clicker.Tools.FSMComponents;
using EventArgs = Clicker.Tools.FSMComponents.EventArgs;

namespace Clicker.Model.FSMComponents.States
{
    internal class LostGameState : AState
    {
        private readonly IGameModel _gameModel;

        protected override Dictionary<EventArgs, Type> HandledEvents { get; } = new Dictionary<EventArgs, Type>();

        public LostGameState(IGameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _gameModel.WaitLost();
        }

        public override (Type stateType, object[] values) Event(EventArgs args, params object[] values)
        {
            if (args.Id == Update.Id)
            {
                var position = _gameModel.FallingOfPlayer();
                if (position < -2f)
                {
                    return (typeof(EndOfGameState), values);
                }
            }

            return base.Event(args);
        }
    }
}