using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Note_Assessment
{
    [Activity(Label = "notepad", MainLauncher = true, NoHistory = true, Theme="@style/Theme.Splash",Icon="@drawable/icon")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Thread.Sleep(100);

            StartActivity(typeof(MainActivity));
        }
    }
}