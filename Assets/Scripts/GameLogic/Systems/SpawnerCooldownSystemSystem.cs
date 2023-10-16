using GameLogic.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Systems
{
    public class SpawnerCooldownSystemSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _spawnerFilter;
        private EcsPool<SpawnerInCooldownTag> _spawnersInCooldownPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _spawnerFilter = _world
                .Filter<SpawnerItemTag>()
                .Inc<SpawnerInCooldownTag>()
                .End();

            _spawnersInCooldownPool = _world.GetPool<SpawnerInCooldownTag>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entityId in _spawnerFilter)
            {
                ref var cooldownComponent = ref _spawnersInCooldownPool.Get(entityId);

                if (cooldownComponent.SecondsLeft > 0)
                {
                    cooldownComponent.SecondsLeft -= Time.deltaTime;
                    continue;
                }
                
                _spawnersInCooldownPool.Del(entityId);
            }
        }
    }
}