namespace Prospects.Models.Companies
{
    using GalaSoft.MvvmLight.Command;
    using Models.Contacts;
    using Services;
    using ViewModels;
    using ViewModels.Companies;
    using ViewModels.Contacts;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class Company
    {
        #region Services
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Properties
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanySector { get; set; }
        public string CompanyNIF { get; set; }
        public string CompanyLegalForm { get; set; }
        public string Capital { get; set; }
        public bool Status { get; set; }
        public string CompanyProspectlStatus { get; set; }
        public string CompanyNotes { get; set; }
        public string CompanyAddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string Image { get; set; }
        public List<Contact> Contacts { get; set; }

        public string ImageFullPath
        {
            get
            {
                return string.Format("http://prospects.outstandservices.pt/{0}",
                    Image.Substring(1));
            }
        }
        #endregion

        #region Constructors
        public Company()
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
            var response = await dialogService.ShowConfirm("Apagar empresa", "Tem a certeza que deseja apagar a empresa?");

            if (!response)
            {
                return;
            }

            CompanyViewModel.GetInstance().DeleteCompany(this);
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
            mainViewModel.EditCompany = new EditCompanyViewModel(this);

            await navigationService.Navigate("EditCompanyView");
        }

        public ICommand SelectCompanyCommand
        {
            get
            {
                return new RelayCommand(SelectCompany);
            }
        }

        async void SelectCompany()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Company = this; //mantem a categoria visivel, quando se selecciona o contacto
            mainViewModel.Contacts = new ContactsViewModel(Contacts);

            await navigationService.Navigate("ContactsView");
        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return CompanyId;
        }
        #endregion
    }
}
