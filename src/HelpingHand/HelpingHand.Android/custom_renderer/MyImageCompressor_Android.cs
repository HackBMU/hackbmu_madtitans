using static HelpingHand.NewEntry;
using Android.App;
using Android.Graphics;
using HelpingHand.Droid.custom_renderer;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(MyImageCompressor_Android))]
namespace HelpingHand.Droid.custom_renderer
{
    public class MyImageCompressor_Android : MyImageCompressor
    {
        public MyImageCompressor_Android() { }

        public string ImageCompressor(byte[] bitmapBytes)
        {
            Console.WriteLine("Executing This Method Now!");
            Bitmap bitmap = BitmapFactory.DecodeByteArray(bitmapBytes, 0, bitmapBytes.Length);
            
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(bitmap, 300, 300, false);
            var stream = new System.IO.MemoryStream();

            resizedImage.Compress(Android.Graphics.Bitmap.CompressFormat.Webp, 100, stream);
            byte[] imageBytes = stream.ToArray();
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;

        }
    }
}