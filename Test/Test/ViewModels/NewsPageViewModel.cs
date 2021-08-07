using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.Models;
using Test.Services.ClientHTTP;
using Test.Services.Navigation;
using Test.Services.Sqlite.InterfaceServices;
using Test.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps.Bindings;

namespace Test.ViewModels
{
    public class NewsPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IServiceBaseHTTP<News> _newsServiceBaseHTTP;
        private readonly ISqliteServiceCRUD<Suscripcion> _sqliteSuscripcionService;
        public ICommand RefreshCommand { get; set; }
        public ICommand NavigateCommand { get; set; }
        public NewsPageViewModel(INavigationService navigationService, ISqliteServiceCRUD<Suscripcion> sqliteSuscripcionService, IServiceBaseHTTP<News> newsServiceBaseHTTP)
        {
            this._newsServiceBaseHTTP = newsServiceBaseHTTP;
            this._sqliteSuscripcionService = sqliteSuscripcionService;
            this._navigationService = navigationService;
            this.RefreshCommand = new Command(async () => await Refresh());
            this.NavigateCommand = new Command<string>(async (Param) => await Navigate(Param));
        }
        private string _nombre;
        public string Nombre
        {

            get { return _nombre; }
            set
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }
        private string _tipoSuscripcion;
        public string TipoSuscripcion
        {
            get { return _tipoSuscripcion; }
            set
            {
                _tipoSuscripcion = value;
                OnPropertyChanged();
            }
        }
        private string _latitude;
        public string Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }
        private string _longitude;
        public string Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }
        public MoveToRegionRequest Request { get; } = new MoveToRegionRequest();
        public ObservableCollection<Xamarin.Forms.GoogleMaps.Pin> Pins { get; set; }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                GetNews();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<News> _newsList;
        public ObservableCollection<News> NewsList
        {
            get { return _newsList; }
            set
            {
                _newsList = value;
                OnPropertyChanged();
            }
        }
        private int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
                if (TipoSuscripcion == "" || TipoSuscripcion == null)
                    return;

                GetNews();
            }
        }
        private bool _refreshing;
        public bool Refreshing
        {
            get { return _refreshing; }
            set
            {
                _refreshing = value;
                OnPropertyChanged();
            }
        }
        private bool _clickedButton = false;
        public bool clickedButton
        {
            get { return _clickedButton; }
            set
            {
                _clickedButton = value;
                OnPropertyChanged();
            }
        }
        private async Task CurrentLocation()
        {
            try
            {
                GeolocationRequest request = new GeolocationRequest(
                   GeolocationAccuracy.Best
                );
                var location = await Geolocation.GetLocationAsync(request);
                if (location != null)
                {
                    Latitude = location.Latitude.ToString();
                    Longitude = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException E)
            {
                var uu = E.Message.ToString();
                string line = E.StackTrace.ToString();
                line = line.Substring(line.LastIndexOf(':') + 1);
                await Application.Current.MainPage.DisplayAlert("Alerta!", "Ocurrio algo Error(" + line + ")", "OK");
            }
            catch (PermissionException E)
            {
                var uu = E.Message.ToString();
                string line = E.StackTrace.ToString();
                line = line.Substring(line.LastIndexOf(':') + 1);
                await Application.Current.MainPage.DisplayAlert("Alerta!", "Ocurrio algo Error(" + line + ")", "OK");
            }
            catch (Exception E)
            {
                var uu = E.Message.ToString();
                string line = E.StackTrace.ToString();
                line = line.Substring(line.LastIndexOf(':') + 1);
                await Application.Current.MainPage.DisplayAlert("Alerta!", "Ocurrio algo Error(" + line + ")", "OK");
            }
        }
        private async Task MoveToRegion(string Lat,string Long,bool Animated)
        {
            try
            {
                double Latitude = Convert.ToDouble(Lat),Longitude = Convert.ToDouble(Long);

                Request.MoveToRegion(
                Xamarin.Forms.GoogleMaps.MapSpan.FromCenterAndRadius(
                    new Xamarin.Forms.GoogleMaps.Position(Latitude, Longitude),
                    Xamarin.Forms.GoogleMaps.Distance.FromKilometers(2)),
                Animated);
            }
            catch (Exception E)
            {
                var uu = E.Message.ToString();
                string line = E.StackTrace.ToString();
                line = line.Substring(line.LastIndexOf(':') + 1);
                await Application.Current.MainPage.DisplayAlert("Alerta!", "Ocurrio algo Error(" + line + ")", "OK");
            }
        }
        private async Task PinMap(string Lat, string Long)
        {
            Pins.Clear();
            // MAPS KEY AIzaSyB5q2c2jDyws2AipkcO1Ru0CVFQFz5reCA

            int pinNumber = 1;
            Xamarin.Forms.GoogleMaps.Position Pos = new Xamarin.Forms.GoogleMaps.Position(Convert.ToDouble(Lat), Convert.ToDouble(Long));
            Pins.Add(new Xamarin.Forms.GoogleMaps.Pin
            {
                Label = $"Pin{pinNumber}",
                Position = Pos
            });
            
            await Task.Delay(3);
        }
        private async Task GetNews()
        {
            if (_clickedButton)
                return;
            try
            {
                _clickedButton = true;
                ResultList<News> News =  await _newsServiceBaseHTTP.GetList(TipoSuscripcion, SelectedCategory == null ? "top" : SelectedCategory.Descripcion, _currentPage);
                if (!News.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Alerta!", "No hay noticias para mostrar", "OK");
                    _clickedButton = false;
                    Refreshing = false;
                    return;
                }

                if (News.Data.Count == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Alerta!", "No hay noticias para mostrar", "OK");
                    _clickedButton = false;
                    Refreshing = false;
                    return;
                }

                NewsList = new ObservableCollection<News>();
                foreach (var _New in News.Data)
                {
                    News temp = new News()
                    {
                        title = _New.title ?? "",
                        link = _New.link ?? "",
                        description = _New.description ?? "",
                        content = _New.content ?? "",
                        pubDate = _New.pubDate ?? "",
                        image_url = _New.image_url ?? "https://previews.123rf.com/images/pe3check/pe3check1710/pe3check171000054/88673746-nenhuma-imagem-dispon%C3%ADvel-sinal-%C3%ADcone-da-web-da-internet-para-indicar-a-aus%C3%AAncia-de-imagem-at%C3%A9-que-e.jpg",
                        source_id = _New.source_id ?? ""
                    };
                    NewsList.Add(temp);
                }
                Refreshing = false;
            }
            catch (Exception E)
            {
                var uu = E.Message.ToString();
                string line = E.StackTrace.ToString();
                line = line.Substring(line.LastIndexOf(':') + 1);
                await Application.Current.MainPage.DisplayAlert("Alerta!", "Ocurrio algo Error(" + line + ")", "OK");
            }
            _clickedButton = false;
        }
        private async Task Refresh()
        {
            Refreshing = true;
            CurrentPage = 0;
            
        }
        private async Task Navigate(string Param)
        {
            if (_clickedButton)
                return;

            _clickedButton = true;

            await _navigationService.PushAsync(_navigationService.GetPageByName(Param), true);
            _clickedButton = !_clickedButton;
        }
        public override async void OnAppearing(object navigationContext)
        {
            try
            {
                await CurrentLocation();
                await MoveToRegion(Latitude,Longitude,true);
                await PinMap(Latitude, Longitude);


                Categories = new ObservableCollection<Category>();

                Categories.Add(new Category() { Id =1,Descripcion= "top" });
                Categories.Add(new Category() { Id =1,Descripcion= "technology" });
                Categories.Add(new Category() { Id =1,Descripcion= "science" });
                Categories.Add(new Category() { Id =1,Descripcion= "entertainment" });

                Suscripcion User =  await _sqliteSuscripcionService.FirstOrDefault();
                TipoSuscripcion = User.TipoSubscripcion;
                Nombre = User.Nombre;
                await GetNews();
                Refreshing = false;
                //// Current app version (2.0.0)
                //Version = $"V{ VersionTracking.CurrentVersion}";

            }
            catch (Exception E)
            {
                var uu = E.Message.ToString();
                string line = E.StackTrace.ToString();
                line = line.Substring(line.LastIndexOf(':') + 1);
                await Application.Current.MainPage.DisplayAlert("Alerta!", "Ocurrio algo Error(" + line + ")", "OK");
            }
        }
    }
}