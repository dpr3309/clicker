using Clicker.View;
using UnityEngine;
using Zenject;

public class ViewInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerView playerView = null;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerView>().FromComponentInNewPrefab(playerView).AsSingle();
    }
}