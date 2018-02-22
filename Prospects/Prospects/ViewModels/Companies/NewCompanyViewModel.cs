namespace Prospects.ViewModels.Companies
{
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Prospects.Helpers;
    using Prospects.Models.Companies;
    using Prospects.Services;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class NewCompanyViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        ImageSource _imageSource;
        MediaFile file;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPhone { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyWebsite { get; set; }

        public string CompanySector { get; set; }

        public string CompanyNIF { get; set; }

        public string Image { get; set; }

        public DateTime AddedDate { get; set; }

        public ImageSource ImageSource
        {
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ImageSource)));
                }
            }
            get
            {
                return _imageSource;
            }
        }

        public string LabelDeImagem { get; set; }
        #endregion

        #region Constructors
        public NewCompanyViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;

            ImageSource = "no_image";
            LabelDeImagem = "Clique para adicionar imagem";
        }
        #endregion

        #region Commands
        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable &&
                CrossMedia.Current.IsTakePhotoSupported)
            {
                var source = await dialogService.ShowImageOptions();

                if (source == "Cancel")
                {
                    file = null;
                    return;
                }

                if (source == "From Camera")
                {
                    DateTime data = DateTime.Now;
                    TimeSpan hora = data.TimeOfDay;

                    file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Prospects",
                            Name = String.Format("Parents_{0:dd/MM/yyyy}_{1}.jpg", data, data.TimeOfDay),
                            PhotoSize = PhotoSize.Small,
                            SaveToAlbum = true
                        }
                    );
                }
                else
                {
                    file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                    });
                }
            }
            else
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
            }

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                LabelDeImagem = "Clique para modificar imagem";
            }
        }


        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        async void Save()
        {
            if (string.IsNullOrEmpty(CompanyName))
            {
                await dialogService.ShowMessage("Erro", "Por favor insira o nome da empresa!");
                return;
            }

            if (string.IsNullOrEmpty(CompanyNIF))
            {
                await dialogService.ShowMessage("Erro", "Por favor insira o NIF da empresa!");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            byte[] imageArray = null;

            try
            {
                if (file != null)
                {
                    imageArray = FilesHelper.ReadFully(file.GetStream());
                    file.Dispose();
                }
            }
            catch (Exception)
            {

                throw;
            }

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }


            var mainViewModel = MainViewModel.GetInstance();


            var company = new Company
            {
                AddedDate = DateTime.Today,
                CompanyName = CompanyName,
                CompanyAddress = CompanyAddress,
                CompanyPhone = CompanyPhone,
                CompanyEmail = CompanyEmail,
                CompanyWebsite = CompanyWebsite,
                CompanySector = CompanySector,
                CompanyNIF = CompanyNIF,
                ImageArray = imageArray
                
            };

            var response = await apiService.Post(
                "http://api.prospects.outstandservices.pt",
                "/api",
                "/Companies",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                company);

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            //passa nova empresa para o viewmodel anterior
            company = (Company)response.Result;
            var companyViewModel = CompanyViewModel.GetInstance();
            companyViewModel.AddCompany(company);

            await navigationService.BackOnMaster();

            IsEnabled = true;
            IsRunning = false;

        }
        #endregion
    }
}
