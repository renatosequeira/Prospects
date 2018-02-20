﻿namespace Prospects.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Prospects.Models;
    using Prospects.Services;
    using Prospects.ViewModels.Companies;
    using Prospects.ViewModels.Contacts;
    using System;
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
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
            navigationService = new NavigationService();
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
            await navigationService.Navigate("NewCompanyView");
        }
        #endregion
    }
}
