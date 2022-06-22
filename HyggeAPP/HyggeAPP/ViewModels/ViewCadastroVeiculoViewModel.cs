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

        public ViewCadastroVeiculoViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }
    }
}
