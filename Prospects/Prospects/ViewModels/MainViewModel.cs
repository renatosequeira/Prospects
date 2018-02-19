namespace Prospects.ViewModels
{
    using Prospects.Models;
    using Prospects.ViewModels.Companies;
    using System;

    public class MainViewModel
    {
        #region Properties
        public TokenResponse Token { get; set; }
        public LoginViewModel Login { get; set; }
        public CompanyViewModel Companies { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
