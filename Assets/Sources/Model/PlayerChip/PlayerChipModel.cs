using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.Model
{
    internal class PlayerChipModel : IPlayerChipModel
    {
        private readonly ICoordinateProcessor _coordinateProcessor;
        public float Speed => 0.005f;

        private IReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>();
        public IReactiveProperty<Vector3> Position => _position;

        private Action _updateAction = () => { };

        public Vector2 Position2D => new(_position.Value.x, _position.Value.z);

        [Inject]
        private PlayerChipModel(ICoordinateProcessor coordinateProcessor)
        {
            _coordinateProcessor = coordinateProcessor;
        }

        public void UpdatePosition(Vector3 newPosition)
        {
            _position.Value = newPosition;
        }

        public void ChangeDirection()
        {
            _coordinateProcessor.ChangeDirection();
        }

        public void StartFall()
        {
            _updateAction = FallingAction;
        }

        public void StopMove()
        {
            _updateAction = () => { };
        }

        public void StartMove()
        {
            _updateAction = InGameAction;
        }

        public void Restart()
        {
            _position.Value = Vector3.zero;
        }

        public void Update()
        {
            _updateAction.Invoke();
        }

        private void InGameAction()
        {
            Vector3 newPlayerPosition =
                _coordinateProcessor.TransformCoordinates(Position.Value, Speed);
            UpdatePosition(newPlayerPosition);
        }

        private void FallingAction()
        {
            Vector3 newPlayerPosition =
                _coordinateProcessor.TransformCoordinatesFall(Position.Value, Speed);
            UpdatePosition(newPlayerPosition);
        }
    }
}