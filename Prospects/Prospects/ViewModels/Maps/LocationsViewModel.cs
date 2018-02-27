namespace Prospects.ViewModels.Maps
{
    using Services;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Xamarin.Forms.Maps;
    using System.Collections.Generic;
    using System;
    using Prospects.Models.Companies;
    using System.Linq;

    public class LocationsViewModel
    {
        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Properties
        public ObservableCollection<Pin> Pins { get; set; }
        #endregion

        #region Constructors
        public LocationsViewModel()
        {
            instance = this;
            apiService = new ApiService();
            dialogService = new DialogService();
        }
        #endregion

        #region Singleton
        static LocationsViewModel instance;

        public static LocationsViewModel GetInstance()
        {
            if (instance == null)
            {
                return new LocationsViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
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
        #endregion
    }
}
