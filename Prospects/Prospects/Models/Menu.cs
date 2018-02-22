namespace Prospects.Models
{
    using GalaSoft.MvvmLight.Command;
    using Prospects.Services;
    using Prospects.ViewModels;
    using System;
    using System.Windows.Input;

    public class Menu
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Attributtes
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Constructors
        public Menu()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        void Navigate()
        {
            switch (PageName)
            {
                case "Exit":
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    navigationService.SetMainPage("LoginView");
                    break;
            }
        }
        #endregion
    }
}
