using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using vip.Models;
using Xamarin.Forms;

namespace vip
{
    public class RoomsViewModel : ReactiveObject
    {      

        private JArray queryResult;

        private ObservableCollection<RoomModel> _rooms;

        public ObservableCollection<RoomModel> Rooms {

            get => _rooms;
            set => this.RaiseAndSetIfChanged(ref _rooms, value);
        }

        private RoomModel _selectedRoom;

        public RoomModel SelectedRoom {

            get => _selectedRoom;
            set => this.RaiseAndSetIfChanged(ref _selectedRoom, value);
        }

        private Boolean _loading;

        public Boolean Loading {
            get => _loading;
            set => this.RaiseAndSetIfChanged(ref _loading, value);
        }

        private string _filterText;

        public string FilterText
        {
            get => _filterText;
            //Notify when property user name changes
            set => this.RaiseAndSetIfChanged(ref _filterText, value);
        }

        public Func<Task> Appearing { get; }

        public ReactiveCommand<Unit, Unit> FilterCommand { get; }

        public ReactiveCommand<Unit, Task> SelectLoungeCommand { get; }

        public RoomsViewModel()
        {
            Loading = true;

            //Appearing += async () => await setup();

            //Appearing();

            Loading = false;

            FilterCommand = ReactiveCommand.Create(
            () => {

                Console.WriteLine("on view model");

                if (FilterText.Length == 0)
                {
                    setList();
                }
                else
                {
                    Rooms = new ObservableCollection<RoomModel>();

                    foreach (JObject jObject in queryResult)
                    {
                        var title = (string)jObject.GetValue("name");
                        var picture = (string)jObject.GetValue("picture");
                        var dragonpassId = (string)jObject.GetValue("dragonpassId");
                        var description = (string)jObject.GetValue("description");
                        //x => x.title.Contains(FilterText)
                        if (title.ToLower().Contains(FilterText.ToLower()))
                        {
                            Rooms.Add(new RoomModel(title, picture, dragonpassId, description));
                        }
                    }

                }
                
            });

            SelectLoungeCommand = ReactiveCommand.Create(
             async () => {

                //Console.WriteLine(SelectedRoom.title);
                 
                var answer = await Application.Current.MainPage.DisplayAlert("", "¿Desea seleccionar '"+ SelectedRoom.title + "' para realizar las lecturas?", "Si", "No");
                if (answer)
                {
                     Console.WriteLine("lounge selected");

                     Application.Current.Properties["loungeTitle"] = SelectedRoom.title;
                     Application.Current.Properties["loungeId"] = SelectedRoom.dragonPassId;
                     await Application.Current.SavePropertiesAsync();

                 }                

            });



        }

        public async Task setup() {

            queryResult = await App.restServices.GetRooms();
            setList();
        }

        void setList() {

            Rooms = new ObservableCollection<RoomModel>();

            if (queryResult != null)
            {


                foreach (JObject jObject in queryResult)
                {

                    var title = (string)jObject.GetValue("name");
                    var picture = (string)jObject.GetValue("picture");
                    var dragonpassId = (string)jObject.GetValue("dragonpassId");
                    var description = (string)jObject.GetValue("description");

                    Console.WriteLine(title);
                    Console.WriteLine(picture);
                    Console.WriteLine(description);
                    Rooms.Add(new RoomModel(title, picture, dragonpassId, description));

                }

                Console.WriteLine(Rooms);
            }


        }

       
    }
}
