using Clicker.ViewModel;
using Zenject;

namespace Clicker.Installers
{
    public class ViewModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameViewModel>().AsSingle();
            Container.BindInterfacesTo<FieldViewModel>().AsSingle();
            Container.BindInterfacesTo<PlayerChipViewModel>().AsSingle();
            Container.BindInterfacesTo<CrystalViewModel>().AsSingle();
            Container.BindInterfacesTo<ScoreViewModel>().AsSingle();
        }
    }
}