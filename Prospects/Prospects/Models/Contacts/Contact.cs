namespace Prospects.Models.Contacts
{
    using GalaSoft.MvvmLight.Command;
    using Prospects.Services;
    using Prospects.ViewModels;
    using Prospects.ViewModels.Contacts;
    using System;
    using System.Windows.Input;

    public class Contact
    {
        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Properties
        public int ContactId { get; set; }
        public int CompanyId { get; set; }
        public string ContactName { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmail { get; set; }
        public string ContactWebsite { get; set; }
        public string ContactCompany { get; set; }
        public DateTime AddedDate { get; set; }
        public bool Contacted { get; set; }
        public string ContactAddedBy { get; set; }
        public string ContactPositionInCompany { get; set; }
        #endregion

        #region Constructors
        public Contact()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
        }
        #endregion

        #region Commands
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }

        async void Delete()
        {
            var response = await dialogService.ShowConfirm("Apagar contacto", "Tem a certeza que deseja apagar este contacto?");

            if (!response)
            {
                return;
            }

            await ContactsViewModel.GetInstance().DeleteContact(this);
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }

        async void Edit()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditContact = new EditContactViewModel(this);

            await navigationService.NavigateOnMaster("EditContactView");
        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return ContactId;
        }
        #endregion
    }
}
