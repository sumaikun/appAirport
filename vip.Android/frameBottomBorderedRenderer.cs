using System;
using CustomRenderer.Android;
using Xamarin.Forms.Platform.Android;
using vip;
using Xamarin.Forms;
using Android.Content;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(frameBorderBottom), typeof(frameBottomBorderedRenderer))]
namespace CustomRenderer.Android
{
    class frameBottomBorderedRenderer : FrameRenderer 
    {
        public frameBottomBorderedRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            GradientDrawable gd = new GradientDrawable();
            


        }

       
    }
}
