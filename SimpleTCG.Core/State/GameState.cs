using SimpleTCG.Core.Card;
using SimpleTCG.Core.Player;
using System.Linq;

namespace SimpleTCG.Core.State
{
    public class GameState : IGameState
    {
        private readonly IPlayer _playerOne;
        private readonly IPlayer _playerTwo;

        public EActivePlayerState ActivePlayerState { get; private set; }

        public IPlayer ActivePlayer
        {
            get
            {
                switch (ActivePlayerState)
                {
                    case EActivePlayerState.PlayerOne:
                        return _playerOne;
                    case EActivePlayerState.PlayerTwo:
                        return _playerTwo;
                    default:
                        return null;
                }
            }
        }
        public IPlayer OpponentPlayer
        {
            get
            {
                switch (ActivePlayerState)
                {
                    case EActivePlayerState.PlayerOne:
                        return _playerTwo;
                    case EActivePlayerState.PlayerTwo:
                        return _playerOne;
                    default:
                        return null;
                }
            }
        }
        public IPlayer Winner
        {
            get
            {
                if (_playerOne.Health <= 0)
                {
                    return _playerTwo;
                }
                if (_playerTwo.Health <= 0)
                {
                    return _playerOne;
                }
                return null;
            }
        }

        public bool IsFinished => Winner != null;

        public GameState(IPlayer playerOne, IPlayer playerTwo)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            ActivePlayerState = EActivePlayerState.PlayerOne;
        }

        public void Use(ICard card)
        {
            int index = ActivePlayer.Hand.IndexOf(card);
            if (index >= 0)
            {
                card.Use(ActivePlayer, OpponentPlayer);
                ActivePlayer.Hand.RemoveAt(index);
            }
            if (!ActivePlayer.CanPlay)
            {
                Next();
            }
        }

        public void Next()
        {
            if (!IsFinished)
            {
                switch (ActivePlayerState)
                {
                    case EActivePlayerState.PlayerOne:
                        ActivePlayerState = EActivePlayerState.PlayerTwo;
                        break;
                    case EActivePlayerState.PlayerTwo:
                        ActivePlayerState = EActivePlayerState.PlayerOne;
                        break;
                }
                ActivePlayer.RefreshMana();
                if (!ActivePlayer.Hand.Deck.Any())
                {
                    ActivePlayer.ReceiveDamage(1);
                    Next();
                }
            }
        }
    }
}
