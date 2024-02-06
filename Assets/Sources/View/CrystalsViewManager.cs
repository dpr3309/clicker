using System;
using System.Collections.Generic;
using Clicker.Factories;
using Clicker.ViewModel;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker.View
{
    public class CrystalsViewManager : MonoBehaviour
    {
        private CrystalsFactory _crystalsFactory;
        private ICrystalViewModel _crystalViewModel;

        private Dictionary<string, AbstractCrystal> _crystalInstances = new Dictionary<string, AbstractCrystal>();

        [Inject]
        private void Initialize(ICrystalViewModel crystalViewModel, CrystalsFactory crystalsFactory)
        {
            _crystalViewModel = crystalViewModel;
            _crystalsFactory = crystalsFactory;
        }

        private void Start()
        {
            _crystalViewModel.CrystalPositions.ObserveAdd()
                .Subscribe(pos =>
                {
                    var crystalInstance = _crystalsFactory.GenerateItemInPosition();
                    crystalInstance.Setup(pos.Value);
                    crystalInstance.Show();
                    _crystalInstances.Add(pos.Value.ToString(), crystalInstance);
                });
            _crystalViewModel.CrystalPositions.ObserveRemove().Subscribe(pos =>
            {
                var key = pos.Value.ToString();
                if (!_crystalInstances.ContainsKey(key))
                {
                    throw new Exception(
                        $"[CrystalViewsManager.OnCrystalRemoved] _crystalInstances not contains key: {key}");
                }

                _crystalInstances.Remove(key, out AbstractCrystal toRemoveITem);
                toRemoveITem.Hide();
                _crystalsFactory.OnItemRemoved(toRemoveITem);
            });
        }
    }
}