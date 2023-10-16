using DefaultNamespace;
using GameLogic.Components;
using GameLogic.Views;
using UnityEngine;

namespace GameLogic.Factory
{
    public class AmmoSpawnerViewFactory : BaseViewFactory<IAmmoSpawnerView>, IAmmoSpawnerViewFactory
    {
        private readonly IEcsController _ecsController;
        protected override string Path => "AmmoSpawnerView";

        public AmmoSpawnerViewFactory(IEcsController ecsController)
        {
            _ecsController = ecsController;
        }
        
        protected override void ConfigureView(int entityId, IAmmoSpawnerView view)
        {
            var entityCanGiveItemsPool = _ecsController.EcsWorld.GetPool<EntityCanGiveItemTag>();
            var positionsPool = _ecsController.EcsWorld.GetPool<PositionComponent>();
            var stashPool = _ecsController.EcsWorld.GetPool<StashComponent>();

            entityCanGiveItemsPool.Add(entityId);
            positionsPool.Add(entityId).Position =  new Vector3(6f,3f,15f);
            stashPool.Add(entityId).Size = 10;
        }
    }
}