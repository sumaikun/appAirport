using System;
using vip.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace vip
{
    public partial class App : Application
    {
        public static vip.Services.RestService restServices { get; private set; }

        public static readonly string AppName = "Vip Rooms";

        public App()
        {
            restServices = new vip.Services.RestService();

            InitializeComponent();

            ContentPage initialPage = new Login();

            if (AuthModel.Token != null)
            {
                initialPage = new Menu();
            }            

            MainPage = new NavigationPage(initialPage)
            {
                BarBackgroundColor = Color.FromHex("#151852"),
                BarTextColor = Color.White
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Console.WriteLine("On sleep");
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            Console.WriteLine("On resume");
            
        }
    }
}
