using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
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

            int playerCount = Intent.GetIntExtra("playerCount", 2);
            int fieldCount = Intent.GetIntExtra("fieldCount", 9);
            int bombCount = Intent.GetIntExtra("bombCount", 1);

            TextView textViewTotalNumberOfFields = FindViewById<TextView>(Resource.Id.textViewTotalNumberOfFields);
            TextView textViewCurrentNumberOfFields = FindViewById<TextView>(Resource.Id.textViewCurrentNumberOfFields);
            TextView textViewTotalNumberOfBombs = FindViewById<TextView>(Resource.Id.textViewTotalNumberOfBombs);
            TextView textViewCurrentNumberOfBombs = FindViewById<TextView>(Resource.Id.textViewCurrentNumberOfBombs);
            TextView textViewCurrentPlayer = FindViewById<TextView>(Resource.Id.textViewCurrentPlayer);

            textViewTotalNumberOfFields.Text = fieldCount.ToString();
            textViewCurrentNumberOfFields.Text = fieldCount.ToString();
            textViewTotalNumberOfBombs.Text = bombCount.ToString();
            textViewCurrentNumberOfBombs.Text = bombCount.ToString();

            var playerService = new PlayerService();
            List<Player> players = playerService.CreatePlayerList(playerCount);
            var nextPlayer = playerService.GetStartPlayer(players);
            textViewCurrentPlayer.Text = nextPlayer.PlayerName;

            GridView gridView = FindViewById<GridView>(Resource.Id.gridViewGameBoard);
            gridView.Adapter = new ImageAdapter(this, fieldCount, bombCount);

            gridView.ItemClick += (sender, e) =>
            {
                ImageView image = (ImageView)e.View;

                if ((bool)image.Tag == true)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Lost");
                    alert.SetMessage($" {nextPlayer.PlayerName} Lost");

                    Dialog dialog = alert.Create();
                    dialog.Show();
                }
                image.SetImageResource(Resource.Drawable.square);
                image.SetBackgroundColor(new Android.Graphics.Color(0, 255, 0));

                nextPlayer = playerService.GetNextPlayer(players, nextPlayer);
                textViewCurrentPlayer.Text = nextPlayer.PlayerName;
            };

        }
    }
}