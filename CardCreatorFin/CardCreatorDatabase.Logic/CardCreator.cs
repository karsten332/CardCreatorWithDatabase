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
        public void CreateCard()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var type = context.Types.Find(2);

                if (type != null)
                {
                    var newCard = new Card()
                    {
                        Name = "Wardruid Klara2",
                        Type = type,
                        ManaCost = 3,
                        AttackPower = 1,
                        Hp = 2,
                        PowerLevel = -1
                    };
                    context.Cards.Add(newCard);
                    context.SaveChanges();
                }
            }
        }
    }
}
