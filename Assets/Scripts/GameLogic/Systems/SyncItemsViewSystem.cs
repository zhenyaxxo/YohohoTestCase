using GameLogic.Components;
using GameLogic.Factory;
using Leopotam.EcsLite;

namespace GameLogic.Systems
{
    public class SyncItemsViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IItemViewFactory _itemViewFactory;
        
        private EcsWorld _world;
        private EcsFilter _itemsFilter;
        private EcsPool<PositionComponent> _positionPool;

        public SyncItemsViewSystem(IItemViewFactory itemViewFactory)
        {
            _itemViewFactory = itemViewFactory;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _itemsFilter = _world
                .Filter<ItemTag>()
                .End();

            _positionPool = _world.GetPool<PositionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entityId in _itemsFilter)
            {
                if (!_itemViewFactory.TryGetView(entityId, out var view))
                {
                    view = _itemViewFactory.CreateView(entityId);
                }

                view.Position = _positionPool.Get(entityId).Position;
                view.Apply();
            }
        }
    }
}