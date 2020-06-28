using System.Collections.Generic;

namespace SimpleTCG.Core.Card
{
    public interface IHand: IList<ICard>
    {
        IList<ICard> Deck { get; }
        int InitialSize { get; }
        int MaxSize { get; }
        bool CanDraw { get; }
        void Draw();
    }
}