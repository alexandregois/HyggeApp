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
        }

        private async void OpenEnderecoAsync()
        {
            //await NavigationService.NavigateAsync("ViewCadastroEndereco");
        }
    }
}
