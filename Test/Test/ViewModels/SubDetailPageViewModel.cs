using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.Services.Navigation;
using Test.ViewModels.Base;
using Xamarin.Forms;

namespace Test.ViewModels
{
    public class SubDetailPageViewModel : BaseViewModel
    {
        private Page _page;
        private readonly INavigationService _navigationService;
        public ICommand BackCommand { get; set; }
        public SubDetailPageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
            this.BackCommand = new Command(async () => await ModalBack());

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
        private async Task ModalBack()
        {
            await _navigationService.PopModalAsync(true);
        }
        public override async void OnAppearing(object navigationContext)
        {
            if (_clickedButton)
                return;

            _clickedButton = true;
            try
            {
                _page = this._navigationService.GetCurrentPage();


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
