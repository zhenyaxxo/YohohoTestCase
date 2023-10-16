using System.Collections.Generic;
using Leopotam.EcsLite;

namespace GameLogic.Components
{
    public struct StashComponent
    {
        public Stack<EcsPackedEntity> Items;
        public int Size;
    }
}