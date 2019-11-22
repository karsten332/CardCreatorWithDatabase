using GalaSoft.MvvmLight;
using CardCreatorDatabase.Logic;
using System.Windows.Input;
using CardCreatorFin.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.ObjectModel;
using CardCreatorDatabase.Domain;

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

        private ObservableCollection<Type1> _typeList;

        #endregion
        public ObservableCollection<Type1> TypeList
        {
            get { return _typeList; }
            set { _typeList = value;
            }
        }

        public ICommand ClickButtonCreateCard { get; private set; }
        public ICommand ClickButtonCreateType { get; private set; }
        
        #region Constructor
        public MainViewModel()
        {
            //TypeCreator.CreateType();
            model = new DataModel();
            ClickButtonCreateCard = new RelayCommand(ClickButtonCreateCardMethod, CanExecuteClickButton);
            ClickButtonCreateType = new RelayCommand(ClickButtonCreateTypeMethod, CanExecuteClickButton);

            TypeList = new ObservableCollection<Type1>()
            {
                new Type1(){Id=1, Name="Normal"},
                new Type1(){Id=2, Name="Spescial"}
            };

        }
        #endregion
        #region Commands
        // Create type

        private void ClickButtonCreateTypeMethod()
        {
            TypeCreator.CreateType(CreateTypeNameText);
        }

        private void ClickButtonCreateCardMethod()
        {
            //RaisePropertyChanged("");

            CardCreator.CreateCard(NameText);
            
            //TestText = "faen";

            MessageBox.Show(NameText);
        }

        private bool CanExecuteClickButton()
        {
            return true;
        }
        #endregion
        #region Properties

        // Create type

        
        public string CreateTypeNameText
        {
            get { return model.CreateTypeNameText; }
            set { model.CreateTypeNameText = value; }
        }

        public string NameText
        {
            get { return model.NameText; }
            set { model.NameText = value; }
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