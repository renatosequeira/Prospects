﻿namespace Prospects.ViewModels.Contacts
{
    using GalaSoft.MvvmLight.Command;
    using Models.Companies;
    using Models.Contacts;
    using Services;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    public class NewContactViewModel : INotifyPropertyChanged
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

        public string AddedDate { get; set; }

        public string ContactPositionInCompany { get; set; }

        #endregion

        #region Constructors
        public NewContactViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

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

            var mainViewModel = MainViewModel.GetInstance();

            var contact = new Contact
            {
                AddedDate = DateTime.Today,
                ContactCompany = mainViewModel.Company.CompanyName,
                CompanyId = mainViewModel.Company.CompanyId,
                ContactName = ContactName,
                ContactMobile = ContactMobile,
                ContactEmail = ContactEmail,
                ContactWebsite = ContactWebsite,
                ContactPositionInCompany = ContactPositionInCompany
            };

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var response = await apiService.Post(
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

            contact = (Contact)response.Result;

            var contactsViewModel = ContactsViewModel.GetInstance();
            contactsViewModel.AddContact(contact);

            await navigationService.BackOnMaster();

            IsEnabled = true;
            IsRunning = false;

        }
        #endregion
    }
}
