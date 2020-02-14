using System;
using System.Collections.Generic;
using vip.Config;
using vip.Models;
using Xamarin.Forms;

namespace vip.Helpers
{
    public static class FormValidation
    {
        public static bool ValidateFields(List<ValidationModel> validations , Page page) {

            string report = "";

            Console.WriteLine("validate function");

            foreach (ValidationModel validation in validations)
            {
                if (validation.name != null)
                {

                    foreach (ConstraintsModel constraint in validation.constraints)
                    {
                        if (constraint.type != null && constraint.format == null)
                        {
                            if (constraint.type == Constants.NOT_EMPTY_DATA)
                            {                               
                                if (validation.data == null)
                                {
                                    report += " "+validation.name + " No puede ser vacio " + Environment.NewLine;
                                }
                            }

                        }
                    }

                }
               
                
            }

            if (report != "")
            {
                page.DisplayAlert("espera", report, "ok");
            }



            return report != ""  ? false : true;         

        }
    }
}
