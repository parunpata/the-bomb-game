﻿using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using TheBombGame.Model;

namespace TheBombGame
{
    internal class ImageAdapter : BaseAdapter
    {
        private readonly Context context;
        private List<Field> gameFields = new List<Field>();

        public ImageAdapter(Context c, int fieldCount, int bombCount)
        {
            context = c;

            for (int i = 0; i < fieldCount; i++)
            {
                var field = new Field
                {
                    FieldId = i,
                    ResourceId = Resource.Drawable.grass,
                    IsBomb = false
                };

                gameFields.Add(field);
            }

            List<int> bombPositions = new List<int>();
            Random random = new Random();
            while (bombCount > 0)
            {
                int randomNumber = random.Next(0, fieldCount);
                if (!bombPositions.Contains(randomNumber))
                {
                    bombCount--;
                    bombPositions.Add(randomNumber);
                }
            }

            foreach (var field in gameFields)
            {
                if (bombPositions.Contains(field.FieldId))
                {
                    field.IsBomb = true;
                }
            }
        }

        public override int Count
        {
            get { return gameFields.Count; }
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
            {
                imageView = new ImageView(context);
                imageView.LayoutParameters = new GridView.LayoutParams(250, 250);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.SetPadding(15, 15, 15, 15);

                if (gameFields[position].IsBomb == true)
                {
                    imageView.Tag = true;
                }
                imageView.SetImageResource(gameFields[position].ResourceId);
            }
            else
            {
                imageView = (ImageView)convertView;
            }

            return imageView;
        }
    }
}