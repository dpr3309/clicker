using Clicker.Factories;
using Clicker.View;
using UnityEngine;
using Zenject;

public class ViewInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerView playerView = null;

    [SerializeField]
    private FloorFactory floorFactory = null;

    [SerializeField]
    private CrystalsFactory crystalsFactory = null;

    [SerializeField]
    private AbstractCrystal crystalPrototype = null;

    [SerializeField]
    private PoolOfCrystals poolOfCrystals = null;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerView>().FromComponentInNewPrefab(playerView).AsSingle();
        Container.BindInterfacesAndSelfTo<FloorFactory>().FromComponentInNewPrefab(floorFactory).AsSingle();
        Container.BindInterfacesAndSelfTo<AbstractCrystal>().FromComponentInNewPrefab(crystalPrototype).AsSingle();
        Container.BindInterfacesAndSelfTo<PoolOfCrystals>().FromComponentInNewPrefab(poolOfCrystals).AsSingle();
        Container.BindInterfacesAndSelfTo<CrystalsFactory>().FromComponentInNewPrefab(crystalsFactory).AsSingle();
    }
}