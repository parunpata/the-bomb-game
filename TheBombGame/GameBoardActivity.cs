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

            int playerCount = Intent.GetIntExtra("playerCount", 1);
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

            List<Player> players = new List<Player>(playerCount);
            Player nextPlayer = null;
            for(int i = 1; i <= players.Capacity; i++)
            {
                players.Add(new Player()
                {
                    PlayerId = i,
                    PlayerName = $"Player {i}",
                    Turn = false
                });
            }

            foreach(var player in players)
            {
                if (player.PlayerId == 1)
                {
                    player.Turn = true;
                    textViewCurrentPlayer.Text = player.PlayerName;
                    nextPlayer = player;
                }   
            }

            GridView gridView = FindViewById<GridView>(Resource.Id.gridViewGameBoard);
            gridView.Adapter = new ImageAdapter(this, fieldCount, bombCount);

            gridView.ItemClick += (sender, e) =>
            {
                ImageView image = (ImageView)e.View;

                if ((bool)image.Tag == true)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Lost");
                    alert.SetMessage("You Lost");

                    Dialog dialog = alert.Create();
                    dialog.Show();
                }
                image.SetImageResource(Resource.Drawable.square);
                image.SetBackgroundColor(new Android.Graphics.Color(0, 255, 0));

                var currentPlayer = players.Where(s => s.PlayerId == nextPlayer.PlayerId).FirstOrDefault();

                if(currentPlayer.PlayerId == players.Capacity)
                {
                    nextPlayer = players[0];
                }
                else
                {
                    nextPlayer = players[currentPlayer.PlayerId];
                }

                textViewCurrentPlayer.Text = nextPlayer.PlayerName;
            };
            
        }
    }
}