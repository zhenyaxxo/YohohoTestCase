namespace GameLogic.Factory
{
    public interface IViewFactory<TView>
    {
        public TView CreateView(int entityId);
        public bool TryGetView(int entityId, out TView view);
        public TView GetView(int entityId);
        public void ReleaseView(int entityId);
    }
}