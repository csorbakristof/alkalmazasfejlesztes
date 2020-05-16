using Autofac;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Services.NavigationService;
using TodoUWP.Template10.Data;
using TodoUWP.Template10;
using TodoUWP.Template10.ViewModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace TodoUWP.Template10
{

    sealed partial class App : BootStrapper
    {
        

        public App()
        {
            this.InitializeComponent();
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            ConfigureDependencies();


            await NavigationService.NavigateAsync(typeof(MainPage));
            //return Task.CompletedTask;
        }

        IContainer _container;
        private void ConfigureDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DbService>().As<IDbService>().SingleInstance();
            builder.RegisterType<TodoPageViewModel>();

            _container = builder.Build();
        }

        public override INavigable ResolveForPage(Page page, NavigationService navigationService)
        {
            if (page is MainPage)
            {
                return _container.Resolve<TodoPageViewModel>();
            }
            return base.ResolveForPage(page, navigationService);
        }
    }
}
