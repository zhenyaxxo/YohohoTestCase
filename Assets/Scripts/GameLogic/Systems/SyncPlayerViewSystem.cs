using GameLogic.Components;
using GameLogic.Factory;
using GameLogic.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Systems
{
    public class SyncPlayerViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float ITEM_SIZE = 0.2f; // ВЫНЕСТИ В КФГ
        private readonly IPlayerView _playerView;
        private readonly IItemViewFactory _itemViewFactory;
        
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<StashComponent> _stashPool;

        public SyncPlayerViewSystem(IPlayerView playerView, 
            IItemViewFactory itemViewFactory)
        {
            _playerView = playerView;
            _itemViewFactory = itemViewFactory;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world
                .Filter<PlayerTag>()
                .Inc<PositionComponent>()
                .End();

            _positionPool = _world.GetPool<PositionComponent>();
            _stashPool = _world.GetPool<StashComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entityId in _playerFilter)
            {
                var stash = _stashPool.Get(entityId);

                if (_playerView.ViewsInStashCount != stash.Items.Count)
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

                        itemView.SetNewParent(_playerView.StashTransform);

                        ref var itemPosition = ref _positionPool.Get(itemEntityId).Position;
                        itemPosition = new Vector3(0, itemIndex * ITEM_SIZE, 0);
                    }
                }

                _playerView.Position = _positionPool.Get(entityId).Position;
                _playerView.Apply();
            }
        }
    }
}