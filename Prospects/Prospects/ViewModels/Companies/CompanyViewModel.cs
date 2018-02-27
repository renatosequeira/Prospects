namespace Prospects.ViewModels.Companies
{
    using GalaSoft.MvvmLight.Command;
    using Prospects.Models.Companies;
    using Prospects.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms.Maps;

    public class CompanyViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Attributes
        List<Company> companies;
        ObservableCollection<Company> _companies;
        bool _isRefreshing;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }

        public ObservableCollection<Company> CompaniesList
        {
            get
            {
                return _companies;
            }
            set
            {
                if(_companies != value)
                {
                    _companies = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CompaniesList)));
                }
            }
        }

        public ObservableCollection<Pin> Pins { get; set; }
        #endregion

        #region Constructors
        public CompanyViewModel()
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();

            LoadCompanies();
        }
        #endregion

        #region Methods
        public void AddCompany(Company company)
        {
            IsRefreshing = true;
            companies.Add(company);
            CompaniesList = new ObservableCollection<Company>(companies.OrderBy(c => c.CompanyName));
            IsRefreshing = false;
        }

        public void UpdateCompany(Company company)
        {
            IsRefreshing = true;
            var oldCompany = companies.Where(c => c.CompanyId == company.CompanyId).FirstOrDefault();

            oldCompany = company;

            CompaniesList = new ObservableCollection<Company>(companies.OrderBy(c => c.CompanyName));
            IsRefreshing = false;
        }

        public async Task DeleteCompany(Company company)
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.Delete(
                "http://api.prospects.outstandservices.pt",
                "/api",
                "/Companies",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                company);

            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            companies.Remove(company);

            CompaniesList = new ObservableCollection<Company>(companies.OrderBy(c => c.CompanyName));
            IsRefreshing = false;
        }

        async void LoadCompanies()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Company>(
              "http://api.prospects.outstandservices.pt",
              "/api",
              "/Companies",
              mainViewModel.Token.TokenType,
              mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            companies = (List<Company>)response.Result;

            CompaniesList = new ObservableCollection<Company>(companies.OrderBy(c => c.CompanyName));

            IsRefreshing = false;
        }

        public async Task LoadPins()
        {
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Company>(
              "http://api.prospects.outstandservices.pt",
              "/api",
              "/Companies",
              mainViewModel.Token.TokenType,
              mainViewModel.Token.AccessToken);


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var locations = (List<Company>)response.Result;
            Pins = new ObservableCollection<Pin>();

            try
            {
                foreach (var location in locations.Where(p => p.Latitude != 0))
                {
                    Pins.Add(new Pin
                    {
                        Address = location.CompanyAddress,
                        Label = location.CompanyName,
                        Position = new Position(location.Latitude, location.Longitude),
                        Type = PinType.Generic
                    });
                }
            }
            catch (Exception ex)
            {

                await dialogService.ShowMessage("Error", ex.Message);
            }
        }
        #endregion

        #region Singleton
        static CompanyViewModel instance;

        public static CompanyViewModel GetInstance()
        {
            if (instance == null)
            {
                return new CompanyViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadCompanies);
            }
        }

        #endregion
    }
}
