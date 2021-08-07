using Autofac;
using Test.Models;
using Test.Services.ClientHTTP;
//using Test.Models;
//using Test.Services.ClientHTTP;
using Test.Services.Navigation;
using Test.Services.Sqlite;
using Test.Services.Sqlite.InterfaceServices;
//using Test.Services.Packages;
//using Test.Services.Sqlite;
//using Test.Services.Sqlite.InterfaceServices;
//using Test.Services.Toast;


namespace Test.ViewModels.Base
{
    public class LocatorViewModel
    {
        private static IContainer _container;

        public LocatorViewModel()
        {
            //var _toastService = DependencyService.Get<IToastService>();
            var builder = new ContainerBuilder();

            // ViewModels
            builder.RegisterType<MainPageViewModel>();
            builder.RegisterType<SuscribePageViewModel>();
            builder.RegisterType<NewsPageViewModel>();
            builder.RegisterType<EditSubPageViewModel>();
            builder.RegisterType<AnularPageViewModel>();
            
            // Services    
            // Navigation
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

            // SQLite
            builder.RegisterType<SqliteService>().As<ISqliteService>();
            builder.RegisterType<SqliteSuscripcionService>().As<ISqliteServiceCRUD<Suscripcion>>();

            // HTTPClient
            builder.RegisterType<NewsClientHTTP>().As<IServiceBaseHTTP<News>>();

            //if (Device.RuntimePlatform == Device.Android)
            //builder.RegisterInstance<IToastService>(_toastService);

            if (_container != null)
            {
                _container.Dispose();
            }

            _container = builder.Build();
        }
        public MainPageViewModel MainPageViewModel
        {
            get { return _container.Resolve<MainPageViewModel>(); }
        }
        public SuscribePageViewModel SuscribePageViewModel
        {
            get { return _container.Resolve<SuscribePageViewModel>(); }
        }
        public NewsPageViewModel NewsPageViewModel
        {
            get { return _container.Resolve<NewsPageViewModel>(); }
        }
        public EditSubPageViewModel EditSubPageViewModel
        {
            get { return _container.Resolve<EditSubPageViewModel>(); }
        }
        public AnularPageViewModel AnularPageViewModel
        {
            get { return _container.Resolve<AnularPageViewModel>(); }
        }
    }
}