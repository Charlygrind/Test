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
    public partial class EditSubPage : ContentPage
    {
        public EditSubPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (BindingContext is EditSubPageViewModel viewModel) viewModel.OnAppearing(this.BindingContext);
        }
        protected override void OnDisappearing()
        {
            if (BindingContext is EditSubPageViewModel viewModel) viewModel.OnDisappearing();
        }
    }
}