using System;
using System.Collections.Generic;
using Clicker.FSMComponents;
using EventArgs = Clicker.FSMComponents.EventArgs;

namespace Clicker.Model.FSMComponents.States
{
    internal class InGameState : AState
    {
        private readonly IPlayerChipModel _playerChipModel;
        private readonly IGameModel _gameModel;

        protected override Dictionary<EventArgs, Type> HandledEvents { get; } = new Dictionary<EventArgs, Type>();

        public InGameState(IPlayerChipModel playerChipModel, IGameModel gameModel)
        {
            _playerChipModel = playerChipModel;
            _gameModel = gameModel;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _gameModel.StartGame();
        }

        public override (Type stateType, object[] values) Event(EventArgs args, params object[] values)
        {
            if (args.Id == Click.Id)
            {
                _playerChipModel.ChangeDirection();
            }

            if (args.Id == Update.Id)
            {
                _gameModel.Update();
                if (_gameModel.LostGame)
                {
                    return (typeof(LostGameState), values);
                }
            }

            return base.Event(args);
        }
    }
}