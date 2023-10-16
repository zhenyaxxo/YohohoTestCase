using DefaultNamespace;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

public class EcsController : MonoBehaviour, IEcsController
{
    [Inject] private IEcsSystem[] _ecsSystems;
    
    private EcsWorld _world;
    private IEcsSystems _systems;
    public EcsWorld EcsWorld => _world ??= new EcsWorld();
    
    private void Awake()
    {
        _systems = new EcsSystems(EcsWorld);
        
        foreach (var ecsInitSystem in _ecsSystems)
        {
            _systems.Add(ecsInitSystem);
        }
        
        _systems.Init();
    }

    private void Update()
    {
        _systems?.Run();
    }
}
