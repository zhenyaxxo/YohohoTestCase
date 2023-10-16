using UnityEngine;

namespace GameLogic.Views
{
    public interface IStashView
    {
        public Transform StashTransform { get; }
        public int ViewsInStashCount { get; }
    }
}