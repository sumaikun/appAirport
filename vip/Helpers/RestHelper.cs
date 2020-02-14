using System;
using System.Net.Http;
using vip.Models;
using Xamarin.Forms;

namespace vip.Helpers
{
    public static class RestHelper
    {
        public static async System.Threading.Tasks.Task<bool> manageResponseAsync(HttpResponseMessage response)
        {
            Console.WriteLine("dato de pagina");
            //Console.WriteLine(page);

            string resposeStatus = response.StatusCode.ToString();

            if (resposeStatus == "OK")
            {
                return true;
            }

            else {

                if (resposeStatus == "Unauthorized")
                {
                    if (Application.Current.MainPage.ToString() == "vip.Login")
                    {
                        await Application.Current.MainPage.DisplayAlert("Espera", "Las credenciales no son validas.", "OK");
                    }
                    else {
                        await Application.Current.MainPage.DisplayAlert("Espera", "Error de autorización.", "OK");
                        AuthModel.DeleteCredentials();
                        await Application.Current.MainPage.Navigation.PushAsync(new Login());
                    }
                    
                    return false;
                }
                else { 
                    await Application.Current.MainPage.DisplayAlert("Error", "Existe un error en el servidor.", "OK");
                    return false;
                }

                //return false;
            }

        }
    }
}
