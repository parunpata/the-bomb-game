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
            textViewLoser.Text = "";
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
                
                if ((bool)image.Tag == true)
                {
                    
                    textViewLoser.Text = $"{nextPlayer.PlayerName} LOST";

                    for (int i = 0; i < gridView.Count; i++)
                    {
                        ImageView imageView = (ImageView)gridView.GetChildAt(i);
                        if ((bool)imageView.Tag == true)
                        {
                            imageView.SetBackgroundColor(new Android.Graphics.Color(255, 0, 0));
                            
                        }
                        imageView.SetOnClickListener(null);
                    }                  
                }
                else
                {
                    fieldCount--;
                    textViewCurrentNumberOfFields.Text = fieldCount.ToString();
                    image.Visibility = Android.Views.ViewStates.Invisible;
                    nextPlayer = playerService.GetNextPlayer(players, nextPlayer);
                    textViewCurrentPlayer.Text = nextPlayer.PlayerName;
                }
            };
        }
    }
}