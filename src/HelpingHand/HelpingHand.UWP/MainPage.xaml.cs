using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static HelpingHand.NewEntry;

namespace HelpingHand.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsMaps.Init();

            LoadApplication(new HelpingHand.App());
        }
    }

    public class MyImageCompressor_Android : MyImageCompressor
    {
        public MyImageCompressor_Android() { }

        public string ImageCompressor(byte[] bitmapBytes)
        {
            return "asdsa";
        }
    }
}
