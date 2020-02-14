using System;
using System.Linq;
using vip.Config;
using Xamarin.Auth;

namespace vip.Models
{
    public static class AuthModel
    {
        public static string UserName
        {
            get
            {
                var account = AccountStore.Create(Constants.appKey).FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Username : null;
            }
        }

        public static string Token
        {
            get
            {
                var account = AccountStore.Create(Constants.appKey).FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["Token"] : null;
            }
        }

        public static void DeleteCredentials()
        {
            var account = AccountStore.Create(Constants.appKey).FindAccountsForService(App.AppName).FirstOrDefault();
            if (account != null)
            {
                AccountStore.Create(Constants.appKey).Delete(account, App.AppName);
            }
        }
    }
}
