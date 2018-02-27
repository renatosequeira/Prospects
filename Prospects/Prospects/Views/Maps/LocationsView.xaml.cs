namespace Prospects.Views.Maps
{
    using System;
    using System.Threading.Tasks;
    using Prospects.ViewModels.Companies;
    using Services;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

	public partial class LocationsView : ContentPage
	{
        #region Services
        GeolocatorService geolocatorService;
        #endregion


        #region Constructors
        public LocationsView()
        {
            InitializeComponent();

            geolocatorService = new GeolocatorService();

            MoveMapToCurrentPosition();
        }
        #endregion

        #region Methods
        async void MoveMapToCurrentPosition()
        {
            await geolocatorService.GetLocation();

            if (geolocatorService.Latitude != 0 ||
                geolocatorService.Longitude != 0)
            {
                var position = new Position(
                    geolocatorService.Latitude,
                    geolocatorService.Longitude);

                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                    position,
                    Distance.FromKilometers(.5)));
            }

            await LoadPins1();
        }

        private async Task LoadPins1()
        {
            var companyViewModel = CompanyViewModel.GetInstance();

            await companyViewModel.LoadPins();

            foreach (var pin in companyViewModel.Pins)
            {
                MyMap.Pins.Add(pin);
            }
        }

        #endregion
    }
}