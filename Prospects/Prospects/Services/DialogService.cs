namespace Prospects.Services
{
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class DialogService
    {
        public async Task ShowMessage(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        public async Task<bool> ShowConfirm(string title, string message)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, "SIM", "NÃO");
        }

        public async Task<string> ShowImageOptions()
        {
            return await Application.Current.MainPage.DisplayActionSheet(
                "Seleccionar imagem de?",
                "Cancel",
                null,
                "Galeria",
                "From Camera");
        }

    }
}
