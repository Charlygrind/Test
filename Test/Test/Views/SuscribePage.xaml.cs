using Test.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuscribePage : ContentPage
    {
        public SuscribePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (BindingContext is SuscribePageViewModel viewModel) viewModel.OnAppearing(this.BindingContext);
        }
        protected override void OnDisappearing()
        {
            if (BindingContext is SuscribePageViewModel viewModel) viewModel.OnDisappearing();
        }
    }
}