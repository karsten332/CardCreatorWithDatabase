using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardCreatorDatabase.Domain;
using CardCreatorDatabase.Data;


namespace CardCreatorDatabase.Logic
{
  public static class TypeCreator
    {
        static public void CreateType()
        {
            var newType = new Type1()
            {
                Name = "Test"
            };

            using (DatabaseContext context = new DatabaseContext())
            {
                context.Types.Add(newType);
                context.SaveChanges();

                foreach (var item in context.Types)
                {
                    //Console.WriteLine("{0} {1}", item.Id, item.Name);
                }
            }
        }
    }
    /* 
     *
        public void CreateCard()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var type = context.Types.Find(1);

                if (type != null)
                {
                    var newCard = new Card()
                    {
                        Name = "Wardruid Klara",
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
        */
}
