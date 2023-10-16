using DefaultNamespace;
using GameLogic.Components;
using GameLogic.Views;
using UnityEngine;

namespace GameLogic.Factory
{
    public class AmmoReceiverViewFactory : BaseViewFactory<IAmmoReceiverView>, IAmmoReceiverViewFactory
    {
        private readonly IEcsController _ecsController;
        protected override string Path => "AmmoReceiverView";
        
        public AmmoReceiverViewFactory(IEcsController ecsController)
        {
            _ecsController = ecsController;
        }
        protected override void ConfigureView(int entityId, IAmmoReceiverView view)
        {
            var entityCanTakeItemsPool = _ecsController.EcsWorld.GetPool<EntityCanTakeItemTag>();
            var positionsPool = _ecsController.EcsWorld.GetPool<PositionComponent>();
            var stashPool = _ecsController.EcsWorld.GetPool<StashComponent>();

            entityCanTakeItemsPool.Add(entityId);
            positionsPool.Add(entityId).Position = new Vector3(-4f,3f,2f);
            stashPool.Add(entityId).Size = 50;
        }
    }
}