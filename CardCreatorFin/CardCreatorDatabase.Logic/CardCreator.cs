using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardCreatorDatabase.Domain;
using CardCreatorDatabase.Data;

namespace CardCreatorDatabase.Logic
{
   public class CardCreator
    {
        public void CreateCard(string name, int manaCost = -1,int attackPower = -1, int hp = -1)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var type = context.Types.Find(2);

                if (type != null)
                {
                    var newCard = new Card()
                    {
                        Name = name,
                        Type = type,
                        ManaCost = manaCost,
                        AttackPower = attackPower,
                        Hp = hp,
                        PowerLevel = -1
                    };
                    context.Cards.Add(newCard);
                    context.SaveChanges();
                }
            }
        }

        public void CardExcits()
        {

        }

        public void DeleteCard()
        {

        }

        
        
    }
}
