using System;
using UnityEngine;
using Zenject;

namespace Clicker.Model
{
    internal class GameModel : IGameModel
    {
        private readonly IPlayerChipModel _playerChipModel;
        private readonly ICoordinateProcessor _coordinateProcessor;
        private readonly IFieldModel _fieldModel;
        private readonly ICrystalModel _crystalModel;
        private readonly IGameInfoModel _gameInfoModel;

        private Action _updateAction = () => { };
        public bool LostGame { get; private set; }

        [Inject]
        private GameModel(IPlayerChipModel playerChipModel, IFieldModel fieldModel,
            ICoordinateProcessor coordinateProcessor, ICrystalModel crystalModel, IGameInfoModel gameInfoModel)
        {
            _playerChipModel = playerChipModel;
            _coordinateProcessor = coordinateProcessor;
            _fieldModel = fieldModel;
            _crystalModel = crystalModel;
            _gameInfoModel = gameInfoModel;
        }


        public float FallingOfPlayer()
        {
            _updateAction.Invoke();
            return _playerChipModel.Position.Value.y;
        }

        public void StartGame()
        {
            _gameInfoModel.ClearMessage();
            _playerChipModel.StartMove();

            _updateAction = () =>
            {
                _playerChipModel.Update();
                ProcessPlayerPosition(_playerChipModel.Position2D);
            };
        }

        public void Update()
        {
            _updateAction.Invoke();
        }

        public void WaitLost()
        {
            LostGame = false;
            _playerChipModel.StartFall();
            _updateAction = () =>
            {
                _playerChipModel.Update();
            };
        }

        public void Restart()
        {
            _crystalModel.ReleaseAll();
            _crystalModel.ResetScore();
            _fieldModel.ReleaseAll();
            _gameInfoModel.ClearMessage();
            _playerChipModel.Restart();
        }

        public void EndOfGame()
        {
            _updateAction = () => { };
            _playerChipModel.StopMove();
            _gameInfoModel.EndOfGameMessage();
        }

        public void Startup()
        {
            _fieldModel.Startup();
            _crystalModel.Startup(_playerChipModel.Position2D);
            _gameInfoModel.StartMessage();
        }

        private void ProcessPlayerPosition(Vector2 playerChipPosition)
        {
            (bool onTheTile, Vector2 tileWithPlayer) =
                _coordinateProcessor.CoordinatesAreWithinTiles(playerChipPosition, _fieldModel.TileInstances);
            if (onTheTile)
            {
                _fieldModel.ProcessPlayerPosition(tileWithPlayer);
                _crystalModel.ProcessPlayerPosition(tileWithPlayer);
            }
            else
            {
                LostGame = true;
            }
        }
    }
}