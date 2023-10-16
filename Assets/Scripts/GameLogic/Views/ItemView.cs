using UnityEngine;

namespace GameLogic.Views
{
    public class ItemView : MonoViewBase, IItemView
    {
        public void SetNewParent(Transform parent)
        {
            transform.parent = parent;
        }
    }
}