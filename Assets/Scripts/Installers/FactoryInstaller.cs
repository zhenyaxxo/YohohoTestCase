using GameLogic.Factory;
using Zenject;

namespace Installers
{
    public class FactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ItemViewFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AmmoSpawnerViewFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AmmoReceiverViewFactory>().AsSingle().NonLazy();
        }
    }
}