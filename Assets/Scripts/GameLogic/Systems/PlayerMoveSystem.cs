using GameLogic.Components;
using Joystick_Pack.Scripts.Joysticks;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Systems
{
    public class PlayerMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float MOVE_SPEED = 5f; // ВЫНЕСТИ В КОНФИГ
        private readonly IJoystickInput _joystickInput;
        
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<PositionComponent> _positionPool;

        public PlayerMoveSystem(IJoystickInput joystickInput)
        {
            _joystickInput = joystickInput;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world
                .Filter<PlayerTag>()
                .Inc<PositionComponent>()
                .End();

            _positionPool = _world.GetPool<PositionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var playerId in _filter)
            {
                _positionPool.Get(playerId).Position +=
                    new Vector3(_joystickInput.Direction.x, 0, _joystickInput.Direction.y)
                    * MOVE_SPEED * Time.deltaTime;
            }
        }
    }
}