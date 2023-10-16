using GameLogic.Components;
using Leopotam.EcsLite;

namespace GameLogic.Systems
{
    public class CleanFullStashSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _fullStashFilter;
        private EcsPool<FullStashTag> _fullStashPool;
        private EcsPool<StashComponent> _stashPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _fullStashFilter = _world
                .Filter<FullStashTag>()
                .Inc<StashComponent>()
                .End();

            _stashPool = _world.GetPool<StashComponent>();
            _fullStashPool = _world.GetPool<FullStashTag>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entityId in _fullStashFilter)
            {
                var stashComponent = _stashPool.Get(entityId);

                if (stashComponent.Items.Count < stashComponent.Size)
                {
                    _fullStashPool.Del(entityId);
                }
            }
        }
    }
}