using System;
namespace vip.Models
{
    public class LoginModel
    {
        public LoginModel()
        {

        }

        public LoginModel(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string username { get; set; }

        public string password { get; set; }
    }
}
