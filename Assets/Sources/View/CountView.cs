using Clicker.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.View
{
    public class CountView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI countLabel;

        private IScoreViewModel _crystalViewModel;

        [Inject]
        private void Initialize(IScoreViewModel crystalViewModel)
        {
            _crystalViewModel = crystalViewModel;
        }

        private void Start()
        {
            _crystalViewModel.Score.Subscribe(value => countLabel.text = value.ToString());
        }
    }
}