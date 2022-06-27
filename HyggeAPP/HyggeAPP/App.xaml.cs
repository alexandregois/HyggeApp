using HyggeAPP.ViewModels;
using HyggeAPP.Views;
using Prism;
using Prism.Ioc;
using SharedTools.Models.WebPortal_API;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace HyggeAPP
{
    public partial class App
    {
        public UsuarioModel Usuario { get; set; }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Usuario = new UsuarioModel();

            if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/MainPage");
            else
                await NavigationService.NavigateAsync("NavigationPage/ViewMenuPrincipal");

            //await NavigationService.NavigateAsync("NavigationPage/ViewCadastroEndereco");

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ViewCadastro, ViewCadastroViewModel>();
            containerRegistry.RegisterForNavigation<ViewCadastroEndereco, ViewCadastroEnderecoViewModel>();
            containerRegistry.RegisterForNavigation<ViewCadastroVeiculo, ViewCadastroVeiculoViewModel>();
            containerRegistry.RegisterForNavigation<ViewCadastroCartao, ViewCadastroCartaoViewModel>();
            containerRegistry.RegisterForNavigation<ViewMenuPrincipal, ViewMenuPrincipalViewModel>();
            containerRegistry.RegisterForNavigation<ViewListaEnderecos, ViewListaEnderecosViewModel>();
            containerRegistry.RegisterForNavigation<ViewListaVeiculos, ViewListaVeiculosViewModel>();
            containerRegistry.RegisterForNavigation<ViewListaCartoes, ViewListaCartoesViewModel>();
        }
    }
}
