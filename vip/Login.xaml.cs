using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace vip
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void SubmitLogin(object sender, EventArgs args)
        {
            Console.WriteLine("login action");
            await Navigation.PushAsync(new Services());
        }
    }
}
