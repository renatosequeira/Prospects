namespace Prospects.ViewModels.Companies
{
    using Prospects.Models.Companies;
    using Prospects.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

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
        #endregion

        #region Properties
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
            companies.Add(company);
            CompaniesList = new ObservableCollection<Company>(companies.OrderBy(c => c.CompanyName));
        }

        async void LoadCompanies()
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
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            companies = (List<Company>)response.Result;

            CompaniesList = new ObservableCollection<Company>(companies.OrderBy(c => c.CompanyName));
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
    }
}
