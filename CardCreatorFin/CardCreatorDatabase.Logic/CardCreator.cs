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
        public Card CreateCard(string name, int selectedTypeId, string imageURL = "none", int manaCost = -1, int attackPower = -1, int hp = -1)
        {
            var newCard = new Card()
            {
                Name = name,
                TypeId = selectedTypeId,
                ImageURL = imageURL,
                ManaCost = manaCost,
                AttackPower = attackPower,
                Hp = hp,
                PowerLevel = -1
            };

            return newCard;

        }
        public void AddNewCardToDatabase(Card newCard)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Type1 selectedtype = context.Types.Find(newCard.TypeId);

                if (selectedtype != null)
                {
                    context.Cards.Add(newCard);
                    context.SaveChanges();
                }

            }
        }
        public void ModifyCard(Card modiftedCard,int currentCardId)
        {

            using (DatabaseContext context = new DatabaseContext())
            {

                Card cardInDatabase = context.Cards.Find(currentCardId);
                if (cardInDatabase != null)
                {
                    cardInDatabase.Name = modiftedCard.Name;
                    cardInDatabase.TypeId = modiftedCard.TypeId;
                    cardInDatabase.ImageURL = modiftedCard.ImageURL;
                    cardInDatabase.ManaCost = modiftedCard.ManaCost;
                    cardInDatabase.AttackPower = modiftedCard.AttackPower;
                    cardInDatabase.Hp = modiftedCard.Hp;
                    cardInDatabase.PowerLevel = modiftedCard.PowerLevel;

                    context.SaveChanges();
                }

            }
        }
        public bool CardExists(int cardId)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Card foundCard;
                foundCard = context.Cards.Find(cardId);
                if (foundCard != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //context.SaveChanges();
            }
        }

        public void DeleteCard(int cardId)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                /*
                IQueryable<Card> card = context.Cards.Where(c => c.Id == cardId)
                context.Cards.Attach(card);
                context.Cards.Remove(card);
                */
                context.Cards.Remove(context.Cards.Find(cardId));
                context.SaveChanges();
            }
        }

        public List<Card> GetCardList()
        {

            using (DatabaseContext context = new DatabaseContext())
            {

                //return context.Cards.ToList();
                return context.Cards.ToList();

            }
        }

    }
}
