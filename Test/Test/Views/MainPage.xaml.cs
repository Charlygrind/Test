using Test.ViewModels;
using Xamarin.Forms;

namespace Test.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (BindingContext is MainPageViewModel viewModel) viewModel.OnAppearing(this.BindingContext);
        }
        protected override void OnDisappearing()
        {
            if (BindingContext is MainPageViewModel viewModel) viewModel.OnDisappearing();
        }
    }
}