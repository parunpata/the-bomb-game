using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using TheBombGame.Model;
using TheBombGame.Service;

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
            TextView textViewCurrentPlayer = FindViewById<TextView>(Resource.Id.textViewCurrentPlayer);
            TextView textViewLoser = FindViewById<TextView>(Resource.Id.textViewLoser);

            LinearLayout linearLayoutGameField = FindViewById<LinearLayout>(Resource.Id.linearLayoutGameField);

            textViewTotalNumberOfFields.Text = fieldCount.ToString();
            textViewCurrentNumberOfFields.Text = fieldCount.ToString();
            textViewTotalNumberOfBombs.Text = bombCount.ToString();

            var playerService = new PlayerService();
            List<Player> players = playerService.CreatePlayerList(playerCount);
            var nextPlayer = playerService.GetStartPlayer(players);
            textViewCurrentPlayer.Text = nextPlayer.PlayerName;

            GridView gridView = FindViewById<GridView>(Resource.Id.gridViewGameBoard);
            gridView.Adapter = new ImageAdapter(this, fieldCount, bombCount);

            gridView.ItemClick += (sender, e) =>
            {
                ImageView image = (ImageView)e.View;
                View view = (View)sender;
                if ((bool)image.Tag == true)
                {
                    
                    textViewLoser.Text = $"{nextPlayer.PlayerName}";
                    Snackbar.Make(view, "Replace with your own action", Snackbar.LengthIndefinite)
                   .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
                    linearLayoutGameField.Activated = false;
                }
                else
                {
                    fieldCount--;
                    textViewCurrentNumberOfFields.Text = fieldCount.ToString();
                    //image.SetImageResource(Resource.Drawable.square);
                    //image.SetBackgroundColor(new Android.Graphics.Color(0, 255, 0));
                    image.Visibility = Android.Views.ViewStates.Invisible;
                    nextPlayer = playerService.GetNextPlayer(players, nextPlayer);
                    textViewCurrentPlayer.Text = nextPlayer.PlayerName;
                }




              
            };

        }
    }
}