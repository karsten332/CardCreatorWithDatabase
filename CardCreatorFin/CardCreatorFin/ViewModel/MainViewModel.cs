using GalaSoft.MvvmLight;
using CardCreatorDatabase.Logic;
using System.Windows.Input;
using CardCreatorFin.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.ObjectModel;
using CardCreatorDatabase.Domain;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;

namespace CardCreatorFin.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Members
        private CardModel cardDataModel;
        private TypeDataModel typeDataModel;
        CardHandler CardCreator = new CardHandler();
        public int CurrentCardId { get; set; } = -1;


        #endregion
        public ICommand ClickButtonCreateCard { get; private set; }
        public ICommand ClickButtonCreateType { get; private set; }
        public ICommand ClickButtonLoadCard { get; private set; }
        public ICommand ClickButtonLoadImage { get; private set; }
        public ICommand ClickButtonImportCardJSON { get; private set; }
        public ICommand ClickButtonExportCardJSON { get; private set; }
        public ICommand ClickButtonDeleteCard { get; private set; }


        #region Constructor
        public MainViewModel()
        {
            cardDataModel = new CardModel();
            typeDataModel = new TypeDataModel();

            ClickButtonCreateCard = new RelayCommand(ClickButtonCreateCardMethod, CanExecuteClickButton);
            ClickButtonCreateType = new RelayCommand(ClickButtonCreateTypeMethod, CanExecuteClickButton);
            ClickButtonLoadImage = new RelayCommand(ClickButtonLoadImageMethod, CanExecuteClickButton);
            ClickButtonLoadCard = new RelayCommand(ClickButtonLoadCardMethod, CanExecuteClickButton);
            ClickButtonImportCardJSON = new RelayCommand(ClickButtonImportCardJSONMethod, CanExecuteClickButton);
            ClickButtonExportCardJSON = new RelayCommand(ClickButtonExportCardJSONMethod, CanExecuteClickButton);
            ClickButtonDeleteCard = new RelayCommand(ClickButtonDeleteCardMethod, CanExecuteClickButton);

            UpdateTypeList();
            UpdateCardList();

        }
        #endregion
        #region Commands
        // Create type

        private void ClickButtonCreateTypeMethod()
        {
            TypeHandler.CreateType(CreateTypeNameText, TypeMinStatText, TypeMaxStatText);

            UpdateTypeList();
            ClearAllCreateTypeFields();

        }
        // Create Card

        private void ClickButtonLoadCardMethod()
        {

            CurrentCardId = SelectedCard.Id;
            ImageSourceText = SelectedCard.ImageURL;
            NameText = SelectedCard.Name;
            SelectedTypeIdText = TypeList[SelectedCard.TypeId - 1]; // hack for å få index som begynner på null til å funke med id som starte på 1
            AttackText = SelectedCard.AttackPower;
            HpText = SelectedCard.Hp;
            ManaCostText = SelectedCard.ManaCost;
            PowerLevelText = SelectedCard.PowerLevel;
            RaisePropertyChanged("");

        }
        private void ClickButtonCreateCardMethod()
        {
            //RaisePropertyChanged("");
            if (CheckIfAttackPowerIsValid(AttackText, SelectedTypeIdText))
            {
                Card newCard = CardCreator.CreateCard(NameText, SelectedTypeIdText.Id, ImageSourceText, ManaCostText, AttackText, HpText);

                if (CardCreator.CardExists(CurrentCardId))
                {
                    CardCreator.ModifyCard(newCard, CurrentCardId);
                }
                else
                {
                    CardCreator.AddNewCardToDatabase(newCard);
                }

                UpdateCardList();
                ClearAllCreateCardFields();
            }
            else
            {

            }
        }

        private void ClickButtonLoadImageMethod()
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();
            if (openfileDialog.ShowDialog() == true)
            {
                string filePath = openfileDialog.FileName;
                if (FileValidator.ValidateImageFileType(filePath))
                {
                    ImageSourceText = openfileDialog.FileName;
                    RaisePropertyChanged("ImageSourceText");
                }
                else
                {
                    MessageBox.Show("Invalid file type, only jpeg,jpg or png");
                }

            }
        }

        private void ClickButtonImportCardJSONMethod()
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();
            if (openfileDialog.ShowDialog() == true)
            {
                string filePath = openfileDialog.FileName;
                if (FileValidator.ValidateCardFileType(filePath))
                {

                    string cardToImport;
                    cardToImport = File.ReadAllText(filePath);

                    Card resultCard = JsonConvert.DeserializeObject<Card>(cardToImport);
                    MessageBox.Show(resultCard.Name + resultCard.Id);
                    // test
                    CurrentCardId = resultCard.Id;
                    NameText = resultCard.Name;
                    SelectedTypeIdText = TypeList[resultCard.TypeId - 1]; // hack for å få index som begynner på null til å funke med id som starte på 1
                    ImageSourceText = resultCard.ImageURL;
                    ManaCostText = resultCard.ManaCost;
                    AttackText = resultCard.AttackPower;
                    HpText = resultCard.Hp;
                    PowerLevelText = resultCard.PowerLevel;

                    RaisePropertyChanged("");
                }
                else
                {
                    MessageBox.Show("Invalid file type, only json and JSON");
                }

            }

        }

        private void ClickButtonExportCardJSONMethod()
        {
            // mye fra youtube video
            Card cardtoExport = new Card()
            {
                Name = NameText,
                TypeId = SelectedTypeIdText.Id,
                ImageURL = ImageSourceText,
                ManaCost = ManaCostText,
                AttackPower = AttackText,
                Hp = HpText,
                PowerLevel = PowerLevelText
            };
            string result = JsonConvert.SerializeObject(cardtoExport);
            ClearAllCreateCardFields();
            File.WriteAllText(@"Card.json", result);

        }

        private void ClickButtonDeleteCardMethod()
        {
            CardCreator.DeleteCard(SelectedCard.Id);
            UpdateCardList();
            ClearAllCreateCardFields();
        }

        private bool CanExecuteClickButton()
        {
            return true;
        }
        #endregion

        #region Type Helper Methods
        private void ClearAllCreateTypeFields()
        {
            CreateTypeNameText = "";
            TypeMinStatText = 0;
            TypeMaxStatText = 0;

            RaisePropertyChanged("");
        }

        #endregion
        #region Card helper Methods
        private void UpdateTypeList()
        {
            TypeList = new ObservableCollection<Type1>(TypeHandler.GetTypeList());
        }

        private void UpdateCardList()
        {
            CardList = new ObservableCollection<Card>(CardCreator.GetCardList());
        }
        private bool CheckIfAttackPowerIsValid(int attackpower, Type1 selectedType)
        {
            if (attackpower <= selectedType.MinStat)
            {
                MessageBox.Show("The attackpower Value is lower than " + selectedType.MinStat +
                    "Try again with value between " + selectedType.MinStat + " and " + selectedType.MaxStat);
                return false;
            }

            if (attackpower >= selectedType.MaxStat)
            {
                MessageBox.Show("The attackpower Value is higher than the maximum." + " Try again with value between "
                    + selectedType.MinStat + " and " + selectedType.MaxStat);
                return false;
            }

            return true;
        }

        private void ClearAllCreateCardFields()
        {
            NameText = "";
            ImageSourceText = "";
            ManaCostText = 0;
            AttackText = 0;
            HpText = 0;
            CurrentCardId = -1;
            RaisePropertyChanged("");

        }
        #endregion
        #region Properties
        public string CreateTypeNameText
        {
            get { return typeDataModel.CreateTypeNameText; }
            set { typeDataModel.CreateTypeNameText = value; }
        }

        public int TypeMinStatText
        {
            get { return typeDataModel.TypeMinStatText; }
            set { typeDataModel.TypeMinStatText = value; }
        }

        public int TypeMaxStatText
        {
            get { return typeDataModel.TypeMaxStatText; }
            set { typeDataModel.TypeMaxStatText = value; }
        }
        // Create Card



        public string NameText
        {
            get { return cardDataModel.NameText; }
            set { cardDataModel.NameText = value; }
        }

        public ObservableCollection<Card> CardList
        {
            get { return cardDataModel._cardList; }
            set
            {
                cardDataModel._cardList = value;
            }
        }

        public Card SelectedCard
        {
            get { return cardDataModel.SelectedCardId; }
            set
            {
                cardDataModel.SelectedCardId = value;
            }
        }


        public ObservableCollection<Type1> TypeList
        {
            get { return cardDataModel._typeList; }
            set
            {
                cardDataModel._typeList = value;
            }
        }

        public Type1 SelectedTypeIdText
        {
            get { return cardDataModel.SelectedTypeId; }
            set
            {
                cardDataModel.SelectedTypeId = value;
            }
        }

        public string ImageSourceText
        {
            get { return cardDataModel.ImageSourceText; }
            set { cardDataModel.ImageSourceText = value; }
        }




        public int AttackText
        {
            get { return cardDataModel.AttackText; }
            set { cardDataModel.AttackText = value; }
        }

        public int HpText
        {
            get { return cardDataModel.HpText; }
            set { cardDataModel.HpText = value; }
        }

        public int ManaCostText
        {
            get { return cardDataModel.ManaCostText; }
            set { cardDataModel.ManaCostText = value; }
        }

        public int PowerLevelText
        {
            get { return cardDataModel.PowerLevelText; }
            set { cardDataModel.PowerLevelText = value; }
        }

        #endregion


    }
}