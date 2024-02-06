using System;
using Clicker.View;
using UnityEngine;
using Zenject;

namespace Clicker.Factories
{
    public class CrystalsFactory : MonoBehaviour
    {
        private PoolOfCrystals _poolOfCrystals;

        [Inject]
        private void Initialize(PoolOfCrystals poolOfCrystals)
        {
            _poolOfCrystals = poolOfCrystals;
        }

        internal AbstractCrystal GenerateItemInPosition()
        {
            var crystalInstance = _poolOfCrystals.GetObject();
            return crystalInstance;
        }

        internal void OnItemRemoved(AbstractCrystal crystal)
        {
            if (crystal == null)
            {
                throw new Exception("[CrystalsFactory.OnItemRemoved] crystal is null");
            }

            _poolOfCrystals.PutObject(crystal);
        }
    }
}