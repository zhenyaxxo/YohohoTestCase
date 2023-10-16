using UnityEngine;

namespace GameLogic.Views
{
    public interface IItemView : IViewBase
    {
        public void SetNewParent(Transform parent);
    }
}