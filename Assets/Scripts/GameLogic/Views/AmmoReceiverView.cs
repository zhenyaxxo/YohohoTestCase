using UnityEngine;

namespace GameLogic.Views
{
    public class AmmoReceiverView : MonoViewBase, IAmmoReceiverView
    {
        [SerializeField] private Transform _stashTransform;
        public Transform StashTransform => _stashTransform;
        public int ViewsInStashCount => StashTransform.childCount;

    }
}