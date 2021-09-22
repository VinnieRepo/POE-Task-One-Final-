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


        public string Tiletype;
        public string[] Tiletypes = { "Hero", "Enemy", "Gold", "Weapon" };
            
        
        // Default values class
        public class defaults : tile
        {
            public void Basevalues(int xval, int yval, int tiletype)
            {
                Tilex = xval;
                Tiley = yval;
                Tiletype = Tiletypes[tiletype];



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
        abstract class character : tile
        {
            protected int HP;
            protected int MAXHP;
            protected int Damage;
            protected int[,] vision;
            public string[] Movement = { "Up", "Down", "Left", "Right", "No Movement" };

            //Position of the character.
            protected void position(int x, int y)
            {
                Tilex = x;
                Tiley = y;
            }
            public bool isdead()
            {
                
                return true;

            }
            public virtual void attack(int charactertarget)
            {

            }
            private int distanceto(int target)
            {
                if (target = 0)
                {

                }
                return distance;
            }
           

            
        }
        }
    }
}
