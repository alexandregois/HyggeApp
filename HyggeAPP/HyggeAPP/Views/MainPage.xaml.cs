using Xamarin.Forms;

namespace HyggeAPP.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

#if DEBUG
            login.Text = "alaxbf";
            senha.Text = "123";
#endif

        }

    }
}
