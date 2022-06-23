using Acr.UserDialogs;
using HyggeAPP.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HyggeAPP.ViewModels
{
    public class ViewMenuPrincipalViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public App _app => (Application.Current as App);

        public DelegateCommand OpenEnderecoCommand { get; set; }
        public DelegateCommand OpenMenuCommand { get; set; }


        private string nome;
        public string Nome
        {
            get { return this.nome; }
            set
            {
                if (value != this.nome)
                {
                    this.nome = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Nome"));
                    }
                }
            }
        }

        private string nome1;
        public string Nome1
        {
            get { return this.nome1; }
            set
            {
                if (value != this.nome1)
                {
                    this.nome1 = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Nome1"));
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

        public ViewMenuPrincipalViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            //Nome = "Olá " + Preferences.Get("nome", "default_value") + " !";
            Nome = "Olá " + _app.Usuario.nome + " !";
            Nome1 = "Podemos cuidar do seu carro?";

            OpenEnderecoCommand = new DelegateCommand(OpenEnderecoAsync);
            OpenMenuCommand = new DelegateCommand(OpenMenu);
        }

        private async void OpenEnderecoAsync()
        {
            //await NavigationService.NavigateAsync("ViewCadastroEndereco");
        }

        private async void OpenMenu()
        {

            //ViewMenuPrincipal vv = new ViewMenuPrincipal();

            //string action = await vv.DisplayActionSheet("Escolha uma opção", "Cancel", null, "Veículos", "Endereços", "Cartões");


            String[] s2 = new String[3] { "Veículos", "Endereços", "Cartões" };


            var action =  await UserDialogs.Instance.ActionSheetAsync("Escolha uma opção", "Cancel", null, null, s2);


            if (action == "Endereços")
                await NavigationService.NavigateAsync("NavigationPage/ViewListaEnderecos");
        }
    }
}
 