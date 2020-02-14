using System;
namespace vip.Config
{
    public static class Constants
    {
        // The iOS simulator can connect to localhost. However, Android emulators must use the 10.0.2.2 special alias to your host loopback interface.
        //public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";
        public static string baseIp = "192.168.0.28";
        public static string BaseAddress = "http://192.168.0.28:8089";
        public static string LoginUrl = BaseAddress + "/authenticate";
        public static string queryCardUrl = BaseAddress + "/pass/validateCard";
        public static string orderEntryUrl = BaseAddress + "/pass/orderEntry";
        public static string cancelEntryUrl = BaseAddress + "/pass/cancelEntry";
        public static string getRoomsUrl = BaseAddress + "/rooms";

        //validations
        public static string NOT_EMPTY_DATA = "NOT_EMPTY_DATA";

        //keys
        public static string appKey = "c061c4ce6e73d3053ec989f86809aeb0";

        //pass default
        public static string defaultLounge = "N00012";
    }
}
