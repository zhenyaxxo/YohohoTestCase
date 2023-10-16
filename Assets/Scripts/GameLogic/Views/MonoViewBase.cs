using UnityEngine;

namespace GameLogic.Views
{
    public class MonoViewBase : MonoBehaviour, IViewBase
    {
        public Transform Transform => transform;
        public Vector3 Position { get; set; }
        
        public virtual void Apply()
        {
            transform.localPosition = Position;
        }
        
        public void Release()
        {
            Destroy(gameObject);
        }
    }
}