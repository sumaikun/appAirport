using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using CustomRenderer.Android;
using Android.Content;
using vip;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Content.Res;

[assembly: ExportRenderer(typeof(EmptyEntry), typeof(MyEntryRenderer))]
namespace CustomRenderer.Android
{
	class MyEntryRenderer : EntryRenderer
	{
		public MyEntryRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				GradientDrawable gd = new GradientDrawable();
				gd.SetColor(global::Android.Graphics.Color.Transparent);
				this.Control.SetBackgroundDrawable(gd);
				this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
				Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
			}
		}
	}
}
