using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SharedTools.Models.WebPortal_API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace HyggeAPP.ViewModels
{
    public class ViewListaEnderecosViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public App _app => (Application.Current as App);

        ObservableCollection<UsuarioEnderecoModel> enderecos;
        public ObservableCollection<UsuarioEnderecoModel> Enderecos
        {
            get
            {
                if (enderecos == null)
                {
                    enderecos = new ObservableCollection<UsuarioEnderecoModel>();
                }
                return enderecos;
            }
            set
            {
                if (value != enderecos)
                {
                    enderecos = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                            new PropertyChangedEventArgs("Enderecos"));
                    }
                }
            }
        }

        public ViewListaEnderecosViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }
    }
}
