namespace Prospects.Models.Companies
{
    using GalaSoft.MvvmLight.Command;
    using Prospects.Models.Contacts;
    using Prospects.Services;
    using Prospects.ViewModels;
    using Prospects.ViewModels.Contacts;
    using Prospects.Views.Contacts;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class Company
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Properties
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public object CompanyAddress { get; set; }
        public object CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyWebsite { get; set; }
        public object CompanySector { get; set; }
        public string CompanyNIF { get; set; }
        public object CompanyLegalForm { get; set; }
        public object Capital { get; set; }
        public bool Status { get; set; }
        public object CompanyProspectlStatus { get; set; }
        public object CompanyNotes { get; set; }
        public object CompanyAddedBy { get; set; }
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
        }
        #endregion
        //public override string ToString()
        //{
        //    return CompanyName;
        //}

        #region Commands
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
            mainViewModel.Contacts = new ContactsViewModel(Contacts);

            await navigationService.Navigate("ContactsView");
        }
        #endregion
    }
}
