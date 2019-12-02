
using CardCreatorDatabase.Domain;

namespace CardCreatorFin.Model
{
    public class CardModel
    {
        public CardModel()
        {
            NameText = "Hello";
        }

        // Create Card
        public System.Collections.ObjectModel.ObservableCollection<Card> _cardList;

        public System.Collections.ObjectModel.ObservableCollection<Type1> _typeList;
        public Card SelectedCardId { get; set; }

        public string NameText { get; set; }



        // Image
        public string ImageSourceText { get; set; }

        public Type1 SelectedTypeId { get; set; }
        public int AttackText { get; set; }
        public int HpText { get; set; }
        public int ManaCostText { get; set; }
        public int PowerLevelText { get; set; }
    }
}
