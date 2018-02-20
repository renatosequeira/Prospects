namespace Prospects.ViewModels.Contacts
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using Prospects.Models.Contacts;

    public class ContactsViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        private List<Contact> contacts;
        ObservableCollection<Contact> _contacts;
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
        #endregion

        #region Constructors
        public ContactsViewModel(List<Contact> contacts)
        {
            this.contacts = contacts;
            ContactsList = new ObservableCollection<Contact>(contacts.OrderBy(c => c.ContactName));
        } 
        #endregion
    }
}
