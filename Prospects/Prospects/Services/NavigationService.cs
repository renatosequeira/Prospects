namespace Prospects.Services
{
    using Prospects.Views.Companies;
    using Prospects.Views.Contacts;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "CompanyView":
                    await Application.Current.MainPage.Navigation.PushAsync(new CompanyView());
                    break;

                case "ContactsView":
                    await Application.Current.MainPage.Navigation.PushAsync(new ContactsView());
                    break;

                case "NewCompanyView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewCompanyView());
                    break;

                case "EditCompanyView":
                    await Application.Current.MainPage.Navigation.PushAsync(new EditCompanyView());
                    break;

                case "NewContactView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewContactView());
                    break;
            }
        }

        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
