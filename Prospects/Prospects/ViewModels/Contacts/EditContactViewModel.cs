namespace Prospects.ViewModels.Contacts
{
    using Prospects.Models.Contacts;
    using GalaSoft.MvvmLight.Command;
    using Models.Companies;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Prospects.Helpers;
    using Services;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditContactViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        bool _enableCompanyEdit;
        Contact contact;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public string ContactName { get; set; }

        public string ContactMobile { get; set; }

        public string ContactEmail { get; set; }

        public string ContactWebsite { get; set; }

        public string ContactCompany { get; set; }

        public string ContactPositionInCompany { get; set; }

        public bool EnableCompanyEdit
        {
            get
            {
                return _enableCompanyEdit;
            }
            set
            {
                if (_enableCompanyEdit != value)
                {
                    _enableCompanyEdit = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnableCompanyEdit)));
                }
            }
        }
        #endregion

        #region Constructors
        public EditContactViewModel(Contact contact)
        {
            this.contact = contact;

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            ContactName = contact.ContactName;
            ContactMobile = contact.ContactMobile;
            ContactEmail = contact.ContactEmail;
            ContactWebsite = contact.ContactWebsite;
            ContactCompany = contact.ContactCompany;
            ContactPositionInCompany = contact.ContactPositionInCompany;

            if (string.IsNullOrEmpty(ContactCompany))
            {
                //EnableCompanyEdit = true;
                ContactCompany = MainViewModel.GetInstance().Company.CompanyName;
            }

            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        async void Save()
        {
            if (string.IsNullOrEmpty(ContactName))
            {
                await dialogService.ShowMessage("Erro", "Por favor insira o nome do contacto!");
                return;
            }

            if (string.IsNullOrEmpty(ContactEmail))
            {
                await dialogService.ShowMessage("Erro", "Por favor insira o Email do contacto!");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            contact.ContactName = ContactName;
            contact.ContactMobile = ContactMobile;
            contact.ContactEmail = ContactEmail;
            contact.CompanyId = mainViewModel.Company.CompanyId;
            contact.ContactPositionInCompany = ContactPositionInCompany;

            var response = await apiService.Put(
                "http://api.prospects.outstandservices.pt",
                "/api",
                "/Contacts",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                contact);

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            var contactViewModel = ContactsViewModel.GetInstance();
            contactViewModel.UpdateContact(contact);

            await navigationService.BackOnMaster();

            IsEnabled = true;
            IsRunning = false;

        }
        #endregion
    }
}
