using System;
using System.Collections.Generic;
using vip.Config;
using vip.Helpers;
using vip.Models;
using vip.Services;
using Xamarin.Forms;
using System.Windows.Input;

namespace vip
{
    public partial class Login : ContentPage
    {
        
        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }

        async void SubmitLogin(object sender, EventArgs args)
        {
            if (loading.IsRunning == false)
            {
                loading.IsRunning = true;

                Console.WriteLine("login action");

                List<ValidationModel> validations = new List<ValidationModel>();

                List<ConstraintsModel> constraintsModel = new List<ConstraintsModel>();

                constraintsModel.Add(new ConstraintsModel(Constants.NOT_EMPTY_DATA));

                validations.Add(new ValidationModel("Usuario:", username.Text, constraintsModel));

                validations.Add(new ValidationModel("Contraseña:", password.Text, constraintsModel));

                var validationResult = FormValidation.ValidateFields(validations, this);

                if (validationResult)
                {
                    if (await App.restServices.login(username.Text, password.Text))
                    {
                        loading.IsRunning = false;
                        await Navigation.PushAsync(new Menu());
                    }
                    else
                    {
                        loading.IsRunning = false;
                    }
                }
                else
                {

                    loading.IsRunning = false;
                }
            }            
          
        }

        void SubmitByKey(object sender, EventArgs e) {

            Console.WriteLine("password completed");

            SubmitLogin(sender,e);

        }

     
    }
}
