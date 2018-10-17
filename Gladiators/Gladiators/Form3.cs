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
    public partial class Form3 : Form
    {
        Form1 ff;
        public Form3(Form1 _ff)
        {
            InitializeComponent();
            ff = _ff;
            LoadWeps();


        }

        PictureBox[] Pweapons = new PictureBox[3];
        Label[] Ldamage = new Label[3];
        Button[] Bprices = new Button[3];
        int[] damages = new int[3] { 30, 60, 80 };
        int[] prices = new int[3] { 150, 250, 300 };


        public int LastClicked = (int) Settings.Default["WEPSAVE"];

        void LoadWeps()
        {

            Pweapons[0] = pictureBox2;
            Pweapons[1] = pictureBox3;
            Pweapons[2] = pictureBox4;

            Ldamage[0] = label1;
            Ldamage[1] = label2;
            Ldamage[2] = label3;

            Bprices[0] = button1;
            Bprices[1] = button2;
            Bprices[2] = button3;

            for (int i = 0; i <= 2; i++)
            {
                Pweapons[i].Image = Image.FromFile(@"images\weapons\" + i.ToString() + ".jpg");
                Ldamage[i].Text = "+" + damages[i].ToString() + " damage";
                Bprices[i].Text = prices[i].ToString();
                Bprices[i].BackColor = Color.Yellow;
                Bprices[i].Click += Buy;
            }

            if(LastClicked!=5){
                for (int i = 0; i <= LastClicked; i++ )
                {
                    Bprices[i].Enabled = false;
                }
                Bprices[LastClicked].BackColor=Color.Blue;
            }
        }


        void Buy(object sender, EventArgs e)
        {
            // get number of button
            int buttonNumber=0;
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
                        Form1.damage -= damages[i];
                        break;
                    }
                }
                // add new weapon damage + change colour
                Form1.damage += damages[buttonNumber];
                Bprices[buttonNumber].BackColor = Color.Blue;
                // substract money
                Form1.money -= prices[buttonNumber];
                // change label showing money left
                label5.Text = Form1.money.ToString();
                // make previous buttons unavailable
                LastClicked = buttonNumber;
                for (int i = 0; i <= buttonNumber; i++)
                {
                    Bprices[i].Enabled = false;
                }
            }
            else
                MessageBox.Show("You don't have enough money to buy this item.");

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Settings.Default["WEPSAVE"] = LastClicked;
            Settings.Default["MONEY"] = Form1.money;
            Settings.Default["DMG"] = Form1.damage;
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

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 2; i++)
            {
                Pweapons[i].Visible = true;
                Bprices[i].Visible = true;
                Ldamage[i].Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            textBox1.Visible = true;
            button6.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "hardpassword123")
            {
                Form1.money += 10000;
                label5.Text = Form1.money.ToString();
            }

                label6.Visible = false;
                textBox1.Visible = false;
                button6.Visible = false;  
        }




    }
}
