using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnularPage : ContentPage
    {
        public AnularPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (BindingContext is AnularPageViewModel viewModel) viewModel.OnAppearing(this.BindingContext);
        }
        protected override void OnDisappearing()
        {
            if (BindingContext is AnularPageViewModel viewModel) viewModel.OnDisappearing();
        }
    }
}