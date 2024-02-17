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

        public Vector3 TransformCoordinatesFall(Vector3 playerChipPosition, float modifier)
        {
            return _coordinateModifierManager.TransformCoordinatesFall(playerChipPosition, modifier);
        }

        public (bool, Vector2) CoordinatesAreWithinTiles(Vector2 playerChipPosition, IEnumerable<Vector2> tilesCoordinates)
        {
            foreach (var tileCenterCoordinate in tilesCoordinates)
            {
                if (_tileCoordinateProcessor.ContainsCoordinates(tileCenterCoordinate, playerChipPosition))
                    return (true, tileCenterCoordinate);
            }

            return (false, default);
        }

        public bool PlayerChipCollisionWithOtherObject(Vector2 playerChipPosition, Vector2 otherObjectCoordinate)
        {
            return _playerChipCoordinateProcessor.ContainsCoordinates(playerChipPosition, otherObjectCoordinate);
        }

        public void ChangeDirection()
        {
            _coordinateModifierManager.ChangeDirection();
        }

        public void Reset()
        {
            _coordinateModifierManager.Reset();
        }
    }
}