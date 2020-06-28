using SimpleTCG.Core.Player;

namespace SimpleTCG.Core.Card
{
    public class DamageCard : ICard
    {
        public int ManaCost { get; }
        public int Damage { get; }

        public DamageCard(int manaCost, int damage)
        {
            ManaCost = manaCost;
            Damage = damage;
        }

        public bool Use(IPlayer activePlayer, IPlayer opponentPlayer)
        {
            if (activePlayer.Mana >= ManaCost)
            {
                activePlayer.UseMana(ManaCost);
                opponentPlayer.ReceiveDamage(Damage);
                return true;
            }
            return false;
        }
    }
}
