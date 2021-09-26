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
            // Base class for characters
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
                // Death check.
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
                //Attack method to be filled in later.
                public virtual void attack(int charactertarget)
                {

                }
                //Distance Calc
                private int distanceto(int target, int charpos)
                {
                    int distance;
                    distance = target - charpos;
                    return distance;

                }
                //Range check
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
                //movement using enum
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
                //return move to be overridden
                public abstract Movement returnmove(Movement move = 0);


                //Tostring to be overridden.
                public abstract override string ToString();
            }
            //Enemy Class
            abstract class Enemy : Character
            {
                //Enemy Constructor
                public void EnemyStats(int posy, int posx, int StartHP, string symbol, int attack)
                {
                    Tilex = posx;
                    Tiley = posy;
                    HP = StartHP;
                    MAXHP = StartHP;
                    Damage = attack;

                }
                //Overridden String
                public override string ToString()
                {
                    return "EnemyClassName at " + "[" + Tilex + ',' + Tiley + "]" + "(" + Damage + ")";
                }


            }
            // Goblin Subclass
            class Goblin : Enemy
            {    //Constructor
                public void GoblinStats(int x, int y)
                {
                    Tilex = x;
                    Tiley = y;
                    HP = 10;
                    Damage = 1;


                }
                //random movement
                public override Movement returnmove(Movement move = Movement.Up)
                {
                    Random r = new Random();
                    int num = r.Next(4);
                    return (Movement)num;
                }
            }
            //Hero Subclass
            class Hero : Character
            {
                public void HeroStats(int x, int y, int hp)
                {
                    position(x, y);
                    HP = hp;
                    Damage = 2;

                }
                //Movement
                public override Movement returnmove(Movement move = Movement.Up)
                {
                    return (Movement)move;
                }
                //Tostring overrider
                public override string ToString()
                {
                    return "Player Stats \r\n"
                        + "HP: " + HP + "/" + MAXHP + "\r\n" + "Damage:" + "(" + Damage + ")\r\n" + "[" + Tilex + ',' + Tiley + "]";
                }
            }
            public class Maphelp
            {
                private char[,] maptiles;
                char HeroIcon = '@';
                private char[,] Enemy;
                int mapwidth;
                int mapheight;
                char[,] enemyArray;
                Random mappy = new Random();

                public void mapmaking(int maxwidth, int minwidth, int minheight, int maxheight, int numberofenemies)
                {
                    int mapheight = mappy.Next(minheight, maxheight);
                    int mapwidth = mappy.Next(minwidth, maxwidth);
                    maptiles[mapwidth, mapheight] = 'v';
                    enemyArray[mapwidth, mapheight] = 'v';

                }
                private void create(int mapheight, int mapwidth, int Enemynumb)
                {
                    int charposy = mappy.Next(1, mapheight);
                    int charposx = mappy.Next(1, mapwidth);
                    maptiles[charposx, charposy] = '@';
                    for (int i = 0; i < Enemynumb; i++)
                    {
                        int enemyposy = mappy.Next(1, mapheight);
                        int enemyposx = mappy.Next(1, mapwidth);
                        maptiles[enemyposy, enemyposx] = '#';

                    }

                }
                private void createtiles(Tiletypes type, int gold, int weapon)
                {
                    for (int i = 0; i < gold; i++)
                    {
                        int charposy = mappy.Next(1, mapheight);
                        int charposx = mappy.Next(1, mapwidth);
                        if (maptiles[charposx, charposy] == 'v')
                        {
                            maptiles[charposx, charposy] = 'G';

                        }
                        else
                        {
                            i = i - 1;
                        }

                    }
                    for (int i = 0; i < weapon; i++)
                    {
                        int charposy = mappy.Next(1, mapheight);
                        int charposx = mappy.Next(1, mapwidth);
                        if (maptiles[charposx, charposy] == 'v')
                        {
                            maptiles[charposx, charposy] = 'W';

                        }
                        else
                        {
                            i = i - 1;
                        }

                    }
                }
                class gameEngine
                {
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        { Random mappy = new Random();
            int maxw = mappy.Next(20);
            int minw = mappy.Next(15);
            int maxh = mappy.Next(20);
            int minh = mappy.Next(15);
            tile.Maphelp make = new tile.Maphelp();
            make.mapmaking(maxw, minw, maxh, minh, 2);
        }

        public void StartButton_Click(object sender, EventArgs e) 
        {
            
        }
    }
    }
    



