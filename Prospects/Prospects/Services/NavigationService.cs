namespace Prospects.Services
{
    using Prospects.Views;
    using Prospects.Views.Companies;
    using Prospects.Views.Contacts;
    using Prospects.Views.Maps;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "LoginView":
                    Application.Current.MainPage = new NavigationPage(new LoginView());
                    break;
                case "MasterView":
                    Application.Current.MainPage = new MasterView();
                    break;
            }
        }

        public async Task NavigateOnMaster(string pageName)
        {
            App.Master.IsPresented = false; //esconde o menu quando uma opção é seleccionada

            switch (pageName)
            {
                case "CompanyView":
                    await App.Navigator.PushAsync(new CompanyView());
                    break;

                case "ContactsView":
                    await App.Navigator.PushAsync(new ContactsView());
                    break;

                case "NewCompanyView":
                    await App.Navigator.PushAsync(new NewCompanyView());
                    break;

                case "EditCompanyView":
                    await App.Navigator.PushAsync(new EditCompanyView());
                    break;

                case "NewContactView":
                    await App.Navigator.PushAsync(new NewContactView());
                    break;

                case "EditContactView":
                    await App.Navigator.PushAsync(new EditContactView());
                    break;

                case "HomePage":
                    await App.Navigator.PushAsync(new HomePage());
                    break;

                case "LocationsView":
                    await App.Navigator.PushAsync(new LocationsView());
                    break;
            }
        }

        public async Task NavigateOnLogin(string pageName) //páginas para onde se navega, no formulario de login
        {
            switch (pageName)
            {
                //case "CompanyView":
                //    await Application.Current.MainPage.Navigation.PushAsync(new CompanyView());
                //    break;
            }
        }

        public async Task BackOnMaster()
        {
            await App.Navigator.PopAsync();
        }

        public async Task BackOnLogin()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
