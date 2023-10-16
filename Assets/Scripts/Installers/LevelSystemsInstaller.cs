using GameLogic.Systems;
using Zenject;

#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif

namespace Installers
{
    public class LevelSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InitStashSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InitPlayerSystem>().AsSingle().NonLazy();

            Container.BindInterfacesTo<SpawnerDoWorkSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SpawnerCooldownSystemSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<TradeItemsSystem>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<SyncItemsViewSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SyncSpawnerViewSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SyncReceiverViewSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerMoveSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SyncPlayerViewSystem>().AsSingle().NonLazy();

            Container.BindInterfacesTo<CleanFullStashSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InitLevelSystem>().AsSingle().NonLazy();

#if UNITY_EDITOR
            Container.BindInterfacesTo<EcsWorldDebugSystem>().AsSingle().NonLazy();
#endif
            
            Container.BindInterfacesTo<EcsController>().FromComponentsInHierarchy().AsSingle().NonLazy();
        }
    }
}