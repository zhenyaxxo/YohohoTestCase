using System.Collections.Generic;
using GameLogic.Views;
using UnityEngine;

namespace GameLogic.Factory
{
    public abstract class BaseViewFactory<T> : IViewFactory<T> where T : IViewBase
    {
        private const int PoolCapacityDefault = 256;

        private readonly Dictionary<int, MonoViewBase> _gameObjects = 
            new Dictionary<int, MonoViewBase>(PoolCapacityDefault);
        private readonly Dictionary<int, T> _views =
            new Dictionary<int, T>(PoolCapacityDefault);
        
        protected abstract string Path { get; } //Сделано только для быстрой реализации, так нужен какой нить poolService

        public T CreateView(int entityId)
        {
            var go = Object.Instantiate(Resources.Load<MonoViewBase>(Path));
            var view = go.GetComponent<T>();
            _views.Add(entityId, view);
            _gameObjects.Add(entityId, go);
            ConfigureView(entityId, view);
            return view;
        }

        public bool TryGetView(int entityId, out T view)
        {
            return _views.TryGetValue(entityId, out view);
        }

        public T GetView(int entityId)
        {
            return _views[entityId];
        }

        public void ReleaseView(int entityId)
        {
            if (_gameObjects.Remove(entityId, out var go))
            {
                go.Release();
            }

            _views.Remove(entityId);
        }

        protected abstract void ConfigureView(int entityId, T view);
    }
}