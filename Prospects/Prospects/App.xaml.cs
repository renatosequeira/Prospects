﻿namespace Prospects
{
    using Views;
    using Xamarin.Forms;

    public partial class App : Application
	{
        public static NavigationPage Navigator { get; internal set; }
        public static MasterView Master { get; internal set; }

        public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new LoginView());
            //MainPage = new MasterView();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
