using System;
using System.Net.Http;
using vip.Config;
using vip.Models;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using vip.Helpers;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;
using System.Reactive.Linq;

namespace vip.Services
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();

            //Console.WriteLine("App Token {0}", AuthModel.Token);

            this._client.DefaultRequestHeaders.Clear();

            if (AuthModel.Token != null)
            {
                Console.WriteLine("App Token {0}", AuthModel.Token);
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthModel.Token);
            }

        }

        public async Task<bool> login(string username, string password)
        {

            //Console.WriteLine("login action");
            //Console.WriteLine(username);
            //Console.WriteLine(password);

            var uri = new Uri(string.Format(Constants.LoginUrl, string.Empty));
            Console.WriteLine(uri);
            HttpResponseMessage response = null;
            LoginModel loginModel = new LoginModel(username, password);

            try
            {

                var json = JsonConvert.SerializeObject(loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync(uri, content);

                if (await RestHelper.manageResponseAsync(response))
                {
                    var contents = await response.Content.ReadAsStringAsync();

                    JObject jsonResponse = JObject.Parse(contents);

                    string token = (string)jsonResponse.GetValue("token");

                    Account account = new Account
                    {
                        Username = username
                    };

                    account.Properties.Add("Token", token);

                    AccountStore.Create(Constants.appKey).Save(account, App.AppName);

                    this._client.DefaultRequestHeaders.Clear();

                    this._client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "Existen problemas de conexión con el servidor.", "OK");
                return false;
            }

        }

        public async Task<JObject> verifyCard(string cardno,
            string loungeid)
        {
            var uri = new Uri(string.Format(Constants.queryCardUrl, string.Empty));
            restModels.queryCard queryCardModel = new restModels.queryCard(cardno, loungeid);
            var json = JsonConvert.SerializeObject(queryCardModel);
            Console.WriteLine(json);
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, content);
                if (await RestHelper.manageResponseAsync(response))
                {
                    Console.WriteLine(response);
                    var contents = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(contents);
                    return jsonResponse;
                }

                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "Existen problemas de conexión con el servidor.", "OK");
                return null;

            }
        }

        public async Task<JObject> orderEntry(string cardno,
            string loungeid,
            string adultcount,
            string childcount)
        {
            var uri = new Uri(string.Format(Constants.orderEntryUrl, string.Empty));
            restModels.orderEntry orderEntryModel = new restModels.orderEntry(cardno, loungeid, adultcount, childcount);
            var json = JsonConvert.SerializeObject(orderEntryModel);
            Console.WriteLine(json);
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, content);
                if (await RestHelper.manageResponseAsync(response))
                {
                    Console.WriteLine(response);
                    var contents = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(contents);
                    return jsonResponse;
                }

                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "Existen problemas de conexión con el servidor.", "OK");
                return null;

            }
        }

        public async Task<JObject> cancelEntry(string cardno)
        {
            var uri = new Uri(string.Format(Constants.cancelEntryUrl, string.Empty));
            restModels.cancelEntry cancelEntryModel = new restModels.cancelEntry(cardno);
            var json = JsonConvert.SerializeObject(cancelEntryModel);
            Console.WriteLine(json);
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, content);
                if (await RestHelper.manageResponseAsync(response))
                {
                    Console.WriteLine(response);
                    var contents = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(contents);
                    return jsonResponse;
                }

                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "Existen problemas de conexión con el servidor.", "OK");
                return null;

            }
        }


        /*public async Task<IObservable<string>> GetRooms()
        {
            var uri = new Uri(string.Format(Constants.getRoomsUrl, string.Empty));
            HttpResponseMessage response = await _client.GetAsync(uri);
            string ires = "dsasd";
            return (System.IObservable<string>)ires.ToObservable();
        }*/

        public async Task<JArray> GetRooms()
        {
            var uri = new Uri(string.Format(Constants.getRoomsUrl, string.Empty));

            try {
                HttpResponseMessage response = await _client.GetAsync(uri);
                Console.WriteLine(response);
                if (await RestHelper.manageResponseAsync(response))
                {
                    var contents = await response.Content.ReadAsStringAsync();
                  
                    var jsonResponse = JArray.Parse(contents);

                    return jsonResponse;
                }

                return null;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await Application.Current.MainPage.DisplayAlert("Error", "Existen problemas de conexión con el servidor.", "OK");
                return null;

            }

        }

    }
       
}
