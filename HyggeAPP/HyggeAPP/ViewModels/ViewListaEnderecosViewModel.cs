using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
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
    }
}
