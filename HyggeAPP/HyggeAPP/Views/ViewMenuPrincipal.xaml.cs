﻿using System.ComponentModel;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HyggeAPP.Views
{
    public partial class ViewMenuPrincipal : ContentPage
    {
        INavigationService navigationService;

        public ViewMenuPrincipal()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

           
        }

        private async void btnMenu_Clicked(System.Object sender, System.EventArgs e)
        {
            //string action = await DisplayActionSheet("Escolha uma opção", "Cancel", null, "Veículos", "Endereços", "Cartões");

            //if (action == "Endereços")
            //    await navigationService.NavigateAsync("NavigationPage/MainPage");
        }
    }
}
