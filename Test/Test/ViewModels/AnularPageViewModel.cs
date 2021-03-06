using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.Helpers;
using Test.Models;
using Test.Services.Navigation;
using Test.Services.Sqlite.InterfaceServices;
using Test.ViewModels.Base;
using Test.Views;
using Xamarin.Forms;

namespace Test.ViewModels
{
    public class AnularPageViewModel : BaseViewModel
    {
        private Page _page;
        private readonly INavigationService _navigationService;
        private readonly ISqliteServiceCRUD<Suscripcion> _sqliteSuscripcionService;
        public ICommand SuscripcionCommand { get; set; }
        public ICommand AnularCommand { get; set; }
        public AnularPageViewModel(INavigationService navigationService, ISqliteServiceCRUD<Suscripcion> sqliteSuscripcionService)
        {
            this._navigationService = navigationService;
            this._sqliteSuscripcionService = sqliteSuscripcionService;
            this.AnularCommand = new Command(async () => await Anular());
        }
        private Suscripcion _suscripcionModel = new Suscripcion();
        public string Nombre
        {

            get { return _suscripcionModel.Nombre; }
            set
            {

                _suscripcionModel.Nombre = value;
                //ValidationHelper.IsFormValid(_suscripcionModel, _page);
                OnPropertyChanged();
            }
        }
        public string Apellido
        {

            get { return _suscripcionModel.Apellido; }
            set
            {

                _suscripcionModel.Apellido = value;
                //ValidationHelper.IsFormValid(_suscripcionModel, _page);
                OnPropertyChanged();
            }
        }
        public string Celular
        {
            get { return _suscripcionModel.Celular; }
            set
            {
                _suscripcionModel.Celular = value;
                //ValidationHelper.IsFormValid(_suscripcionModel, _page);
                OnPropertyChanged();
            }
        }
        public string Correo
        {
            get { return _suscripcionModel.Correo; }
            set
            {
                _suscripcionModel.Correo = value;
                //if (_suscripcionModel.Correo != "")
                //    ValidationHelper.IsFormValid(_suscripcionModel, _page);

                OnPropertyChanged();
            }
        }
        public string TipoSuscripcion
        {
            get { return _suscripcionModel.TipoSubscripcion; }
            set
            {
                _suscripcionModel.TipoSubscripcion = value;
                //if (_suscripcionModel.Correo != "")
                //    ValidationHelper.IsFormValid(_suscripcionModel, _page);
                OnPropertyChanged();
            }
        }
        public bool RecibirEmail
        {
            get { return _suscripcionModel.RecibirEmail; }
            set
            {
                _suscripcionModel.RecibirEmail = value;
                //    ValidationHelper.IsFormValid(_suscripcionModel, _page);
                OnPropertyChanged();
            }
        }

        private bool _basic;
        public bool Basic
        {
            get { return _basic; }
            set
            {
                _basic = value;
                OnPropertyChanged();
            }
        }

        private bool _premium;
        public bool Premium
        {
            get { return _premium; }
            set
            {
                _premium = value;
                OnPropertyChanged();
            }
        }

        private bool _gold;
        public bool Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
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

        private async Task Suscripcion(string Suscripcion)
        {
            if (_clickedButton)
                return;
            try
            {
                _clickedButton = true;

                switch (Suscripcion)
                {
                    case "Basic":
                        Basic = true;
                        Premium = false;
                        Gold = false;
                        TipoSuscripcion = 250.ToString("C");
                        break;
                    case "Premium":
                        Basic = false;
                        Premium = true;
                        Gold = false;
                        TipoSuscripcion = 500.ToString("C");
                        break;
                    case "Gold":
                        Basic = false;
                        Premium = false;
                        Gold = true;
                        TipoSuscripcion = 750.ToString("C");
                        break;
                    default:
                        break;
                }
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
        private async Task Anular()
        {
            if (_clickedButton)
                return;

            _clickedButton = true;
            try
            {
                bool Deleted = await _sqliteSuscripcionService.Remove(_suscripcionModel);
                if (Deleted)
                {
                    await Application.Current.MainPage.DisplayAlert("Suscrito", "Suscripcion anulada!", "OK");
                    _navigationService.SetMain(new MainPage(),true);
                }

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
        private async Task GetSubscriber()
        {
            try
            {
                var Subscriber = await _sqliteSuscripcionService.GetAll();
                int Sub = Subscriber.FirstOrDefault().Id;
                _suscripcionModel = await _sqliteSuscripcionService.GetSingle(Sub);
                if (_suscripcionModel == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Alerta!", "No se encontro la sucripcion", "OK");
                    return;
                }

                Nombre = _suscripcionModel.Nombre;
                Apellido = _suscripcionModel.Apellido;
                Celular = _suscripcionModel.Celular;
                Correo = _suscripcionModel.Correo;

                RecibirEmail = _suscripcionModel.RecibirEmail;
                switch (_suscripcionModel.TipoSubscripcion)
                {
                    case "Basic":
                        Basic = true;
                        Premium = false;
                        Gold = false;
                        TipoSuscripcion = 250.ToString("C");
                        break;
                    case "Premium":
                        Basic = false;
                        Premium = true;
                        Gold = false;
                        TipoSuscripcion = 500.ToString("C");
                        break;
                    case "Gold":
                        Basic = false;
                        Premium = false;
                        Gold = true;
                        TipoSuscripcion = 750.ToString("C");
                        break;
                    default:
                        break;
                }

            }
            catch (Exception E)
            {
                var uu = E.Message.ToString();
                string line = E.StackTrace.ToString();
                line = line.Substring(line.LastIndexOf(':') + 1);
                await Application.Current.MainPage.DisplayAlert("Alerta!", "Ocurrio algo Error(" + line + ")", "OK");
            }
        }
        public override async void OnAppearing(object navigationContext)
        {
            if (_clickedButton)
                return;

            _clickedButton = true;
            try
            {
                _page = this._navigationService.GetCurrentPage();
                await GetSubscriber();

                //// Current app version (1.0.0)
                //Version = $"V{ VersionTracking.CurrentVersion}";
                //await Task.Delay(TimeSpan.FromSeconds(1));

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
    }
}
