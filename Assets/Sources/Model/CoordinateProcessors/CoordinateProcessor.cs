using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Clicker.Model
{
    internal class CoordinateProcessor : ICoordinateProcessor
    {
        private readonly IFigureCoordinateProcessor _tileCoordinateProcessor;
        private readonly IFigureCoordinateProcessor _playerChipCoordinateProcessor;
        private readonly ICoordinateModifierManager _coordinateModifierManager;

        [Inject]
        private CoordinateProcessor(ITileCoordinateProcessor tileCoordinateProcessor,
            IPlayerChipCoordinateProcessor playerChipCoordinateProcessor,
            ICoordinateModifierManager coordinateModifierManager)
        {
            _tileCoordinateProcessor = tileCoordinateProcessor;
            _playerChipCoordinateProcessor = playerChipCoordinateProcessor;
            _coordinateModifierManager = coordinateModifierManager;
        }

        public Vector3 TransformCoordinates(Vector3 playerChipPosition, float modifier)
        {
            return _coordinateModifierManager.TransformCoordinates(playerChipPosition, modifier);
        }

        public bool CoordinatesAreWithinTiles(Vector3 playerChipPosition, IEnumerable<Vector3> tilesCoordinates)
        {
            foreach (var tileCenterCoordinate in tilesCoordinates)
            {
                if (_tileCoordinateProcessor.ContainsCoordinates(tileCenterCoordinate, playerChipPosition))
                    return true;
            }

            return false;
        }

        public bool PlayerChipCollisionWithOtherObject(Vector3 playerChipPosition, Vector3 otherObjectCoordinate)
        {
            return _playerChipCoordinateProcessor.ContainsCoordinates(playerChipPosition, otherObjectCoordinate);
        }
    }
}