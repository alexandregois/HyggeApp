using HyggeAPP.ViewModels;
using HyggeAPP.Views;
using Prism;
using Prism.Ioc;
using SharedTools.Models.WebPortal_API;
using System;
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


            //string strNome = Preferences.Get("nome", null);
            //string strToken = Preferences.Get("token", null);
            //string strCodUsuario = Preferences.Get("codusuario", null);
            //string strRecId = Preferences.Get("recid", null).ToString();


            //if (!String.IsNullOrEmpty(strNome)
            //    && !String.IsNullOrEmpty(strToken)
            //    && !String.IsNullOrEmpty(strCodUsuario)
            //    && !String.IsNullOrEmpty(strRecId))
            //{
            //    Usuario.nome = Preferences.Get("nome", null);
            //    Usuario.token = Preferences.Get("token", null);
            //    Usuario.cod_usuario = Preferences.Get("codusuario", null);
            //    Usuario.rec_id = Convert.ToInt32(Preferences.Get("recid", null));
            //}

            //if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            //    await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/MainPage");
            //else
            //    await NavigationService.NavigateAsync("NavigationPage/ViewMenuPrincipal");

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
            //await NavigationService.NavigateAsync("NavigationPage/ViewMenuPrincipal");

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
            containerRegistry.RegisterForNavigation<ViewRecuperarSenha, ViewRecuperarSenhaViewModel>();
        }
    }
}
