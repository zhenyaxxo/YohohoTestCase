using System.Collections.Generic;
using GameLogic.Components;
using Leopotam.EcsLite;

namespace GameLogic.Systems
{
    public class InitStashSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _stashFilter;
        private EcsPool<StashComponent> _stashPool;
        private EcsPool<StashInitializedTag> _initializedStashPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _stashFilter = _world
                .Filter<StashComponent>()
                .Exc<StashInitializedTag>()
                .End();
            
            _stashPool = _world.GetPool<StashComponent>();
            _initializedStashPool = _world.GetPool<StashInitializedTag>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _stashFilter)
            {
                ref var stashComponent = ref _stashPool.Get(entity);
                stashComponent.Items = new Stack<EcsPackedEntity>(stashComponent.Size);
                _initializedStashPool.Add(entity);
            }
        }
    }
}