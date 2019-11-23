using GalaSoft.MvvmLight;
using CardCreatorDatabase.Logic;
using System.Windows.Input;
using CardCreatorFin.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.ObjectModel;
using CardCreatorDatabase.Domain;
using CardCreatorDatabase.Data;

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

        

        #endregion


        public ICommand ClickButtonCreateCard { get; private set; }
        public ICommand ClickButtonCreateType { get; private set; }
        // own viewmodel
        public ICommand ClickButtonLoadImage { get; private set; }

        #region Constructor
        public MainViewModel()
        {
            //TypeCreator.CreateType();
            model = new DataModel();
            ClickButtonCreateCard = new RelayCommand(ClickButtonCreateCardMethod, CanExecuteClickButton);
            ClickButtonCreateType = new RelayCommand(ClickButtonCreateTypeMethod, CanExecuteClickButton);
            ClickButtonLoadImage = new RelayCommand(ClickButtonLoadImageMethod, CanExecuteClickButton);

            // Test


            TypeList = new ObservableCollection<Type1>(TypeCreator.GetTypeList());
            //DatabaseContext context = new DatabaseContext();

        }
        #endregion
        #region Commands
        // Create type

        private void ClickButtonCreateTypeMethod()
        {
            TypeCreator.CreateType(CreateTypeNameText,TypeMinStatText,TypeMaxStatText);
            ClearAllCreateTypeFields();
        }

        private void ClickButtonCreateCardMethod()
        {
            //RaisePropertyChanged("");
            if (CheckIfAttackPowerIsValid(AttackText, SelectedTypeIdText))
            {
                CardCreator.CreateCard(NameText, SelectedTypeIdText.Id, ManaCostText, AttackText, HpText);
                ClearAllCreateCardFields();
            } else
            {
                MessageBox.Show("The attackpower Value is invalid, try again");
            }
           


            string TestText = SelectedTypeIdText.Id.ToString(); //TypeList[1].Name;


            //MessageBox.Show(TestText);
        }

        private void ClickButtonLoadImageMethod()
        {

            ImageSourceText = "/Images/redSquare.png";
            RaisePropertyChanged("");
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
            ManaCostText = 0;
            AttackText = 0;
            HpText = 0;

            RaisePropertyChanged("");

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

        public ObservableCollection<Type1> TypeList
        {
            get { return model._typeList; }
            set
            {
                model._typeList = value;
            }
        }

        public string ImageSourceText
        {
            get { return model.ImageSourceText; }
            set { model.ImageSourceText = value; }
        }

        public Type1 SelectedTypeIdText
        {
            get { return model.SelectedTypeId; }
            set
            {
                model.SelectedTypeId = value;
            }
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