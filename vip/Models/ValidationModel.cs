using System;
using System.Collections.Generic;

namespace vip.Models
{
    public class ValidationModel
    {
        public ValidationModel()
        {

        }

        public ValidationModel(String name, String data, List<ConstraintsModel> constraints)
        {
            this.name = name;
            this.data = data;
            this.constraints = constraints;
        }

        public string name { get; set; }

        public string data { get; set; }

        public List<ConstraintsModel> constraints { get; set; }
    }
}
