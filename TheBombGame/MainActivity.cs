using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace TheBombGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += (sender, e) =>
            {
                View view = (View)sender;
                Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
            };

            var playerCount = 1;
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
                var activity = new Intent(this, typeof(GameBoardActivity));
                activity.PutExtra("playerCount", playerCount);
                activity.PutExtra("fieldCount", fieldCount);
                activity.PutExtra("bombCount", bombCount);
                StartActivity(activity);
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}

