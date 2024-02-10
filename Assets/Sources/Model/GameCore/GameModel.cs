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

        [Inject]
        private GameModel(IPlayerChipModel playerChipModel, IFieldModel fieldModel,
            ICoordinateProcessor coordinateProcessor, ICrystalModel crystalModel)
        {
            _playerChipModel = playerChipModel;
            _coordinateProcessor = coordinateProcessor;
            _fieldModel = fieldModel;
            _crystalModel = crystalModel;
        }

        public void Startup()
        {
            _fieldModel.Startup();
            _crystalModel.Startup(_playerChipModel.Position2D);
        }

        public void Update()
        {
            Vector3 newPlayerPosition =
                _coordinateProcessor.TransformCoordinates(_playerChipModel.Position.Value, _playerChipModel.Speed);
            _playerChipModel.UpdatePosition(newPlayerPosition);

            ProcessPlayerPosition(_playerChipModel.Position2D);
        }

        private void ProcessPlayerPosition(Vector2 playerChipPosition)
        {
            if (_coordinateProcessor.CoordinatesAreWithinTiles(playerChipPosition, _fieldModel.TileInstances))
            {
                _fieldModel.ProcessPlayerPosition(playerChipPosition);
                _crystalModel.ProcessPlayerPosition(playerChipPosition);
            }
            else
            {
                throw new NotImplementedException("Game over");
            }
        }
    }
}