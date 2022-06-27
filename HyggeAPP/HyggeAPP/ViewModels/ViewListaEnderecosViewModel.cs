using Acr.UserDialogs;
using HyggeAPP.Interface;
using HyggeAPP.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using SharedTools.Models.WebPortal_API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HyggeAPP.ViewModels
{
    public class ViewListaEnderecosViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public App _app => (Application.Current as App);

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


        private List<UsuarioEnderecoModel> listaEnderecos;
        public List<UsuarioEnderecoModel> ListaEnderecos
        {
            get { return this.listaEnderecos; }
            set
            {
                if (value != this.listaEnderecos)
                {
                    this.listaEnderecos = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("ListaEnderecos"));
                    }
                }
            }
        }

        private DelegateCommand _enderecoCommand;
        public DelegateCommand EnderecoCommand =>
            _enderecoCommand ?? (_enderecoCommand = new DelegateCommand(async () => await ExecuteEnderecoCommand()));

        private DelegateCommand<UsuarioEnderecoModel> _inativarCommand;
        public DelegateCommand<UsuarioEnderecoModel> InativarCommand =>
            _inativarCommand ?? (_inativarCommand = new DelegateCommand<UsuarioEnderecoModel>(async (item) => await ExecuteInativarCommand(item)));


        private DelegateCommand<UsuarioEnderecoModel> _itemSelectedCommand;
        public DelegateCommand<UsuarioEnderecoModel> ItemSelectedCommand =>
            _itemSelectedCommand ?? (_itemSelectedCommand = new DelegateCommand<UsuarioEnderecoModel>(async (item) => await ExecuteItemSelectedCommand(item)));

        public ViewListaEnderecosViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            GetEnderecosCliente();
        }

        public async Task GetEnderecosCliente()
        {
            try
            {

                IsTaskRunning = true;

                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                //var resultRetorno = await metodoAPI.PostListaEndereco(Preferences.Get("token", ""), Preferences.Get("rec_id", 0));
                var resultRetorno = await metodoAPI.PostListaEndereco(_app.Usuario.token, _app.Usuario.rec_id);


                if (resultRetorno.Count() > 0)
                {
                    ListaEnderecos?.Clear();
                    ListaEnderecos = resultRetorno;
                }

                IsTaskRunning = false;

         
            }
            catch (Exception e)
            {

                if (e.Message == "No such host is known")
                    UserDialogs.Instance.Alert("Erro ao acessar os servidores. Por favor, tente mais tarde.", "Aviso");
                else
                    UserDialogs.Instance.Alert("Ocorreu um erro. Por favor, tente novamente.", "Aviso");


                //throw;
                IsTaskRunning = false;
                return;
            }
        }


        private async Task ExecuteEnderecoCommand()
            => await NavigationService.NavigateAsync(nameof(ViewCadastroEndereco));

        private async Task ExecuteItemSelectedCommand(UsuarioEnderecoModel item)
        {
            if (item is null)
                return;

            var parametros = new NavigationParameters();
            parametros.Add("endereco", item);

            await NavigationService.NavigateAsync(nameof(ViewCadastroEndereco), parametros);
        }


        private async Task ExecuteInativarCommand(UsuarioEnderecoModel item)
        {
            try
            {
                IsTaskRunning = false;

                if (item is null)
                    return;

                var check = await UserDialogs.Instance.ConfirmAsync("Deseja realmente desativar o endereço?", "Aviso", "Ok", "Cancel");
                if (check)
                {
                    var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                    var resultRetorno = await metodoAPI.InativarCadEndereco(item, Preferences.Get("token", ""));
                    await GetEnderecosCliente();
                    UserDialogs.Instance.Alert("Endereço desativado com sucesso!", "Aviso");
                }
                else
                { }

                
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert("Ocorreu um erro. Por favor, tente novamente.", "Aviso");
            }
            finally
            {
                IsTaskRunning = false;
            }
        }
    }
}
