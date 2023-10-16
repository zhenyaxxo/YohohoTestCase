using GameLogic.Components;
using GameLogic.Factory;
using Leopotam.EcsLite;

namespace GameLogic.Systems
{
    public class InitLevelSystem : IEcsInitSystem
    {
        private readonly IAmmoSpawnerViewFactory _spawnerViewFactory;
        private readonly IAmmoReceiverViewFactory _receiverViewFactory;
        
        public InitLevelSystem(IAmmoSpawnerViewFactory spawnerViewFactory,
            IAmmoReceiverViewFactory receiverViewFactory)
        {
            _spawnerViewFactory = spawnerViewFactory;
            _receiverViewFactory = receiverViewFactory;
        }
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var spawnerNewEntity = world.NewEntity();
            world.GetPool<SpawnerItemTag>().Add(spawnerNewEntity).SpawnDelay = 2f;
            _spawnerViewFactory.CreateView(spawnerNewEntity);
            
            var receiverNewEntity = world.NewEntity();
            world.GetPool<ReceiverItemTag>().Add(receiverNewEntity);
            _receiverViewFactory.CreateView(receiverNewEntity);
        }
    }
}