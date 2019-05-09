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
            SetContentView(Resource.Layout.game_board);

            int playerCount = Intent.GetIntExtra("playerCount", 1);
            int fieldCount = Intent.GetIntExtra("fieldCount", 9);

            //GridLayout gridLayoutGameBoard = FindViewById<GridLayout>(Resource.Id.gridLay outGameBoard);
            //gridLayoutGameBoard.ColumnCount = 4;

            GridView gridView = FindViewById<GridView>(Resource.Id.gridViewGameBoard);
            

            List<ImageView> imageList = new List<ImageView>();
            ImageView imageView;

            for (int i = 0; i < fieldCount; i++)
            {
                imageView = new ImageView(this);
                imageView.SetImageResource(Resource.Drawable.square);

                imageView.LayoutParameters = new ViewGroup.LayoutParams(250, 250);
                imageView.SetPadding(10, 10, 10, 10);
                imageList.Add(imageView);
            }
            
            //foreach(var i in imageList)
            //{
            //    gridLayoutGameBoard.AddView(i);
            //}
 

            //List<Button> buttonList = new List<Button>(fieldCount);
            //foreach(var button in buttonList)
            //{
            //    button.Text = "Hallo";
            //}
            //Button[] buttons = new Button[fieldCount];
            ////ArrayAdapter adapter = new ArrayAdapter(this, Resource.Layout., buttons);

            //foreach (var button in buttons)
            //{
            //    button.Text = "Hallo";
            //    //gridViewGameBoard.AddView(button);
            //}

            //gridViewGameBoard.Adapter = adapter;


        }
    }
}