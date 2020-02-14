using System;
using vip.Config;

namespace vip.Models
{
    public class RoomModel
    {
        public RoomModel()
        {

        }

        public RoomModel(string title, string picture, string dragonPassId, string description)
        {
            this.title = title;
            this.dragonPassId = dragonPassId;
            picture = picture.Replace("localhost", Constants.baseIp);
            this.picture = new Uri(string.Format(picture, string.Empty));
            this.description = description;
        }

        public string title { get; set; }

        public string dragonPassId { get; set; }

        public Uri picture { get; set; }

        public string description { get; set; }
    }
}
