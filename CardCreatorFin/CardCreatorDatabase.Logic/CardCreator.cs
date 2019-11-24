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
        public void CreateCard(string name, int selectedTypeId, string imageURL= "none", int manaCost = -1,int attackPower = -1, int hp = -1)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Type1 selectedtype = context.Types.Find(selectedTypeId);

                if (selectedtype != null)
                {                   
                   var newCard = new Card()
                        {
                            Name = name,
                            Type = selectedtype,
                            ImageURL = imageURL,
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

         public List<Card> GetCardList()
        {

            using (DatabaseContext context = new DatabaseContext())
            {
               
                //return context.Cards.ToList();
                return context.Cards.ToList();

            }
        }

        public Card GetCard()
        {
        

            using (DatabaseContext context = new DatabaseContext())
            {

                return context.Cards.Find(1);

            }
        }



    }
}
