using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;


namespace vip
{
    public partial class Rooms : ReactiveContentPage<RoomsViewModel>
    {
        public Rooms()
        {
            InitializeComponent();

            ViewModel = new RoomsViewModel();

            NavigationPage.SetHasBackButton(this, false);

            this.WhenActivated(disposable =>
            {
                //this.OneWayBind(ViewModel, vm => vm.Rooms, v => v.MyListView.ItemsSource)
                //.DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.FilterCommand, v => v.filterEntry, "Completed")
                .DisposeWith(disposable);

                this.BindCommand(ViewModel, vm => vm.SelectLoungeCommand, v => v.MyListView, "ItemTapped")
                .DisposeWith(disposable);

                /*var filterEntryCompleted = Observable.FromEventPattern(
                    x => filterEntry.Completed += x.Invoke,
                    x => filterEntry.Completed -= x.Invoke);

                filterEntryCompleted.InvokeCommand(this, x => x.ViewModel.FilterCommand).DisposeWith(disposable);*/

            });

        }

        protected override void OnAppearing()
        {
            ViewModel = new RoomsViewModel();
            ViewModel.setup();
        }
    }
}
 