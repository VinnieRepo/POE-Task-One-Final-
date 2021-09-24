using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POE_Task_One__Final_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    //Base Class for Tiles
    abstract class tile


    {
        protected int Tilex;
        protected int Tiley;

        public void valuex(int value)
        {
            Tilex = value;
        }
        public void valuey(int value)
        {
            Tiley = value;
        }
        //Tiletype and Enumerator


        public enum Tiletypes
        {
            Hero,
            Enemy,
            Gold,
            Weapon
        }


        // Default values class
        public class defaults : tile
        {
            public void Basevalues(int xval, int yval, int tiletype)
            {
                Tilex = xval;
                Tiley = yval;
                var Tiletype = (Tiletypes)tiletype;



            }

        }
        //Obstacles Classs
        public class obstacles : defaults
        {
            public void Places(int xval, int yval, int tiletype)
            {
                Basevalues(xval, yval, tiletype);
            }



        }
        // Empty Tiles class.
        public class emptyTiles : defaults
        {
            public void Places(int xval, int yval, int tiletype)
            {
                Basevalues(xval, yval, tiletype);
            }


        }
        // Base class doe character
        abstract class Character : tile
        {
            protected int HP;
            protected int MAXHP;
            protected int Damage;
            protected int[,] vision;
            public enum Movement
            { Up, Down, Left, Right, NoMovement };

            //Position of the character.
            protected void position(int x, int y)
            {
                Tilex = x;
                Tiley = y;
            }
            public bool isdead(int value)
            {
                if (value == 1)
                {
                    return true;
                }

                else
                {
                    return false;
                }

            }
            public virtual void attack(int charactertarget)
            {

            }
            private int distanceto(int target, int charpos)
            {
                int distance;
                distance = target - charpos;
                return distance;

            }
            private bool CheckRange(int target, int charpos)
            {
                int distance = distanceto(target, charpos);
                if (distance <= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public void CharacterMove(int direction, int moveamount)
            {
                var whichway = (Movement)direction;
                switch (whichway)
                {
                    case Movement.Up:
                        Tiley = Tiley - 1;
                        break;
                    case Movement.Down:
                        Tiley = Tiley + 1;
                        break;
                    case Movement.Left:
                        Tilex = Tilex - 1;
                        break;
                    case Movement.Right:
                        Tilex = Tilex + 1;
                        break;

                }

            }
            public abstract override string ToString();
        }
        abstract class Enemy : Character
        {
            Random rand = new Random();
            public void EnemyStats(int posy, int posx, int StartHP, string symbol)
            {
                Tilex = posx;
                Tiley = posy;
                HP = StartHP;
                MAXHP = StartHP;
                public override string ToString()
            {
                throw new NotImplementedException();


            }


        }
        }
    }
}

