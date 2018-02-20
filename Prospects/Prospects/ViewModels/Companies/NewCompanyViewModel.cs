namespace Prospects.ViewModels.Companies
{
    using GalaSoft.MvvmLight.Command;
    using Prospects.Models.Companies;
    using Prospects.Services;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    public class NewCompanyViewModel : INotifyPropertyChanged
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

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPhone { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyWebsite { get; set; }

        public string CompanySector { get; set; }

        public string CompanyNIF { get; set; }
        #endregion

        #region Constructors
        public NewCompanyViewModel()
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
            if (string.IsNullOrEmpty(CompanyName))
            {
                await dialogService.ShowMessage("Erro", "Por favor insira o nome da empresa!");
                return;
            }

            if (string.IsNullOrEmpty(CompanyNIF))
            {
                await dialogService.ShowMessage("Erro", "Por favor insira o NIF da empresa!");
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

            var company = new Company
            {
                AddedDate = DateTime.Today,
                CompanyName = CompanyName,
                CompanyAddress = CompanyAddress,
                CompanyPhone = CompanyPhone,
                CompanyEmail = CompanyEmail,
                CompanyWebsite = CompanyWebsite,
                CompanySector = CompanySector,
                CompanyNIF = CompanyNIF,
                Image = "no_image"
            };

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.Post(
                "http://api.prospects.outstandservices.pt",
                "/api",
                "/Companies",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                company);

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            //passa nova empresa para o viewmodel anterior
            company = (Company)response.Result;
            var companyViewModel = CompanyViewModel.GetInstance();
            companyViewModel.AddCompany(company);

            await navigationService.Back();

            IsEnabled = true;
            IsRunning = false;

        }
        #endregion
    }
}
