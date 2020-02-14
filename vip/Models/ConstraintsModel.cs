using System;
namespace vip.Models
{
    public class ConstraintsModel
    {
        public ConstraintsModel()
        {

        }

        public ConstraintsModel(string type)
        {
            this.type = type;            
        }

        public ConstraintsModel(string type, string format)
        {
            this.type = type;
            this.format = format;
        }

        public string type { get; set; }

        public string format { get; set; }
    }
}
