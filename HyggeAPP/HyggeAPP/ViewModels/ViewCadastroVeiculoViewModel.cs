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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HyggeAPP.ViewModels
{
    public class ViewCadastroVeiculoViewModel : ViewModelBase, INotifyPropertyChanged
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

        private int rec_id;
        public int Rec_id
        {
            get { return this.rec_id; }
            set
            {
                if (value != this.rec_id)
                {
                    this.rec_id = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Rec_id"));
                    }
                }
            }
        }

        private int usuario_rec_id;
        public int Usuario_rec_id
        {
            get { return this.usuario_rec_id; }
            set
            {
                if (value != this.usuario_rec_id)
                {
                    this.usuario_rec_id = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Usuario_rec_id"));
                    }
                }
            }
        }

        private int modelo_rec_id;
        public int Modelo_rec_id
        {
            get { return this.modelo_rec_id; }
            set
            {
                if (value != this.modelo_rec_id)
                {
                    this.modelo_rec_id = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Modelo_rec_id"));
                    }
                }
            }
        }

        private int marca_rec_id;
        public int Marca_rec_id
        {
            get { return this.marca_rec_id; }
            set
            {
                if (value != this.marca_rec_id)
                {
                    this.marca_rec_id = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Marca_rec_id"));
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

        private bool isPinturaMetalica;
        public bool IsPinturaMetalica
        {
            get { return this.isPinturaMetalica; }
            set
            {
                if (value != this.isPinturaMetalica)
                {
                    this.isPinturaMetalica = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("IsPinturaMetalica"));
                    }
                }
            }
        }

        private string descricaoAmigavel;
        public string DescricaoAmigavel
        {
            get { return this.descricaoAmigavel; }
            set
            {
                if (value != this.descricaoAmigavel)
                {
                    this.descricaoAmigavel = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("DescricaoAmigavel"));
                    }
                }
            }
        }

        private string cor;
        public string Cor
        {
            get { return this.cor; }
            set
            {
                if (value != this.cor)
                {
                    this.cor = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Cor"));
                    }
                }
            }
        }

        private string descricaoMarca;
        public string DescricaoMarca
        {
            get { return this.descricaoMarca; }
            set
            {
                if (value != this.descricaoMarca)
                {
                    this.descricaoMarca = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("DescricaoMarca"));
                    }
                }
            }
        }

        private string descricaoModelo;
        public string DescricaoModelo
        {
            get { return this.descricaoModelo; }
            set
            {
                if (value != this.descricaoModelo)
                {
                    this.descricaoModelo = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("DescricaoModelo"));
                    }
                }
            }
        }

        private bool _isUpdate;
        public bool IsUpdate
        {
            get => _isUpdate;
            set => SetProperty(ref _isUpdate, value);
        }


        public ViewCadastroVeiculoViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            SalvarCommand = new DelegateCommand(async () => await ExecuteSalvarCommand());
        }

        private async Task ExecuteSalvarCommand()
        {
            if (String.IsNullOrEmpty(descricaoAmigavel))
            {
                UserDialogs.Instance.Alert("Favor informar a descrição.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Cor))
            {
                UserDialogs.Instance.Alert("Favor informar a cor.", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(DescricaoMarca))
            {
                UserDialogs.Instance.Alert("Favor informar a marca", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(DescricaoModelo))
            {
                UserDialogs.Instance.Alert("Favor informar o modelo", "AVISO");
                return;
            }

            try
            {
                Isbusy = true;
                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                var veiculo = new VeiculoClienteModel
                {
                    cor = Cor,
                    descricao_amigavel = DescricaoAmigavel,
                    descricao_marca = DescricaoMarca,
                    descricao_modelo = DescricaoModelo,
                    principal = IsPrincipal,
                    pintura_metalica = IsPinturaMetalica,
                    usuario_rec_id = Convert.ToInt32(Preferences.Get("rec_id", 0)),
                    rec_id = 1000,
                    marca_rec_id = 1001,
                    modelo_rec_id = 1002
                };

                if (IsUpdate)
                {
                    await metodoAPI.AlterarVeiculo(veiculo, Preferences.Get("token", ""));
                    UserDialogs.Instance.Alert("Veículo editado com sucesso!", "AVISO");
                }
                else
                {
                    await metodoAPI.PostVeiculo(veiculo, Preferences.Get("token", ""));
                    UserDialogs.Instance.Alert("Veículo cadastrado com sucesso!", "AVISO");
                }

                await NavigationService.GoBackAsync();

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert("Ocorreu um erro. Por favor, tente novamente.", "Aviso");
            }
            finally
            {
                Isbusy = false;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() == Prism.Navigation.NavigationMode.Back)
                return;

            var veiculo = parameters.GetValue<VeiculoClienteModel>("veiculo");
            if (veiculo is null)
                return;

            CarregarVeiculo(veiculo);
        }

        private void CarregarVeiculo(VeiculoClienteModel veiculo)
        {
            IsUpdate = true;
            IsPinturaMetalica = veiculo.pintura_metalica;
            IsPrincipal = veiculo.principal;
            DescricaoAmigavel = veiculo.descricao_amigavel;
            Cor = veiculo.cor;
            descricaoMarca = veiculo.descricao_marca;
            DescricaoModelo = veiculo.descricao_modelo;
            marca_rec_id = veiculo.marca_rec_id;
            modelo_rec_id = veiculo.modelo_rec_id;
        }
    }
}
