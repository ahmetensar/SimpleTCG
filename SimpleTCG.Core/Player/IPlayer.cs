using SimpleTCG.Core.Card;

namespace SimpleTCG.Core.Player
{
    public interface IPlayer
    {
        string Name { get; }
        IHand Hand { get; }
        bool CanPlay { get; }
        int Health { get; }
        int Mana { get; }
        int ManaSlot { get; }

        void RefreshMana();
        void UseMana(int mana);
        void ReceiveDamage(int damage);
    }
}
