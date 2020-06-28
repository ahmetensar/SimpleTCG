using SimpleTCG.Core.Card;
using SimpleTCG.Core.Player;

namespace SimpleTCG.Core.State
{
    public interface IGameState
    {
        EActivePlayerState ActivePlayerState { get; }
        IPlayer ActivePlayer { get; }
        IPlayer OpponentPlayer { get; }
        IPlayer Winner { get; }
        bool IsFinished { get; }

        void Use(ICard card);
        void Next();
    }
}