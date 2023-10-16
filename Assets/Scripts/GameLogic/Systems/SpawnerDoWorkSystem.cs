using GameLogic.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Systems
{
    public class SpawnerDoWorkSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _spawnersFilter;
        private EcsPool<SpawnerItemTag> _spawnersPool;
        private EcsPool<SpawnerInCooldownTag> _spawnersCooldownPool;
        private EcsPool<FullStashTag> _fullStashPool;
        private EcsPool<StashComponent> _stashPool;
        private EcsPool<ItemTag> _itemsPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _spawnersFilter = _world
                .Filter<SpawnerItemTag>()
                .Inc<StashComponent>()
                .Inc<StashInitializedTag>()
                .Exc<SpawnerInCooldownTag>()
                .Exc<FullStashTag>()
                .End();

            _spawnersPool = _world.GetPool<SpawnerItemTag>();
            _spawnersCooldownPool = _world.GetPool<SpawnerInCooldownTag>();
            _fullStashPool = _world.GetPool<FullStashTag>();
            _stashPool = _world.GetPool<StashComponent>();
            _itemsPool = _world.GetPool<ItemTag>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entityId in _spawnersFilter)
            {
                var stash = _stashPool.Get(entityId);

                if (stash.Items.Count == stash.Size)
                {
                    _fullStashPool.Add(entityId);
                    continue;
                }

                var newItem = _world.NewEntity();
                _itemsPool.Add(newItem);
                stash.Items.Push(_world.PackEntity(newItem));

                var spawnerDelay = _spawnersPool.Get(entityId).SpawnDelay;
                _spawnersCooldownPool.Add(entityId).SecondsLeft = spawnerDelay;
            }
        }
    }
}