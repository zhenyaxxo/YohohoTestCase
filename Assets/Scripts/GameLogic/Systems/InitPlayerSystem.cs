using GameLogic.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Systems
{
    public class InitPlayerSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var playerPool = world.GetPool<PlayerTag>();
            var entitiesCanTakeItemPool = world.GetPool<EntityCanTakeItemTag>();
            var entitiesCanGiveItemPool = world.GetPool<EntityCanGiveItemTag>();
            var stashPool = world.GetPool<StashComponent>();
            var positionPool = world.GetPool<PositionComponent>();

            var playerEntity = world.NewEntity();
            playerPool.Add(playerEntity);
            entitiesCanTakeItemPool.Add(playerEntity);
            entitiesCanGiveItemPool.Add(playerEntity);
            stashPool.Add(playerEntity).Size = 10;
            positionPool.Add(playerEntity).Position = new Vector3(2f,3,-6f);
        }
    }
}