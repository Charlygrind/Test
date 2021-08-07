using System;
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
    public class SuscribePageViewModel : BaseViewModel
    {
        private Page _page;
        private readonly INavigationService _navigationService;
        private readonly ISqliteServiceCRUD<Suscripcion> _sqliteSuscripcionService;
        public ICommand SuscripcionCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public SuscribePageViewModel(INavigationService navigationService, ISqliteServiceCRUD<Suscripcion> sqliteSuscripcionService)
        {
            this._navigationService = navigationService;
            this._sqliteSuscripcionService = sqliteSuscripcionService;
            this.SuscripcionCommand = new Command<string>(async (Param) => await Suscripcion(Param));
            this.SaveCommand = new Command(async () => await Save());
        }
        private Suscripcion _subscripcionModel = new Suscripcion();
        public string Nombre
        {

            get { return _subscripcionModel.Nombre; }
            set
            {

                _subscripcionModel.Nombre = value;
                //ValidationHelper.IsFormValid(_subscripcionModel, _page);
                OnPropertyChanged();
            }
        }
        public string Apellido
        {

            get { return _subscripcionModel.Apellido; }
            set
            {

                _subscripcionModel.Apellido = value;
                //ValidationHelper.IsFormValid(_subscripcionModel, _page);
                OnPropertyChanged();
            }
        }
        public string Celular
        {
            get { return _subscripcionModel.Celular; }
            set
            {
                _subscripcionModel.Celular = value;
                //ValidationHelper.IsFormValid(_subscripcionModel, _page);
                OnPropertyChanged();
            }
        }
        public string Correo
        {
            get { return _subscripcionModel.Correo; }
            set
            {
                _subscripcionModel.Correo = value;
                //if (_subscripcionModel.Correo != "")
                //    ValidationHelper.IsFormValid(_subscripcionModel, _page);

                OnPropertyChanged();
            }
        }
        public string TipoSuscripcion
        {
            get { return _subscripcionModel.TipoSubscripcion; }
            set
            {
                _subscripcionModel.TipoSubscripcion = value;
                //if (_subscripcionModel.Correo != "")
                //    ValidationHelper.IsFormValid(_subscripcionModel, _page);
                OnPropertyChanged();
            }
        }
        public bool RecibirEmail
        {
            get { return _subscripcionModel.RecibirEmail; }
            set
            {
                _subscripcionModel.RecibirEmail = value;
                //    ValidationHelper.IsFormValid(_subscripcionModel, _page);
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
            } catch (Exception E)
            {
                var uu = E.Message.ToString();
                string line = E.StackTrace.ToString();
                line = line.Substring(line.LastIndexOf(':') + 1);
                await Application.Current.MainPage.DisplayAlert("Alerta!", "Ocurrio algo Error(" + line + ")", "OK");
            }
             _clickedButton = false;
        }
        private async Task Save()
        {
            if (_clickedButton)
                return;

            _clickedButton = true;
            try
            {
                if (!ValidationHelper.IsFormValid(_subscripcionModel, _page))
                {
                    await Application.Current.MainPage.DisplayAlert("Alerta", "Verifique los campos requeridos!", "OK");
                    _clickedButton = false;
                    return;
                }

                TipoSuscripcion = Basic ? "Basic" : Premium ? "Premium" : Gold ? "Gold" : "";
                if (TipoSuscripcion == "")
                {
                    await Application.Current.MainPage.DisplayAlert("Alerta", "Seleccione tipo de suscripcion!", "OK");
                    _clickedButton = false;
                    return;
                }

                _subscripcionModel.Correo = Correo == null ? "" : Correo;
                _subscripcionModel.UsuarioInsert = "1";
                _subscripcionModel.FechaInsert = DateTime.Now;

                bool Saved = await _sqliteSuscripcionService.Insert(_subscripcionModel);
                if (Saved)
                {
                    await Application.Current.MainPage.DisplayAlert("Suscrito", "Suscripcion Enviada!", "OK");
                    _navigationService.SetMain(new NewsPage(), true);
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
        public override async void OnAppearing(object navigationContext)
        {
            if (_clickedButton)
                return;

            _clickedButton = true;
            try
            {
                _page = this._navigationService.GetCurrentPage();
                var QuizName = _page.GetType().Name;

                Basic = true;
                Premium = false;
                Gold = false;
                TipoSuscripcion = 250.ToString("C");

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