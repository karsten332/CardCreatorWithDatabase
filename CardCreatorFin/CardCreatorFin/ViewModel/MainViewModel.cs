using GalaSoft.MvvmLight;
using CardCreatorDatabase.Logic;
using System.Windows.Input;
using CardCreatorFin.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows;

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
        private DataModel model;
        CardCreator CardCreator = new CardCreator();
        public ICommand ClickButtonCreateCard { get; private set; }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //TypeCreator.CreateType();
            model = new DataModel();
            ClickButtonCreateCard = new RelayCommand(ClickButtonCreateCardMethod, CanExecuteClickButton);


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
        // members 
        public string NameText
        {
            get { return model.NameText; }
            set { model.NameText = value; }
        }

        public string TypeText
        {
            get { return model.TypeText; }
            set { model.TypeText = value; }
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




    }
}