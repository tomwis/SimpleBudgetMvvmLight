﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Droid;
using GalaSoft.MvvmLight.Views;

namespace SimpleBudgetMvvmLight.Droid
{
    [Activity(Label = "SimpleBudgetMvvmLight", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            CachedImageRenderer.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            App.Locator.SetDialogService(new DialogService());
        }
    }
}

