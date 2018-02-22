namespace Prospects.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Prospects.Models;
    using Prospects.Models.Companies;
    using Prospects.Services;
    using Prospects.ViewModels.Companies;
    using Prospects.ViewModels.Contacts;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class MainViewModel
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Properties
        public TokenResponse Token { get; set; }
        public LoginViewModel Login { get; set; }
        public CompanyViewModel Companies { get; set; }
        public ContactsViewModel Contacts { get; set; }
        public NewCompanyViewModel NewCompany { get; set; }
        public EditCompanyViewModel EditCompany { get; set; }
        public Company Company { get; set; } //para não perder de vista a empresa, quando se seleccionam os contactos
        public NewContactViewModel NewContact { get; set; }
        public EditContactViewModel EditContact { get; set; }

        public ObservableCollection<Menu> MyMenu { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
            navigationService = new NavigationService();

            LoadMenu();
        }
        #endregion

        #region Singleton
        static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand CompanyListCommand
        {
            get
            {
                return new RelayCommand(GoCompanyList);
            }
        }

        async void GoCompanyList()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Companies = new CompanyViewModel();

            await navigationService.NavigateOnMaster("CompanyView");
        }

        public ICommand NewContactCommand
        {
            get
            {
                return new RelayCommand(GoNewContact);
            }
        }

        async void GoNewContact()
        {
            NewContact = new NewContactViewModel();
            await navigationService.NavigateOnMaster("NewContactView");
        }

        public ICommand NewCompanyCommand
        {
            get
            {
                return new RelayCommand(GoNewCompany);
            }
        }

        async void GoNewCompany()
        {
            NewCompany = new NewCompanyViewModel();
            await navigationService.NavigateOnMaster("NewCompanyView");
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            MyMenu = new ObservableCollection<Menu>();

            MyMenu.Add(new Menu
            {
                Icon = "ic_settings_50",
                PageName = "MyProfile",
                Title = "Meu Perfil"
            });

            MyMenu.Add(new Menu
            {
                Icon = "ic_maps_50",
                PageName = "Locations",
                Title = "Localizações"
            });

            MyMenu.Add(new Menu
            {
                Icon = "ic_exit_50",
                PageName = "Exit",
                Title = "Sair"
            });

        }
        #endregion
    }
}
