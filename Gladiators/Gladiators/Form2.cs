using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gladiators.Properties;


namespace Gladiators
{
    public partial class Form2 : Form
    {
        Form1 ff;
        public Form2( Form1 _ff)
        {
            InitializeComponent();

            ff = _ff;


            pictures[0] = pictureBox2;
            pictures[1] = pictureBox3;
            pictures[2] = pictureBox4;

            labels[0] = label1;
            labels[1] = label2;
            labels[2] = label3;

            Bprices[0] = button1;
            Bprices[1] = button2;
            Bprices[2] = button3;

            for (int i = 0; i <= 2; i++)
                Bprices[i].BackColor = Color.Yellow;
          // Settings.Default.Reset();
            ButtonCLickEnable[0] = (int) Settings.Default["HELMSAVE"];
            ButtonCLickEnable[1] = (int) Settings.Default["ARMSAVE"];
            ButtonCLickEnable[2] = (int) Settings.Default["SANSAVE"];
        }

        PictureBox[] pictures = new PictureBox[3];
        Label[] labels = new Label[3];
        Button[] Bprices = new Button[3];

        int[] PricesSandals = new int[3] { 50, 70, 100 };
        int[] PricesArmours = new int[3] { 150, 220, 300 };
        int[] PricesHelmets = new int[3] { 10, 30, 100 };

        int[] HPSandals = new int[3] { 10, 20, 30 };
        int[] HPArmours = new int[3] { 30, 50, 70 };
        int[] HPHelmets = new int[3] { 5, 10, 25 };
        public int[] ButtonCLickEnable=new int[3];// = new int[3]{ 5, 5, 5 }; // helmets,armor,sandals - last button clicked; to use to disable previous+ colouring

        


        int ButtonClicked = 0;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Settings.Default["HELMSAVE"] = ButtonCLickEnable[0];
            Settings.Default["ARMSAVE"] = ButtonCLickEnable[1];
            Settings.Default["SANSAVE"] = ButtonCLickEnable[2];
            Settings.Default["MONEY"] = Form1.money;
            Settings.Default["HP"] = Form1.health;
            Settings.Default.Save();

           
            this.Visible = false;
            ff.Visible = true;
        }

        public string LabelText
        {
            get
            {
                return this.label5.Text;
            }
            set
            {
                this.label5.Text = value;
            }
        }

        void LoadHelmets(object sender, EventArgs e)
        {
            for (int i = 0; i <= 2; i++)
            {
                pictures[i].Image = Image.FromFile(@"images\helmets\" + i.ToString() + ".jpg");
                labels[i].Text = "+" + HPHelmets[i].ToString() + " health";
                Bprices[i].Text = PricesHelmets[i].ToString();
                Bprices[i].BackColor = Color.Yellow;
                Bprices[i].Enabled = true;

            }
            ButtonClicked = 0;
            // disable prev buttons + colouring
            if (ButtonCLickEnable[ButtonClicked] != 5)
            {
                Bprices[ButtonCLickEnable[ButtonClicked]].BackColor = Color.Blue;
                for (int i = 0; i <= ButtonCLickEnable[ButtonClicked]; i++)
                {
                    Bprices[i].Enabled = false;
                }
            }

        }

        void LoadArmour(object sender, EventArgs e)
        {
            for (int i = 0; i <= 2; i++)
            {
                pictures[i].Image = Image.FromFile(@"images\armour\" + i.ToString() + ".jpg");
                labels[i].Text = "+" + HPArmours[i].ToString() + " health";
                Bprices[i].Text = PricesArmours[i].ToString();
                Bprices[i].BackColor = Color.Yellow;
                Bprices[i].Enabled = true;
            }
            ButtonClicked = 1;
            // disable prev buttons + colouring
            if (ButtonCLickEnable[ButtonClicked] != 5)
            {
                Bprices[ButtonCLickEnable[ButtonClicked]].BackColor = Color.Blue;
                for (int i = 0; i <= ButtonCLickEnable[ButtonClicked]; i++)
                {
                    Bprices[i].Enabled = false;
                }
            }

        }

        void LoadSandals(object sender, EventArgs e)
        {
            for (int i = 0; i <= 2; i++)
            {
                pictures[i].Image = Image.FromFile(@"images\sandals\" + i.ToString() + ".jpg");
                labels[i].Text = "+" + HPSandals[i].ToString() + " health";
                Bprices[i].Text = PricesSandals[i].ToString();
                Bprices[i].BackColor = Color.Yellow;
                Bprices[i].Enabled = true;
            }
            ButtonClicked = 2;
            // disable prev buttons + colouring
            if (ButtonCLickEnable[ButtonClicked] != 5)
            {
                Bprices[ButtonCLickEnable[ButtonClicked]].BackColor = Color.Blue;
                for (int i = 0; i <= ButtonCLickEnable[ButtonClicked]; i++)
                {
                    Bprices[i].Enabled = false;
                }
            }
        }

        void Buy(object sender, EventArgs e)
        {
            // buttonCLicked= 1,2,3 -> helmets, armour, sandals
            int[] prices = new int[3];
            int[] HP = new int[3];

            if (ButtonClicked == 0)
            {
                for (int i = 0; i <= 2; i++)
                {
                    prices[i] = PricesHelmets[i];
                    HP[i] = HPHelmets[i];
                }
            }

            if (ButtonClicked == 1)
            {
                for (int i = 0; i <= 2; i++)
                {
                    prices[i] = PricesArmours[i];
                    HP[i] = HPArmours[i];
                }
            }

            if (ButtonClicked == 2)
            {
                for (int i = 0; i <= 2; i++)
                {
                    prices[i] = PricesSandals[i];
                    HP[i] = HPSandals[i];
                }
            }

            // get number of button
            int buttonNumber = 0;
            for (int i = 0; i <= 2; i++)
            {
                if (Bprices[i] == sender)
                {
                    buttonNumber = i;
                    break;
                }
            }

            // check price
            if (Form1.money >= prices[buttonNumber])
            {
                // remove previous weapon damage ( based on colour )
                for (int i = 0; i <= 2; i++)
                {
                    if (Bprices[i].BackColor == Color.Blue)
                    {
                        Bprices[i].BackColor = Color.Yellow;
                        Form1.health -= HP[i];
                        break;
                    }
                }
                // add new weapon damage + change colour
                Form1.health += HP[buttonNumber];

                Bprices[buttonNumber].BackColor = Color.Blue;
                ButtonCLickEnable[ButtonClicked] = buttonNumber;
                // substract money
                Form1.money -= prices[buttonNumber];
                // change label showing money left
                label5.Text = Form1.money.ToString();

                // make previous buttons unavailable
                for (int i = 0; i <= buttonNumber; i++)
                {
                    Bprices[i].Enabled = false;
                }
            }
            else
                MessageBox.Show("You don't have enough money to buy this item.");

        }


    }

}
