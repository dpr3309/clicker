using Clicker.Factories;
using Clicker.View;
using UnityEngine;
using Zenject;

public class ViewInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerView playerView = null;

    [SerializeField]
    private FloorFactory floorFactory;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerView>().FromComponentInNewPrefab(playerView).AsSingle();
        Container.BindInterfacesAndSelfTo<FloorFactory>().FromComponentInNewPrefab(floorFactory).AsSingle();

    }
}