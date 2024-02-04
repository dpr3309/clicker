using Clicker.View;
using UnityEngine;
using Zenject;

namespace Clicker.Tools
{
    public class ObjectTracker : MonoBehaviour
    {
        [SerializeField]
        private float smooth = 0.5f;

        private Vector3 offset;

        private Transform _playerViewTransform = null;

        [Inject]
        private void Construct(PlayerView playerView)
        {
            _playerViewTransform = playerView.transform;
        }

        private void Start()
        {
            offset = transform.position - _playerViewTransform.position;
        }

        private void LateUpdate()
        {
            var newPosition = Vector3.Lerp(transform.position,
                new Vector3(_playerViewTransform.position.x, 0, _playerViewTransform.position.z) + offset, smooth);
            transform.position = newPosition;
        }
    }
}