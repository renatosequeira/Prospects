namespace Prospects.ViewModels.Contacts
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Prospects.Models.Companies;
    using Prospects.Models.Contacts;
    using Prospects.Services;

    public class ContactsViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Attributes
        private List<Contact> contacts;
        ObservableCollection<Contact> _contacts;
        bool _isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<Contact> ContactsList
        {
            get
            {
                return _contacts;
            }
            set
            {
                if (_contacts != value)
                {
                    _contacts = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContactsList)));
                }
            }
        }

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
        #endregion

        #region Constructors
        public ContactsViewModel(List<Contact> contacts)
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();

            this.contacts = contacts;
            ContactsList = new ObservableCollection<Contact>(contacts.OrderBy(c => c.ContactName));
        }
        #endregion

        #region Sigleton
        static ContactsViewModel instance;

        public static ContactsViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        #region Methods
        public void AddContact(Contact contact)
        {
            IsRefreshing = true;
            contacts.Add(contact);
            ContactsList = new ObservableCollection<Contact>(contacts.OrderBy(c => c.ContactName));
            IsRefreshing = false;
        }

        public void UpdateContact(Contact contact)
        {
            IsRefreshing = true;
            var oldContact = contacts.Where(c => c.ContactId == contact.ContactId).FirstOrDefault();

            oldContact = contact;

            ContactsList = new ObservableCollection<Contact>(contacts.OrderBy(c => c.ContactName));
            IsRefreshing = false;
        }

        public async Task DeleteContact(Contact contact)
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
                "/Contacts",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                contact);

            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            contacts.Remove(contact);

            ContactsList = new ObservableCollection<Contact>(contacts.OrderBy(c => c.ContactName));
            IsRefreshing = false;
        }

        async void LoadContacts()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Contact>(
              "http://api.prospects.outstandservices.pt",
              "/api",
              "/Contacts",
              mainViewModel.Token.TokenType,
              mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            contacts = (List<Contact>)response.Result;

            ContactsList = new ObservableCollection<Contact>(contacts.OrderBy(c => c.ContactName));

            IsRefreshing = false;
        }
        #endregion

       
    }
}
