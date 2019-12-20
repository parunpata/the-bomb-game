﻿using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;

namespace TheBombGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected AdView mAdView;
        protected InterstitialAd mInterstitialAd;
        protected Button mLoadInterstitialButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            #region
            mAdView = FindViewById<AdView>(Resource.Id.adView);
            var adRequest = new AdRequest.Builder().Build();
            mAdView.LoadAd(adRequest);

            //mInterstitialAd = new InterstitialAd(this);
            //mInterstitialAd.AdUnitId = GetString(Resource.String.test_interstitial_ad_unit_id);

            //mInterstitialAd.AdListener = new AdListener(this);

            //mLoadInterstitialButton = FindViewById<Button>(Resource.Id.load_interstitial_button);
            //mLoadInterstitialButton.SetOnClickListener(new OnClickListener(this));
            #endregion

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += (sender, e) =>
            //{
            //    View view = (View)sender;
            //    Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
            //        .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
            //};

            var playerCount = 2;
            RadioButton radioButtonOnePlayer = FindViewById<RadioButton>(Resource.Id.radioButtonOnePlayer);
            RadioButton radioButtonTwoPlayer = FindViewById<RadioButton>(Resource.Id.radioButtonTwoPlayer);
            RadioButton radioButtonThreePlayer = FindViewById<RadioButton>(Resource.Id.radioButtonThreePlayer);
            RadioButton radioButtonFourPlayer = FindViewById<RadioButton>(Resource.Id.radioButtonFourPlayer);
            radioButtonOnePlayer.Click += (sender, e) => playerCount = Convert.ToInt16(radioButtonOnePlayer.Text);
            radioButtonTwoPlayer.Click += (sender, e) => playerCount = Convert.ToInt16(radioButtonTwoPlayer.Text);
            radioButtonThreePlayer.Click += (sender, e) => playerCount = Convert.ToInt16(radioButtonThreePlayer.Text);
            radioButtonFourPlayer.Click += (sender, e) => playerCount = Convert.ToInt16(radioButtonFourPlayer.Text);

            NumberPicker npFieldCount = FindViewById<NumberPicker>(Resource.Id.numberPickerFieldCount);
            npFieldCount.MinValue = 2;
            npFieldCount.MaxValue = 32;
            int fieldCount = npFieldCount.Value;
            npFieldCount.ValueChanged += (sender, e) => fieldCount = e.NewVal;

            NumberPicker npBombCount = FindViewById<NumberPicker>(Resource.Id.numberPickerBombCount);
            npBombCount.MinValue = 1;
            npBombCount.MaxValue = npFieldCount.MaxValue - 1;
            int bombCount = npBombCount.Value;
            npBombCount.ValueChanged += (sender, e) => bombCount = e.NewVal;

            Button buttonStart = FindViewById<Button>(Resource.Id.buttonStart);
            buttonStart.Click += (sender, e) =>
            {
                if (fieldCount > bombCount)
                {
                    var activity = new Intent(this, typeof(GameBoardActivity));
                    activity.PutExtra("playerCount", playerCount);
                    activity.PutExtra("fieldCount", fieldCount);
                    activity.PutExtra("bombCount", bombCount);
                    StartActivity(activity);
                }
                else
                {
                    Toast.MakeText(this, Resource.String.too_much_bombs, ToastLength.Short).Show();
                }
            };
        }
        protected override void OnPause()
        {
            if (mAdView != null)
            {
                mAdView.Pause();
            }
            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (mAdView != null)
            {
                mAdView.Resume();
            }
        }

        protected override void OnDestroy()
        {
            if (mAdView != null)
            {
                mAdView.Destroy();
            }
            base.OnDestroy();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            //if (id == Resource.Id.action_settings)
            //{
            //    return true;
            //}
            if (id == Resource.Id.action_rate)
            {
                OpenPlayStoreLink();
            }
            if (id == Resource.Id.action_about)
            {
                var activity = new Intent(this, typeof(AboutActivity));
                StartActivity(activity);
            }

            return base.OnOptionsItemSelected(item);
        }

        private void OpenPlayStoreLink()
        {
            var a = PackageName;
            var activity = new Intent(Intent.ActionView);
            try
            {
                activity.SetData(Android.Net.Uri.Parse("market://details?id=com.twentycode.thebombgame"));
                StartActivity(activity);
            }
            catch (Android.Content.ActivityNotFoundException anfe)
            {
                Toast.MakeText(this, anfe.Message, ToastLength.Short);
                activity.SetData(Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=com.twentycode.thebombgame"));
                StartActivity(activity);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}