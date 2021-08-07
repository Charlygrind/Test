using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.Services.Navigation;
using Test.Services.Sqlite.InterfaceServices;
using Test.ViewModels.Base;
using Test.Views;
using Xamarin.Forms;

namespace Test.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ISqliteService _sqliteService;
        public ICommand SuscribeCommand { get;set;}
        public MainPageViewModel(INavigationService navigationService, ISqliteService sqliteService)
        {
            this._navigationService = navigationService;
            this._sqliteService = sqliteService;
            this.SuscribeCommand = new Command(async () => await Suscribe());
        }

        private async Task Suscribe()
        {
            await _navigationService.PushAsync(new SuscribePage(),true);
        }
        private async Task PermissionsPrompt()
        {
            try
            {
                var request = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                request = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
                if (request != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    await Application.Current.MainPage.DisplayAlert("Ubicacion requeria", "La aplicacion no funcionara sin permisos de ubicacion", "OK");
                    await PermissionsPrompt();
                    //System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                var request2 = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
            }
            catch (Exception ex)
            {

                var tu = ex.Message.ToString();
            }
        }
        public override async void OnAppearing(object navigationContext)
        {
            try
            {
                bool dbExist = await _sqliteService.DBExist();
                if (!dbExist)
                {
                    //Creamos base de datos local
                    await _sqliteService.CreateDatabaseAsync();
                }
                else
                {
                    dbExist = await _sqliteService.DBClear();
                    if (!dbExist)
                        await _sqliteService.CreateDatabaseAsync();
                    //// Current app version (2.0.0)
                    //Version = $"V{ VersionTracking.CurrentVersion}";
                }

                //Pedimos permisos
                if (Device.RuntimePlatform == Device.Android)
                    await PermissionsPrompt();
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