using Acr.UserDialogs;
using HyggeAPP.Interface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using SharedTools.Models.WebPortal_API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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


        public ViewListaEnderecosViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            GetEnderecosCliente();
        }

        public async void GetEnderecosCliente()
        {
            try
            {

                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                var resultRetorno = await metodoAPI.PostListaEndereco(_app.Usuario.token, _app.Usuario.rec_id);

                IsTaskRunning = false;

                if (resultRetorno.Count() > 0)
                    ListaEnderecos = resultRetorno;


                //await NavigationService.NavigateAsync("MainPage");


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
    }
}
