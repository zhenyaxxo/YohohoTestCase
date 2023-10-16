using System.Collections.Generic;
using GameLogic.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Systems
{
    public class TradeItemsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float DISATANCE_TO_TAKE = 5f; //TODO: вынести в какой нибудь конфиг
        
        private EcsWorld _world;
        private EcsFilter _entityCanGiveItemFilter;
        private EcsFilter _entityCanTakeItemFilter;

        private EcsPool<StashComponent> _stashPool;
        private EcsPool<PositionComponent> _positionPool;

        private EcsPool<FullStashTag> _fullStashPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _entityCanGiveItemFilter = _world
                .Filter<EntityCanGiveItemTag>()
                .Inc<PositionComponent>()
                .Inc<StashComponent>()
                .Inc<StashInitializedTag>()
                .End();
            
            _entityCanTakeItemFilter = _world
                .Filter<EntityCanTakeItemTag>()
                .Inc<PositionComponent>()
                .Inc<StashComponent>()
                .Inc<StashInitializedTag>()
                .Exc<FullStashTag>()
                .End();

            _stashPool = _world.GetPool<StashComponent>();
            _positionPool = _world.GetPool<PositionComponent>();
            _fullStashPool = _world.GetPool<FullStashTag>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entityId in _entityCanGiveItemFilter)
            {
                var entityGivePosition = _positionPool.Get(entityId).Position;
                var stashToGive = _stashPool.Get(entityId);

                foreach (var id in _entityCanTakeItemFilter)
                {
                    if (id == entityId)
                    {
                        continue;
                    }
                    
                    if (stashToGive.Items.Count == 0)
                    {
                        break;
                    }
                    
                    var entityTakePosition = _positionPool.Get(id).Position;

                    if ((entityGivePosition - entityTakePosition).magnitude > DISATANCE_TO_TAKE)
                    {
                        continue;
                    }
                    
                    var stashToTake = _stashPool.Get(id);
                    
                    var itemsToGiveCount = stashToTake.Size - stashToTake.Items.Count;

                    if (itemsToGiveCount > stashToGive.Items.Count)
                    {
                        itemsToGiveCount = stashToGive.Items.Count;
                    }

                    RearrangeObjects(stashToGive.Items, stashToTake.Items, itemsToGiveCount);

                    if (itemsToGiveCount == 0)
                    {
                        _fullStashPool.Add(id);
                    }
                }
            }
        }

        private void RearrangeObjects(Stack<EcsPackedEntity> giveQueue, Stack<EcsPackedEntity> takeQueue, int count)
        {
            for (var j = 0; j < count; j++)
            {
                var item = giveQueue.Pop();
                takeQueue.Push(item);
            }
        }
    }
}