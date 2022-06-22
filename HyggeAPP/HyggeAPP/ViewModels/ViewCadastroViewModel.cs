using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using SharedTools.Models.WebPortal_API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HyggeAPP.Interface;
using Refit;
using Xamarin.Essentials;
using Prism.Navigation;
using Xamarin.Forms;

namespace HyggeAPP.ViewModels
{
    public class ViewCadastroViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public App _app => (Application.Current as App);

        public DelegateCommand SalvarCommand { get; set; }
        public DelegateCommand CancelarCommand { get; set; }

        public string token { get; set; }
        public string codusuario { get; set; }

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

        private bool isCheckRbCpf;
        public bool IsCheckRbCpf
        {
            get { return this.isCheckRbCpf; }
            set
            {
                if (value != this.isCheckRbCpf)
                {
                    this.isCheckRbCpf = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("IsCheckRbCpf"));
                    }
                }
            }
        }

        private bool isCheckRbCnpj;
        public bool IsCheckRbCnpj
        {
            get { return this.isCheckRbCnpj; }
            set
            {
                if (value != this.isCheckRbCnpj)
                {
                    this.isCheckRbCnpj = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("IsCheckRbCnpj"));
                    }
                }
            }
        }


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

        private string cpfcnpj;
        public string CpfCnpj
        {
            get { return this.cpfcnpj; }
            set
            {
                if (value != this.cpfcnpj)
                {
                    this.cpfcnpj = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("CpfCnpj"));
                    }
                }
            }
        }

        private string login;
        public string Login
        {
            get { return this.login; }
            set
            {
                if (value != this.login)
                {
                    this.login = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Login"));
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

        private string confirmarsenha;
        public string ConfirmarSenha
        {
            get { return this.confirmarsenha; }
            set
            {
                if (value != this.confirmarsenha)
                {
                    this.confirmarsenha = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("ConfirmarSenha"));
                    }
                }
            }
        }

        private string email;
        public string Email
        {
            get { return this.email; }
            set
            {
                if (value != this.email)
                {
                    this.email = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Email"));
                    }
                }
            }
        }

        public ViewCadastroViewModel(INavigationService navigationService)
            : base(navigationService)
        {

            IsTaskRunning = false;
            isCheckRbCpf = true;
            isCheckRbCnpj = false;
            SalvarCommand = new DelegateCommand(SalvarAsync);
            CancelarCommand = new DelegateCommand(CancelarAsync);

        }

        private void CancelarAsync()
        {
            LimpaCampos();
        }

        private void LimpaCampos()
        {
            Nome = String.Empty;
            CpfCnpj = String.Empty;
            Login = String.Empty;
            Senha = String.Empty;
            ConfirmarSenha = String.Empty;
            Email = String.Empty;
        }

        private async void SalvarAsync()
        {
            if (String.IsNullOrEmpty(Nome))
            {
                UserDialogs.Instance.Alert("Favor informar o nome.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(CpfCnpj))
            {
                UserDialogs.Instance.Alert("Favor informar cpf ou cnpj.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(login))
            {
                UserDialogs.Instance.Alert("Favor informar o login.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Email))
            {
                UserDialogs.Instance.Alert("Favor informar o e-mail.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Senha))
            {
                UserDialogs.Instance.Alert("Favor informar a senha.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(ConfirmarSenha))
            {
                UserDialogs.Instance.Alert("Favor confirmar a senha.", "AVISO");
                return;
            }


            IsTaskRunning = true;

            ClienteModel model = new ClienteModel();


            if (IsCheckRbCpf)
                model.cpf = CpfCnpj;
            else
                model.cnpj = CpfCnpj;

            model.nome = Nome;
            model.login = Login;
            model.email = Email;
            model.password = Senha;
            model.cod_usuario = Guid.NewGuid().ToString("N").Substring(0, 5);
            model.rec_id = 1;


            //model.cpf = "07036770719";
            //model.nome = "Alexandre";
            //model.login = "alex";
            //model.email = "alexandregois.ti@gmail.com";
            //model.password = "123456";
            //model.cod_usuario = Guid.NewGuid().ToString("N").Substring(0, 5);
            //model.rec_id = 1;


            try
            {

                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                var usuariosRetorno = await metodoAPI.PostCadCliente(model, null);

                
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
