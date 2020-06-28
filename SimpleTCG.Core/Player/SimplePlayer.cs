using SimpleTCG.Core.Card;
using System.Linq;

namespace SimpleTCG.Core.Player
{
    public class SimplePlayer : IPlayer
    {
        private readonly int _maxMana;

        public string Name { get; }
        public IHand Hand { get; }
        public bool CanPlay => Hand.Any(c => c.ManaCost <= Mana);
        public int Health { get; set; }
        public int Mana { get; set; }
        public int ManaSlot { get; set; }

        public SimplePlayer(string name, IHand hand, int initialHealth, int initialMana, int maxMana)
        {
            Name = name;
            Hand = hand;
            Health = initialHealth;
            _maxMana = maxMana;

            Mana = initialMana;
            ManaSlot = initialMana;
        }

        public void RefreshMana()
        {
            if (ManaSlot < _maxMana)
            {
                ManaSlot++;
            }
            Mana = ManaSlot;
        }

        public void UseMana(int mana)
        {
            if (Mana > mana)
            {
                Mana -= mana;
            }
            else
            {
                Mana = 0;
            }
        }

        public void ReceiveDamage(int damage)
        {
            if (Health > damage)
            {
                Health -= damage;
            }
            else
            {
                Health = 0;
            }
        }
    }
}
