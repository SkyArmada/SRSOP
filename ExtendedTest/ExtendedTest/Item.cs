﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExtendedTest
{
    class Item
    {
        public  String _Name;
        public double _Weight;
        public String _Image;
        public int _SaleValue;

        public enum ItemType
        {
            ItemLog, ItemNone, ItemError
        }

        public ItemType myType = ItemType.ItemNone;
        

        public void Draw(SpriteBatch spritebatch, Vector2 Pos, Texture2D tex)
        {
            spritebatch.Draw(tex, Pos, Color.White);
        }
    }
}
