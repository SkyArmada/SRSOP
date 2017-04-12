using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    class Tree : Sprite
    {
        bool cut = false;
        int difficulty = 300;
        Texture2D treeTop;
        int hits = 0;

        public enum TreeType
        {
            kNormalTree,
            kOakTree,
            kCedarTree
        }

        public TreeType treeType = TreeType.kNormalTree;
        public Tree(TreeType myType)
        {
            treeType = myType;
            this._HP = 1;
            this.startHP = 1;
            switch(treeType)
            {
                case TreeType.kNormalTree:
                    difficulty = 300;
                    break;
                case TreeType.kOakTree:
                    difficulty = 600;
                    break;
                case TreeType.kCedarTree:
                    difficulty = 900;
                    break;
                    
                default:
                    difficulty = 9001;
                    break;
            }
        }

        public void getChopped(Player chopper)
        {
            Random ran = new Random();
            int randomNumber = ran.Next(0, difficulty);
            if(randomNumber == 0)
            {
                Console.WriteLine("Dead Tree");
                chopper.inventory.Add(new Item(1));
                chopper.stopAction();
                this.ReceiveDamage(1);
            }
            else
            {
                hits++;
                Console.WriteLine(hits);
            }
        }
    }
}
