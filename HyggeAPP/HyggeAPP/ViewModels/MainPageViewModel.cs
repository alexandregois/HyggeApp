using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Refit;
using SharedTools.Models;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HyggeAPP.Interface;
using SharedTools.Models.WebPortal_API;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HyggeAPP.ViewModels
{
    public class MainPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public DelegateCommand LoginCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public App _app => (Application.Current as App);

        public DelegateCommand OpenCadastroCommand { get; set; }

        private string usuario;
        public string Usuario
        {
            get { return this.usuario; }
            set
            {
                if (value != this.usuario)
                {
                    this.usuario = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Usuario"));
                    }
                }
            }
        }

        private string senha;
        public string Senha
        {
            get { return this.senha; }
            set
            {
                if (value != this.senha)
                {
                    this.senha = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Senha"));
                    }
                }
            }
        }

        private bool isTaskRunning;
        public bool IsTaskRunning
        {
            get { return this.isTaskRunning; }
            set
            {
                if (value != this.isTaskRunning)
                {
                    this.isTaskRunning = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("IsTaskRunning"));
                    }
                }
            }
        }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Hygge";
            IsTaskRunning = false;
            LoginCommand = new DelegateCommand(LoginAsync);
            OpenCadastroCommand = new DelegateCommand(OpenCadastroAsync);


            isTaskRunning = true;

            string strNome = Preferences.Get("nome", null);
            string strToken = Preferences.Get("token", null);
            string strCodUsuario = Preferences.Get("codusuario", null);
            string strRecId = Preferences.Get("recid", null);
            string strLogin = Preferences.Get("login", null);
            string strSenha = Preferences.Get("senha", null);


            if (!String.IsNullOrEmpty(strNome)
                && !String.IsNullOrEmpty(strToken)
                && !String.IsNullOrEmpty(strCodUsuario)
                && !String.IsNullOrEmpty(strRecId))
            {
                _app.Usuario.nome = Preferences.Get("nome", null);
                _app.Usuario.token = Preferences.Get("token", null);
                _app.Usuario.cod_usuario = Preferences.Get("codusuario", null);
                _app.Usuario.rec_id = Convert.ToInt32(Preferences.Get("recid", null));
                _app.Usuario.login = Preferences.Get("login", null);
                _app.Usuario.password = Preferences.Get("senha", null);
            }


            if (!String.IsNullOrEmpty(_app.Usuario.nome)
                && !String.IsNullOrEmpty(_app.Usuario.token)
                && !String.IsNullOrEmpty(_app.Usuario.cod_usuario)
                )
            {
                LoginTokenAsync();
                Task.Delay(2000);

                isTaskRunning = false;

                NavigationService.NavigateAsync("ViewMenuPrincipal");
            }

        }

        private async void OpenCadastroAsync()
        {
            await NavigationService.NavigateAsync("ViewCadastro");
        }

        public async void LoginAsync()
        {

            if (String.IsNullOrEmpty(Usuario))
            {
                UserDialogs.Instance.Alert("Favor informar o usuário.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Senha))
            {
                UserDialogs.Instance.Alert("Favor informar a senha.", "AVISO");
                return;
            }

            IsTaskRunning = true;

            UsuarioModel userUsuario = new UsuarioModel
            {
                login = Usuario,
                password = Senha
            };

            try
            {

                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                var usuariosRetorno = await metodoAPI.PostLogin(userUsuario);

                await Task.Delay(3000);

                if (usuariosRetorno.nome != string.Empty)
                {
                    UserDialogs.Instance.Alert("Login efetuado com sucesso.", "Aviso");

                    Preferences.Set("token", usuariosRetorno.token);
                    Preferences.Set("codusuario", usuariosRetorno.cod_usuario);
                    Preferences.Set("nome", usuariosRetorno.nome);
                    Preferences.Set("recid", usuariosRetorno.rec_id.ToString());
                    Preferences.Set("login", Usuario);
                    Preferences.Set("senha", Senha);

                    _app.Usuario.token = usuariosRetorno.token;
                    _app.Usuario.cod_usuario = usuariosRetorno.cod_usuario;
                    _app.Usuario.nome = usuariosRetorno.nome;
                    _app.Usuario.rec_id = usuariosRetorno.rec_id;
                    _app.Usuario.login = Usuario;
                    _app.Usuario.password = Senha;

                    IsTaskRunning = false;

                    await NavigationService.NavigateAsync("ViewMenuPrincipal");

                }
                else
                {
                    UserDialogs.Instance.Alert("Usuário não encontrado.", "Aviso");

                    IsTaskRunning = false;
                    return;
                }

            }
            catch (Exception e)
            {

                if (e.Message == "No such host is known")
                    UserDialogs.Instance.Alert("Erro ao acessar os servidores. Por favor, tente mais tarde.", "Aviso");

                //throw;
                IsTaskRunning = false;
                return;
            }

        }

        public async void LoginTokenAsync()
        {

            IsTaskRunning = true;

            try
            {

                UsuarioModel uu = new UsuarioModel();
                uu.login = _app.Usuario.login;
                uu.password = _app.Usuario.password;

                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                var usuariosRetorno = await metodoAPI.PostLogin(uu);

                await Task.Delay(1000);

                if (usuariosRetorno.nome != string.Empty)
                {

                    Preferences.Set("token", usuariosRetorno.token);
                    Preferences.Set("codusuario", usuariosRetorno.cod_usuario);
                    Preferences.Set("nome", usuariosRetorno.nome);
                    Preferences.Set("recid", usuariosRetorno.rec_id.ToString());
                    Preferences.Set("login", usuariosRetorno.login);
                    Preferences.Set("senha", usuariosRetorno.password);

                    _app.Usuario.token = usuariosRetorno.token;
                    _app.Usuario.cod_usuario = usuariosRetorno.cod_usuario;
                    _app.Usuario.nome = usuariosRetorno.nome;
                    _app.Usuario.rec_id = usuariosRetorno.rec_id;
                    _app.Usuario.login = usuariosRetorno.login;
                    _app.Usuario.password = usuariosRetorno.password;

                    IsTaskRunning = false;

                    await NavigationService.NavigateAsync("ViewMenuPrincipal");

                }
                else
                {
                    IsTaskRunning = false;
                    return;
                }

            }
            catch (Exception e)
            {

                if (e.Message == "No such host is known")
                    UserDialogs.Instance.Alert("Erro ao acessar os servidores. Por favor, tente mais tarde.", "Aviso");

                //throw;
                IsTaskRunning = false;
                return;
            }

        }

    }
}
