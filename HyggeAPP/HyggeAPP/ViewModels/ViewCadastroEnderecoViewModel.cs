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
    public class ViewCadastroEnderecoViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public App _app => (Application.Current as App);

        public DelegateCommand SalvarCommand { get; set; }
        public DelegateCommand CancelarCommand { get; set; }

        public DelegateCommand TextCepChangedCommand { get; set; }

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

        private bool isAtivo;
        public bool IsAtivo
        {
            get { return this.isAtivo; }
            set
            {
                if (value != this.isAtivo)
                {
                    this.isAtivo = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("IsAtivo"));
                    }
                }
            }
        }

        private bool isPrincipal;
        public bool IsPrincipal
        {
            get { return this.isPrincipal; }
            set
            {
                if (value != this.isPrincipal)
                {
                    this.isPrincipal = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("IsPrincipal"));
                    }
                }
            }
        }
        private string cep;
        public string Cep
        {
            get { return this.cep; }
            set
            {
                if (value != this.cep)
                {
                    this.cep = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Cep"));
                    }
                }
            }
        }

        private string descricao;
        public string Descricao
        {
            get { return this.descricao; }
            set
            {
                if (value != this.descricao)
                {
                    this.descricao = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Descricao"));
                    }
                }
            }
        }

        private string logradouro;
        public string Logradouro
        {
            get { return this.logradouro; }
            set
            {
                if (value != this.logradouro)
                {
                    this.logradouro = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Logradouro"));
                    }
                }
            }
        }

        private string bairro;
        public string Bairro
        {
            get { return this.bairro; }
            set
            {
                if (value != this.bairro)
                {
                    this.bairro = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Bairro"));
                    }
                }
            }
        }

        private string cidade;
        public string Cidade
        {
            get { return this.cidade; }
            set
            {
                if (value != this.cidade)
                {
                    this.cidade = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Cidade"));
                    }
                }
            }
        }

        private string uf;
        public string Uf
        {
            get { return this.uf; }
            set
            {
                if (value != this.uf)
                {
                    this.uf = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Uf"));
                    }
                }
            }
        }

        private string numero;
        public string Numero
        {
            get { return this.numero; }
            set
            {
                if (value != this.numero)
                {
                    this.numero = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Numero"));
                    }
                }
            }
        }

        private string complemento;
        public string Complemento
        {
            get { return this.complemento; }
            set
            {
                if (value != this.complemento)
                {
                    this.complemento = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Complemento"));
                    }
                }
            }
        }

        public ViewCadastroEnderecoViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            IsTaskRunning = false;
            TextCepChangedCommand = new DelegateCommand(TextCepChanged);
            CancelarCommand = new DelegateCommand(Cancelar);
            SalvarCommand = new DelegateCommand(Salvar);
            IsAtivo = false;
            IsPrincipal = false;
        }

        private async void Cancelar()
        {
            LimpaCampos();

            IsTaskRunning = false;

            await NavigationService.NavigateAsync("ViewMenuPrincipal");

        }

        private async void Salvar()
        {
            if (String.IsNullOrEmpty(Cep))
            {
                UserDialogs.Instance.Alert("Favor informar o cep.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Descricao))
            {
                UserDialogs.Instance.Alert("Favor informar o descrição.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Logradouro))
            {
                UserDialogs.Instance.Alert("Favor informar o logradouro.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Bairro))
            {
                UserDialogs.Instance.Alert("Favor informar o bairro.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Cidade))
            {
                UserDialogs.Instance.Alert("Favor informar o cidade.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Uf))
            {
                UserDialogs.Instance.Alert("Favor informar o uf.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Numero))
            {
                UserDialogs.Instance.Alert("Favor informar o número.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Complemento))
            {
                UserDialogs.Instance.Alert("Favor informar o complemento.", "AVISO");
                return;
            }

            IsTaskRunning = true;

            UsuarioEnderecoModel model = new UsuarioEnderecoModel();
            model.usuario_rec_id = _app.Usuario.rec_id;
            model.cep = Cep;
            model.logradouro = Logradouro;
            model.bairro = Bairro;
            model.cidade = Cidade;
            model.uf = Uf;
            model.numero = Numero;
            model.descricao_amigavel = Descricao;

            if (IsAtivo)
                model.ativo = 1;
            else
                model.ativo = 0;

            model.principal = IsPrincipal;

            try
            {

                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                var apiRetorno = await metodoAPI.PostCadEndereco(model, _app.Usuario.token);


                UserDialogs.Instance.Alert("Endereço cadastrado com sucesso.", "Aviso");

                LimpaCampos();

                IsTaskRunning = false;

                await NavigationService.NavigateAsync("ViewMenuPrincipal");


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

        private void LimpaCampos()
        {
            Cep = string.Empty;
            Logradouro = string.Empty;
            Bairro = string.Empty;
            Cidade = string.Empty;
            Uf = string.Empty;
            Numero = string.Empty;
            Descricao = string.Empty;

            IsAtivo = false;
            IsPrincipal = false;
        }

        private async void TextCepChanged()
        {

            if (String.IsNullOrEmpty(Cep))
            {
                UserDialogs.Instance.Alert("Favor informar o cep.", "AVISO");
                return;
            }

            IsTaskRunning = true;

            var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

            var apiRetorno = await metodoAPI.PostConsultaCEP(_app.Usuario.token, Cep);

            Logradouro = apiRetorno.logradouro;
            Bairro = apiRetorno.bairro;
            Cidade = apiRetorno.localidade;
            Uf = apiRetorno.uf;

            IsTaskRunning = false;

        }
    }
}
