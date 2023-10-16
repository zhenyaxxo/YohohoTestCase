using GameLogic.Components;
using GameLogic.Factory;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Systems
{
    public class SyncSpawnerViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float ITEM_SIZE = 2f; // ВЫНЕСТИ В КФГ
        
        private readonly IAmmoSpawnerViewFactory _ammoSpawnerViewFactory;
        private readonly IItemViewFactory _itemViewFactory;
        
        private EcsWorld _world;
        private EcsFilter _spawnersFilter;
        private EcsPool<StashComponent> _stashPool;
        private EcsPool<PositionComponent> _positionPool;

        public SyncSpawnerViewSystem(IAmmoSpawnerViewFactory ammoSpawnerViewFactory,
            IItemViewFactory itemViewFactory)
        {
            _ammoSpawnerViewFactory = ammoSpawnerViewFactory;
            _itemViewFactory = itemViewFactory;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _spawnersFilter = _world
                .Filter<SpawnerItemTag>()
                .Inc<StashComponent>()
                .Inc<StashInitializedTag>()
                .End();

            _stashPool = _world.GetPool<StashComponent>();
            _positionPool = _world.GetPool<PositionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entityId in _spawnersFilter)
            {
                if (!_ammoSpawnerViewFactory.TryGetView(entityId, out var view))
                {
                    view = _ammoSpawnerViewFactory.CreateView(entityId);
                }

                var stash = _stashPool.Get(entityId);

                if (view.ViewsInStashCount != stash.Items.Count)
                {
                    var itemIndex = -1;
                    foreach (var packedEntity in stash.Items)
                    {
                        itemIndex++;

                        if (!packedEntity.Unpack(_world, out var itemEntityId))
                        {
                            Debug.LogError("Сущности не существует!");
                            break;
                        }

                        if (!_itemViewFactory.TryGetView(itemEntityId, out var itemView))
                        {
                            Debug.LogError("View для этого предмета не создана!");
                            break;
                        }

                        itemView.SetNewParent(view.StashTransform);

                        ref var itemPosition = ref _positionPool.Get(itemEntityId).Position;
                        itemPosition = new Vector3(0, itemIndex * ITEM_SIZE, 0);
                    }
                }

                view.Position = _positionPool.Get(entityId).Position;
                view.Apply();
            }
        }
    }
}