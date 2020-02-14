using System;
using System.Collections.Generic;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;
using vip.Models;
using vip.Config;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using System.Threading.Tasks;

namespace vip
{
    public partial class Menu : ContentPage
    {
        ZXingScannerPage scanPage;
        //public Func<Task> Appearing { get; }

        public Menu()
        {
            InitializeComponent();            
            NavigationPage.SetHasBackButton(this, false);
           
        }

        private string loungeId = null;

        protected override void OnAppearing()
        {
            setup();
        }


        void setup() {

            try
            {
                string loungeName = Application.Current.Properties["loungeTitle"].ToString();
                if ( loungeName != null )
                {
                    //Console.WriteLine("send info data");
                    //Console.WriteLine(loungeName);
                    loungeInf.Text = "Sala: " + loungeName;

                    loungeId = Application.Current.Properties["loungeId"].ToString();
                }
            }
            catch (Exception e)
            {
                DisplayAlert("","Recuerda seleccionar una sala para realizar las lecturas","OK");
            }
            
        }
       

        async void goToQr(object sender, EventArgs args)
        {

            Console.WriteLine("Go to qr action");
            //await Navigation.PushAsync(new Menu());
            // Create our custom overlay

            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Espera", "Debe permitir a la app usar la camara para continuar", "OK");
                await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                return;
            }

            if (loungeId == null)
            {
                await DisplayAlert("","Debe seleccionar una sala para continuar","OK");
                return;
            }


                var customOverlay = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            scanPage = new ZXingScannerPage(customOverlay: customOverlay);
            scanPage.OnScanResult += (result) => {

                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    //DisplayAlert("Scanned Barcode", result.Text, "OK");
                    VerifyCard(result.Text);
                });
            };
            await Navigation.PushAsync(scanPage);
        }

        async void goToServices(object sender, EventArgs args)
        {
            loading.IsRunning = true;
            Console.WriteLine("Go to services action");
            await Navigation.PushAsync(new Rooms());
            loading.IsRunning = false;
        }

        async void giveBackServices(object sender, EventArgs args)
        {

            if (loungeId == null)
            {
                await DisplayAlert("", "Debe seleccionar una sala para continuar", "OK");
                return;
            }

            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Espera", "Debe permitir a la app usar la camara para continuar", "OK");
                await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                return;
            }

            Console.WriteLine("Give back services action");
            // Create our custom overlay
            var customOverlay = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            scanPage = new ZXingScannerPage(customOverlay: customOverlay);
            scanPage.OnScanResult += (result) => {

                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();

                    CancelService(result.Text);

                });
            };
            await Navigation.PushAsync(scanPage);
        }

        async void signOut(object sender, EventArgs args)
        {
            Console.WriteLine("Sign Out action");
            var answer = await DisplayAlert("", "¿Deseas cerrar sesión?", "Si", "No");
            if (answer)
            {
                AuthModel.DeleteCredentials();
                Application.Current.Properties.Clear();
                await Application.Current.SavePropertiesAsync();
                await Navigation.PushAsync(new Login());
            }
        }

        async void VerifyCard(string cardNumber)
        {

            loading.IsRunning = true;
            JObject queryResult = await App.restServices.verifyCard(cardNumber, loungeId);
            loading.IsRunning = false;
            if (queryResult != null)
            {
                Console.WriteLine(queryResult);

                bool validation = (bool)queryResult.GetValue("validation");

                if (validation) {

                    string message = (string)queryResult.GetValue("message");

                    if (message != null)
                    {
                        await DisplayAlert("¡Estamos para servirle!", "Por favor continue", "OK");

                        return;
                    }                    

                    await DisplayAlert("¡Pasajero aceptado!", "Número de invitados posibles " + (string)queryResult.GetValue("partners"), "OK");

                    var answer = await DisplayAlert("", "¿Desea entrar con invitados?", "Si", "No");

                    string adultC = "0";

                    string childrenC = "0";

                    if (answer)
                    {
                        adultC = await DisplayPromptAsync("", "Número de adultos acompañantes");

                        childrenC = await DisplayPromptAsync("", "Número de niños acompañantes");


                        if (Regex.IsMatch(adultC, @"^\d+$") == false || Regex.IsMatch(childrenC, @"^\d+$") == false)
                        {
                            await DisplayAlert("Espere", "Verifique la información que ingreso e intentelo nuevamente", "OK");
                            return;
                        }

                    }                                          

                    loading.IsRunning = true;

                    queryResult = await App.restServices.orderEntry(cardNumber, Constants.defaultLounge, adultC, childrenC);

                    loading.IsRunning = false;


                    if (queryResult != null)
                    {
                        validation = (bool)queryResult.GetValue("validation");

                        if (validation)
                        {
                            string transsequno = (string)queryResult.GetValue("transsequno");

                            await DisplayAlert("Entrada autorizada", "Codigo de transacción generado " + transsequno, "OK");
                        }

                        else
                        {
                            await DisplayAlert("Espera", "No puede continuar, verifica los invitados e ¡intentalo nuevamente!", "OK");
                        }

                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo generar el código de transacción", "OK");
                    }                  

                }
                else {
                    await DisplayAlert("Espera", "La tarjeta no cuenta con servicio de salas vip", "OK");
                }
            }
            
        }

        async void CancelService(string cardNumber)
        {
            loading.IsRunning = true;
            JObject queryResult = await App.restServices.cancelEntry(cardNumber);
            loading.IsRunning = false;

            if (queryResult != null)
            {
                Console.WriteLine(queryResult);

                bool validation = (bool)queryResult.GetValue("validation");

                string message = (string)queryResult.GetValue("message");

                if (validation)
                {
                    await DisplayAlert("Reversión", "Servicio cancelado", "OK");
                }
                else {

                    string responseText = " No se pudo cancelar el servicio ";

                    if (message != null)
                    {
                        responseText += ", "+message;
                        
                    }

                    await DisplayAlert("Espera",
                            responseText, "OK");
                }
            }



        }
    }
}
