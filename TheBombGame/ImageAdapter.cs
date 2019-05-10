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
using Java.Lang;

namespace TheBombGame
{
    class ImageAdapter : BaseAdapter
    {
        Context context;
        List<int> squares = new List<int>();
        public ImageAdapter(Context c, int fieldCount)
        {
            context = c;

            for (int i = 0; i < fieldCount; i++)
            {
                squares.Add(Resource.Drawable.square);
            }
         }

        public override int Count
        {
            get { return squares.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView imageView;
            
            if (convertView == null)
            {  // if it's not recycled, initialize some attributes
                imageView = new ImageView(context);
                imageView.LayoutParameters = new GridView.LayoutParams(250, 250);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.SetPadding(8, 8, 8, 8);
            }
            else
            {
                imageView = (ImageView)convertView;
            }

            imageView.SetImageResource(squares[position]);
            return imageView;
        }

    }
}