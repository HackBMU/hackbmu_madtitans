using Xamarin.Forms;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Rox;
using Android.Content;
using ButtonCircle.FormsPlugin.Droid;
using HelpingHand;
using ImageCircle.Forms.Plugin.Droid;
using ImageCircle.Forms.Plugin.Abstractions;

namespace HelpingHand.Droid
{
    [Activity(Label = "HelpingHand", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            ButtonCircleRenderer.Init();
            global::Xamarin.FormsMaps.Init(this, bundle);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            switch (requestCode)
            {
                case 1:
                    {
                        CameraProvider.OnCameraResult(resultCode);
                        break;
                    }
            }
        }

    }
}

