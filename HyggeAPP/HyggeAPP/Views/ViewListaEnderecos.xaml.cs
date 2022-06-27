using Xamarin.Forms;

namespace HyggeAPP.Views
{
    public partial class ViewListaEnderecos : ContentPage
    {
        public ViewListaEnderecos()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void listaEnderecos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listaEnderecos.SelectedItem = null;
        }
    }
}
