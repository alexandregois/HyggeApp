using Acr.UserDialogs;
using HyggeAPP.Interface;
using HyggeAPP.Views;
using Prism.Commands;
using Prism.Navigation;
using Refit;
using SharedTools.Models.WebPortal_API;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HyggeAPP.ViewModels
{
    public class ViewListaVeiculosViewModel : ViewModelBase
    {

        public ObservableCollection<VeiculoClienteModel> Veiculos { get; set; } = new ObservableCollection<VeiculoClienteModel>();

        private DelegateCommand<VeiculoClienteModel> _inativarCommand;
        public DelegateCommand<VeiculoClienteModel> InativarCommand =>
            _inativarCommand ?? (_inativarCommand = new DelegateCommand<VeiculoClienteModel>(async (item) => await ExecuteInativarCommand(item)));


        private DelegateCommand<VeiculoClienteModel> _itemSelectedCommand;
        public DelegateCommand<VeiculoClienteModel> ItemSelectedCommand =>
            _itemSelectedCommand ?? (_itemSelectedCommand = new DelegateCommand<VeiculoClienteModel>(async (item) => await ExecuteItemSelectedCommand(item)));

        private DelegateCommand _veiculoCommand;
        public DelegateCommand VeiculoCommand =>
            _veiculoCommand ?? (_veiculoCommand = new DelegateCommand(async () => await ExecuteVeiculoCommand()));


        public ViewListaVeiculosViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await CarregarVeiculos();
        }

        internal async Task CarregarVeiculos()
        {
            try
            {
                Isbusy = true;
                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                var result = await metodoAPI.PostListaVeiculo(Preferences.Get("token", ""), Convert.ToInt32(Preferences.Get("rec_id", 0)));

                if (result is null)
                    return;

                Veiculos = new ObservableCollection<VeiculoClienteModel>(result);
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


        private async Task ExecuteItemSelectedCommand(VeiculoClienteModel item)
        {
            if (item is null)
                return;

            var parametros = new NavigationParameters();
            parametros.Add("veiculo", item);

            await NavigationService.NavigateAsync(nameof(ViewCadastroVeiculo), parametros);
        }


        private async Task ExecuteInativarCommand(VeiculoClienteModel item)
        {
            try
            {
                Isbusy = false;

                if (item is null)
                    return;

                var metodoAPI = RestService.For<IRestClientApi>(Constants.ApiUrl);

                var resultRetorno = await metodoAPI.InativarVeiculo(item, Preferences.Get("token", ""));
                await CarregarVeiculos();
                UserDialogs.Instance.Alert("Veículo desativado com sucesso!", "Aviso");
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


        private async Task ExecuteVeiculoCommand()
            => await NavigationService.NavigateAsync(nameof(ViewCadastroVeiculo));
    }
}
