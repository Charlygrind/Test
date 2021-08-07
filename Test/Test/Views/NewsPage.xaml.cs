using Test.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        public NewsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (BindingContext is NewsPageViewModel viewModel) viewModel.OnAppearing(this.BindingContext);
        }
        protected override void OnDisappearing()
        {
            if (BindingContext is NewsPageViewModel viewModel) viewModel.OnDisappearing();
        }
    }
}