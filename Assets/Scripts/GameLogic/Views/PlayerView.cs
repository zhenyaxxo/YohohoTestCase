using UnityEngine;

namespace GameLogic.Views
{
    public class PlayerView : MonoViewBase, IPlayerView
    {
        [SerializeField] private Transform _stashTransform;
        public Transform StashTransform => _stashTransform;
        public int ViewsInStashCount => _stashTransform.childCount;
    }
}