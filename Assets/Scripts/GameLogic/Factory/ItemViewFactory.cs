using DefaultNamespace;
using GameLogic.Components;
using GameLogic.Views;

namespace GameLogic.Factory
{
    public class ItemViewFactory : BaseViewFactory<IItemView>, IItemViewFactory
    {
        private readonly IEcsController _ecsController;
        protected override string Path => "ItemView";

        public ItemViewFactory(IEcsController ecsController)
        {
            _ecsController = ecsController;
        }
        protected override void ConfigureView(int entityId, IItemView view)
        {
            _ecsController.EcsWorld.GetPool<PositionComponent>().Add(entityId);
        }
    }
}