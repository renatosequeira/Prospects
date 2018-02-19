namespace Prospects.ViewModels.Companies
{
    using Prospects.Models.Companies;
    using System.Collections.ObjectModel;

    public class CompanyViewModel
    {
        #region Properties
        ObservableCollection<Company> Companies;
        #endregion

        #region Constructors
        public CompanyViewModel()
        {
            LoadCompanies();
        }

        private void LoadCompanies()
        {
            
        }
        #endregion
    }
}
