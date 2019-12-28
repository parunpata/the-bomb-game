using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using TheBombGame.Model;
using TheBombGame.Service;

namespace TheBombGame
{
    [Activity(Label = "GameBoardActivity")]
    public class GameBoardActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                SetContentView(Resource.Layout.activity_game_board);

                int playerCount = Intent.GetIntExtra("playerCount", 2);
                int fieldCount = Intent.GetIntExtra("fieldCount", 9);
                int bombCount = Intent.GetIntExtra("bombCount", 1);
                int tempFieldCount = fieldCount;

                TextView textViewNumberOfFields = FindViewById<TextView>(Resource.Id.textViewNumberOfFields);
                TextView textViewRemainingFields = FindViewById<TextView>(Resource.Id.textViewRemainingFields);
                TextView textViewNumberOfBombs = FindViewById<TextView>(Resource.Id.textViewNumberOfBombs);
                TextView textViewCurrentPlayer = FindViewById<TextView>(Resource.Id.textViewCurrentPlayer);
                TextView textViewResult = FindViewById<TextView>(Resource.Id.textViewResult);
                textViewResult.Text = "";

                textViewNumberOfFields.Text = fieldCount.ToString();
                textViewRemainingFields.Text = fieldCount.ToString();
                textViewNumberOfBombs.Text = bombCount.ToString();

                GridView gridView = FindViewById<GridView>(Resource.Id.gridViewGameBoard);


                var playerService = new PlayerService();
                List<Player> players = playerService.CreatePlayerList(playerCount);
                var nextPlayer = playerService.GetStartPlayer(players);
                textViewCurrentPlayer.Text = nextPlayer.PlayerName;

                gridView.Adapter = new ImageAdapter(this, fieldCount, bombCount);

               
                gridView.ItemClick += (sender, e) =>
                {
                    ImageView image = (ImageView)e.View;

                    if ((bool)image.Tag == true)
                    {
                        textViewResult.Text = $"{nextPlayer.PlayerName} LOST";
                        textViewResult.SetTextColor(new Android.Graphics.Color(128, 0, 0));
                        EndTheGameAfterLoose(gridView);
                    }
                    else
                    {
                        tempFieldCount--;
                        textViewRemainingFields.Text = tempFieldCount.ToString();
                        image.SetImageResource(Resource.Drawable.sheep);
                        image.SetOnClickListener(null);

                        if (tempFieldCount == bombCount)
                        {
                            textViewResult.Text = "DRAW!";
                            textViewResult.SetTextColor(new Android.Graphics.Color(0, 128, 0));
                            EndTheGameAfterDraw(gridView);
                        }
                        else
                        {
                            nextPlayer = playerService.GetNextPlayer(players, nextPlayer);
                            textViewCurrentPlayer.Text = nextPlayer.PlayerName;
                        }
                    }
                };

                //Restart a game
                Button buttonRestartGame = FindViewById<Button>(Resource.Id.buttonRestartGame);
                buttonRestartGame.Click += (sender, e) =>
                {
                    tempFieldCount = fieldCount;
                    textViewRemainingFields.Text = fieldCount.ToString();
                    textViewResult.Text = "";

                    gridView.Adapter = new ImageAdapter(this, fieldCount, bombCount);
                };

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long);
            }
        }

        private void EndTheGameAfterLoose(GridView gridView)
        {
            for (int i = 0; i < gridView.Count; i++)
            {
                ImageView imageView = (ImageView)gridView.GetChildAt(i);
                if ((bool)imageView.Tag == true)
                {
                    imageView.SetImageResource(Resource.Drawable.bomb);
                }
                imageView.SetOnClickListener(null);
            }
        }

        private void EndTheGameAfterDraw(GridView gridView)
        {
            for (int i = 0; i < gridView.Count; i++)
            {
                ImageView imageView = (ImageView)gridView.GetChildAt(i);
                imageView.SetOnClickListener(null);
            }
        }
    }
}