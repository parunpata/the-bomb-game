using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TheBombGame
{
    [Activity(Label = "GameBoardActivity")]
    public class GameBoardActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_game_board);

            int playerCount = Intent.GetIntExtra("playerCount", 1);
            int fieldCount = Intent.GetIntExtra("fieldCount", 9);
            int bombCount = Intent.GetIntExtra("bombCount", 1);

            GridView gridView = FindViewById<GridView>(Resource.Id.gridViewGameBoard);
            gridView.Adapter = new ImageAdapter(this, fieldCount, bombCount);
            
        }
    }
}