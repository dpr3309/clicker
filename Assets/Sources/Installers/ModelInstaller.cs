using Clicker.Model;
using UnityEngine;
using Zenject;

namespace Clicker.Installers
{
    public class ModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FieldModel>().AsSingle();
            Container.BindInterfacesTo<CoordinateProcessor>().AsSingle();
            Container.BindInterfacesTo<PlayerChipModel>().AsSingle();
            Container.BindInterfacesTo<CrystalModel>().AsSingle();
            Container.BindInterfacesTo<GameModel>().AsSingle();
        }
    }
}