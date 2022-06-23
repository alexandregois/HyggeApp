using HyggeAPP.Interface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
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

        public ViewListaEnderecosViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public async void GetEnderecosCliente(int IdCliente)
        {
            try
            {

                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                var resultRetorno = await metodoAPI.PostListaEndereco(_app.Usuario.token, _app.Usuario.rec_id);


                UserDialogs.Instance.Alert("Cadastro efetuado com sucesso.", "Aviso");

                LimpaCampos();

                IsTaskRunning = false;

                await NavigationService.NavigateAsync("MainPage");


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
