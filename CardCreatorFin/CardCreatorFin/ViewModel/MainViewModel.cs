using GalaSoft.MvvmLight;
using CardCreatorDatabase.Logic;
using System.Windows.Input;
using CardCreatorFin.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.ObjectModel;
using CardCreatorDatabase.Domain;
using CardCreatorDatabase.Data;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;

namespace CardCreatorFin.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Members
        private DataModel model;
        CardCreator CardCreator = new CardCreator();
        public int CurrentCardId { get; set; } = -1;


        #endregion


        public ICommand ClickButtonCreateCard { get; private set; }
        public ICommand ClickButtonCreateType { get; private set; }
        public ICommand ClickButtonLoadCard { get; private set; }
        // own viewmodel
        public ICommand ClickButtonLoadImage { get; private set; }
        public ICommand ClickButtonImportCardJSON { get; private set; }
        public ICommand ClickButtonExportCardJSON { get; private set; }
        public ICommand ClickButtonDeleteCard { get; private set; }


        #region Constructor
        public MainViewModel()
        {
            //TypeCreator.CreateType();
            model = new DataModel();
            ClickButtonCreateCard = new RelayCommand(ClickButtonCreateCardMethod, CanExecuteClickButton);
            ClickButtonCreateType = new RelayCommand(ClickButtonCreateTypeMethod, CanExecuteClickButton);
            ClickButtonLoadImage = new RelayCommand(ClickButtonLoadImageMethod, CanExecuteClickButton);
            ClickButtonLoadCard = new RelayCommand(ClickButtonLoadCardMethod, CanExecuteClickButton); // gir type = null!
            ClickButtonImportCardJSON = new RelayCommand(ClickButtonImportCardJSONMethod, CanExecuteClickButton);
            ClickButtonExportCardJSON = new RelayCommand(ClickButtonExportCardJSONMethod, CanExecuteClickButton);
            ClickButtonDeleteCard = new RelayCommand(ClickButtonDeleteCardMethod, CanExecuteClickButton);

            UpdateTypeList();
            UpdateCardList();
            //DatabaseContext context = new DatabaseContext();

        }
        #endregion
        #region Commands
        // Create type

        private void ClickButtonCreateTypeMethod()
        {
            TypeCreator.CreateType(CreateTypeNameText, TypeMinStatText, TypeMaxStatText);

            UpdateTypeList();
            ClearAllCreateTypeFields();

        }
        // Create Card

        private void ClickButtonLoadCardMethod()
        {
            // bytte navn på variable til selectedcardText
            //MessageBox.Show(SelectedCardIdText.Name);

            CurrentCardId = SelectedCardIdText.Id;
            ImageSourceText = SelectedCardIdText.ImageURL;
            NameText = SelectedCardIdText.Name;
            SelectedTypeIdText = TypeList[SelectedCardIdText.TypeId - 1]; // hack for å få index som begynner på null til å funke med id som starte på 1
            AttackText = SelectedCardIdText.AttackPower;
            HpText = SelectedCardIdText.Hp;
            ManaCostText = SelectedCardIdText.ManaCost;
            PowerLevelText = SelectedCardIdText.PowerLevel;
            RaisePropertyChanged("");
            //MessageBox.Show(SelectedCardIdText.Type.Name); // null value

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
                MessageBox.Show("The attackpower Value is invalid for this type, try again");
            }



            string TestText = SelectedTypeIdText.Id.ToString(); //TypeList[1].Name;


            //MessageBox.Show(TestText);
        }

        private void ClickButtonLoadImageMethod()
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();
            if (openfileDialog.ShowDialog() == true)
            {
                string filePath = openfileDialog.FileName;
                if (ValidateImageFileType(filePath))
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
                if (ValidateCardFileType(filePath))
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

                    //MessageBox.Show(SelectedTypeIdText.Name); 

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
            //MessageBox.Show(result);
            File.WriteAllText(@"Card.json", result);

        }

        private void ClickButtonDeleteCardMethod()
        {
            //MessageBox.Show("Not implemented");
            CardCreator.DeleteCard(SelectedCardIdText.Id);
            UpdateCardList();
            ClearAllCreateCardFields();
        }

        private bool CanExecuteClickButton()
        {
            return true;
        }
        #endregion
        #region Properties

        // Create Type helpers
        private void ClearAllCreateTypeFields()
        {
            CreateTypeNameText = "";
            TypeMinStatText = 0;
            TypeMaxStatText = 0;

            RaisePropertyChanged("");
        }


        // Card creator helper methods
        private void UpdateTypeList()
        {
            TypeList = new ObservableCollection<Type1>(TypeCreator.GetTypeList());
        }

        private void UpdateCardList()
        {
            CardList = new ObservableCollection<Card>(CardCreator.GetCardList());
        }
        private bool CheckIfAttackPowerIsValid(int attackpower, Type1 selectedType)
        {
            if (attackpower <= selectedType.MinStat)
            {
                return false;
            }

            if (attackpower > selectedType.MaxStat)
            {
                return false;
            }

            return true;
        }

        private void ClearAllCreateCardFields()
        {
            NameText = "";
            //SelectedTypeIdText
            ImageSourceText = "";
            ManaCostText = 0;
            AttackText = 0;
            HpText = 0;
            CurrentCardId = -1;
            RaisePropertyChanged("");

        }

        private bool ValidateCardFileType(string pathToValdiate)
        {
            string result = Path.GetExtension(pathToValdiate);
            switch (result)
            {
                case ".json":
                    return true;
                case ".JSON":
                    return true;
                default:
                    break;
            }
            return false;

        }

        private bool ValidateImageFileType(string pathToValdiate)
        {
            string result = Path.GetExtension(pathToValdiate);
            switch (result)
            {
                case ".png":
                    return true;
                case ".PNG":
                    return true;
                case ".jpeg":
                    return true;
                case ".JPG":
                    return true;
                case ".jpg":
                    return true;


                default:
                    break;
            }
            return false;

        }
        // Create type


        public string CreateTypeNameText
        {
            get { return model.CreateTypeNameText; }
            set { model.CreateTypeNameText = value; }
        }

        public int TypeMinStatText
        {
            get { return model.TypeMinStatText; }
            set { model.TypeMinStatText = value; }
        }

        public int TypeMaxStatText
        {
            get { return model.TypeMaxStatText; }
            set { model.TypeMaxStatText = value; }
        }
        // Create Card



        public string NameText
        {
            get { return model.NameText; }
            set { model.NameText = value; }
        }

        public ObservableCollection<Card> CardList
        {
            get { return model._cardList; }
            set
            {
                model._cardList = value;
            }
        }

        public Card SelectedCardIdText
        {
            get { return model.SelectedCardId; }
            set
            {
                model.SelectedCardId = value;
            }
        }


        public ObservableCollection<Type1> TypeList
        {
            get { return model._typeList; }
            set
            {
                model._typeList = value;
            }
        }

        public Type1 SelectedTypeIdText
        {
            get { return model.SelectedTypeId; }
            set
            {
                model.SelectedTypeId = value;
            }
        }

        public string ImageSourceText
        {
            get { return model.ImageSourceText; }
            set { model.ImageSourceText = value; }
        }




        public int AttackText
        {
            get { return model.AttackText; }
            set { model.AttackText = value; }
        }

        public int HpText
        {
            get { return model.HpText; }
            set { model.HpText = value; }
        }

        public int ManaCostText
        {
            get { return model.ManaCostText; }
            set { model.ManaCostText = value; }
        }

        public int PowerLevelText
        {
            get { return model.PowerLevelText; }
            set { model.PowerLevelText = value; }
        }

        #endregion


    }
}