using Xamarin.Forms;

namespace HyggeAPP.Views
{
    public partial class ViewListaVeiculos : ContentPage
    {
        public ViewListaVeiculos()
        {
            InitializeComponent();
        }

        private void listaVeiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listaVeiculos.SelectedItem = null;
        }
    }
}
