using GameLogic.Views;
using Zenject;

namespace Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Joystick>().FromComponentsInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerView>().FromComponentsInHierarchy().AsSingle().NonLazy();
        }
    }
}