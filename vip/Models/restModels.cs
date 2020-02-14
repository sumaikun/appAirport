using System;
namespace vip.Models
{
    public class restModels
    {
        public restModels()
        {
        }

        public class queryCard
        {
            public queryCard()
            {}

            public queryCard(string cardno, string loungeid)
            {
                this.cardno = cardno;
                this.loungeid = loungeid;
            }

            public string cardno { get; set; }

            public string loungeid { get; set; }
        }

        public class orderEntry
        {
            public orderEntry()
            { }

            public orderEntry(string cardno, string loungeid, string adultcount, string childcount)
            {
                this.cardno = cardno;
                this.loungeid = loungeid;
                this.adultcount = adultcount;
                this.childcount = childcount;
            }

            public string cardno { get; set; }

            public string loungeid { get; set; }

            public string adultcount { get; set; }

            public string childcount { get; set; }
        }

        public class cancelEntry
        {
            public cancelEntry()
            { }

            public cancelEntry(string cardno)
            {
                this.cardno = cardno;                               
            }

            public string cardno { get; set; }

            //public string loungeid { get; set; }

            //public string transsequno { get; set; }

        }
    }
}
