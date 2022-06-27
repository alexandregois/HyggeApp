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
    public class ViewCadastroVeiculoViewModel : ViewModelBase
    {
        public DelegateCommand SalvarCommand { get; set; }
        public DelegateCommand CancelarCommand { get; set; }

        private bool isTaskRunning;
        public bool IsTaskRunning
        {
            get { return this.isTaskRunning; }
            set => SetProperty(ref this.isTaskRunning, value);
        }

        private int rec_id;
        public int Rec_id
        {
            get { return this.rec_id; }
            set => SetProperty(ref this.rec_id, value);
        }

        private int usuario_rec_id;
        public int Usuario_rec_id
        {
            get { return this.usuario_rec_id; }
            set => SetProperty(ref this.usuario_rec_id, value);
        }

        private int modelo_rec_id;
        public int Modelo_rec_id
        {
            get { return this.modelo_rec_id; }
            set => SetProperty(ref this.modelo_rec_id, value);
        }

        private int marca_rec_id;
        public int Marca_rec_id
        {
            get { return this.marca_rec_id; }
            set => SetProperty(ref this.marca_rec_id, value);
        }

        private bool isPrincipal;
        public bool IsPrincipal
        {
            get { return this.isPrincipal; }
            set => SetProperty(ref this.isPrincipal, value);
        }

        private bool isPinturaMetalica;
        public bool IsPinturaMetalica
        {
            get { return this.isPinturaMetalica; }
            set => SetProperty(ref this.isPinturaMetalica, value);
        }

        private string descricaoAmigavel;
        public string DescricaoAmigavel
        {
            get { return this.descricaoAmigavel; }
            set => SetProperty(ref this.descricaoAmigavel, value);
        }

        private string cor;
        public string Cor
        {
            get { return this.cor; }
            set => SetProperty(ref cor, value);
        }

        private string descricaoMarca;
        public string DescricaoMarca
        {
            get { return this.descricaoMarca; }
            set => SetProperty(ref descricaoMarca, value);
        }

        private string descricaoModelo;
        public string DescricaoModelo
        {
            get { return this.descricaoModelo; }
            set => SetProperty(ref descricaoModelo, value);
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get => _isUpdate;
            set => SetProperty(ref _isUpdate, value);
        }

        private MarcaVeiculoModel marca;
        public MarcaVeiculoModel Marca
        {
            get => marca;
            set
            {
                SetProperty(ref marca, value);
                _ = CarregarModelo(value.red_id);
            }
        }

        private int selectedIndexModel = -1;
        public int SelectedIndexModel { get => selectedIndexModel; set => SetProperty(ref selectedIndexModel, value); }

        private int selectedIndexMarca = -1;
        public int SelectedIndexMarca { get => selectedIndexMarca; set => SetProperty(ref selectedIndexMarca, value); }

        private ModeloVeiculoModel modelo;
        public ModeloVeiculoModel Modelo { get => modelo; set => SetProperty(ref modelo, value); }

        private ObservableCollection<MarcaVeiculoModel> marcas = new ObservableCollection<MarcaVeiculoModel>();
        public ObservableCollection<MarcaVeiculoModel> Marcas
        {
            get => marcas; set => SetProperty(ref marcas, value);
        }

        private ObservableCollection<ModeloVeiculoModel> modelos = new ObservableCollection<ModeloVeiculoModel>();

        public ObservableCollection<ModeloVeiculoModel> Modelos
        {
            get => modelos; set => SetProperty(ref modelos, value);
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

            if (String.IsNullOrEmpty(Marca?.descricao_marca))
            {
                UserDialogs.Instance.Alert("Favor informar a marca", "AVISO");
                return;
            }

            if (String.IsNullOrEmpty(Modelo?.descricao_modelo))
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
                    descricao_marca = Marca.descricao_marca,
                    descricao_modelo = Modelo.descricao_modelo,
                    principal = IsPrincipal,
                    pintura_metalica = IsPinturaMetalica,
                    usuario_rec_id = Convert.ToInt32(Preferences.Get("rec_id", 0)),
                    rec_id = IsUpdate ? Rec_id : 1000,
                    marca_rec_id = Marca.red_id,
                    modelo_rec_id = Modelo.rec_id
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

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await CarregarMarcas();

            if (parameters.GetNavigationMode() == Prism.Navigation.NavigationMode.Back)
                return;

            var veiculo = parameters.GetValue<VeiculoClienteModel>("veiculo");
            if (veiculo is null)
                return;

            await CarregarVeiculo(veiculo);
        }

        private async Task CarregarVeiculo(VeiculoClienteModel veiculo)
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
            Marca = Marcas?.FirstOrDefault(x => x.red_id == veiculo.marca_rec_id);
            SelectedIndexMarca = Marcas.IndexOf(Marca);
            rec_id = veiculo.rec_id;

        }

        internal async Task CarregarMarcas()
        {
            try
            {
                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                var result = await metodoAPI.ListarMarcasVeiculos(Preferences.Get("token", ""));

                if (result is null)
                    return;

                Marcas?.Clear();
                result.ForEach(x =>
                {
                    Marcas.Add(x);
                });

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

        }

        internal async Task<bool> CarregarModelo(int marca_rec_id)
        {
            try
            {
                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                var result = await metodoAPI.ListarModelosVeiculosByMarca(Preferences.Get("token", ""), marca_rec_id);

                if (result is null)
                    return false;

                Modelos?.Clear();
                result.ForEach(x =>
                {
                    Modelos.Add(x);
                });

                if (IsUpdate)
                {
                    Modelo = Modelos?.FirstOrDefault(x => x.rec_id == modelo_rec_id);
                    SelectedIndexModel = Modelos.IndexOf(Modelo);
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return true;
        }
    }
}
