using UnityEngine;

namespace GameLogic.Views
{
    public interface IViewBase
    {
        public Transform Transform { get; }
        public Vector3 Position { get; set; }
        public void Apply();
    }
}