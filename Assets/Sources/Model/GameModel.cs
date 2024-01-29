using System;
using UnityEngine;
using Zenject;

namespace Clicker.Model
{
    public class GameModel : IGameModel
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
        }

        public void Update()
        {
            Vector3 newPlayerPosition =
                _coordinateProcessor.TransformCoordinates(_playerChipModel.Position, _playerChipModel.Speed);
            _playerChipModel.UpdatePosition(newPlayerPosition);
            ProcessPlayerPosition(_playerChipModel.Position);
        }

        private void ProcessPlayerPosition(Vector3 playerChipPosition)
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