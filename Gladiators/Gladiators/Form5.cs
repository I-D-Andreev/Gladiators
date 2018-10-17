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
    public partial class Form5 : Form
    {
        Form1 ff;
        public Form5(Form1 _ff)
        {
            InitializeComponent();
            ff = _ff;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;

            Attack[0] = button1;
            Attack[1] = button2;
            Attack[2] = button3;


            Pictures[0] = pictureBox1;
            Pictures[1] = pictureBox2;
            Pictures[2] = pictureBox3;

        }


        Button[] Attack = new Button[3];
        PictureBox[] Pictures = new PictureBox[3];

        int choose=0;
        int HeroHP, BossHP, BossDmg=70;
        int[] ArrayBossHp = new int[3] { 100, 230, 300 };
        Random r = new Random();

        void Fight(object sender, EventArgs e)
        {
            int dmgHero = 0, dmgBoss = 0;
            int missChance = 0;    
            dmgBoss = BossDmg * r.Next(40, 45) / 100;

            int buttonClicked = 0;
            for (int i = 0; i <= 2; i++)
            {
                if (Attack[i] == sender)
                {
                    buttonClicked = i;
                    break;
                }
            }

            if (buttonClicked == 0) { dmgHero = Form1.damage * r.Next(40, 45) / 100; missChance = 0; }
            else
                if (buttonClicked == 1) { dmgHero = Form1.damage * r.Next(60, 65) / 100; missChance = 20; }
                else
                    if (buttonClicked == 2) { dmgHero = Form1.damage * r.Next(85, 90) / 100; missChance = 50; }

            if (r.Next(0, 101) <= missChance)
            {
                dmgHero = 0;
            }


            this.Refresh();

            label3.Visible = true;

            if (dmgHero == 0) label3.Text = "Miss";
            else
                label3.Text = dmgHero.ToString();

            Pictures[buttonClicked].Visible = true;
            Attack[buttonClicked].Enabled = false;

            this.Refresh();
            System.Threading.Thread.Sleep(1500);
            this.Refresh();

            BossHP -= dmgHero;
            label2.Text = BossHP.ToString();
            label3.Visible = false;
            Pictures[buttonClicked].Visible = false;

            if (BossHP <= 0)
            {
                int moneyz = r.Next(5, 10) * (choose+1);
                Form1.money += moneyz;
                MessageBox.Show("You win! You loot " + moneyz.ToString() + " money!");
              //  Form1.MakeVisible();
                this.Visible = false;
                ff.Visible = true;

            }
            else
            {

                this.Refresh();
                System.Threading.Thread.Sleep(1500);
                /////////// boss attack
                this.Refresh();

                label4.Visible = true;
                label4.Text = dmgBoss.ToString();
                pictureBox4.Visible = true;
                this.Refresh();

                System.Threading.Thread.Sleep(1500);
                this.Refresh();

                HeroHP -= dmgBoss;

                if (HeroHP <= 0)
                {
                    MessageBox.Show("You lose!");
                   // Form1.MakeVisible();
                    //this.Close();
                    this.Visible = false;
                    ff.Visible = true;
                }


                label4.Visible = false;
                label1.Text = HeroHP.ToString();
                pictureBox4.Visible = false;


                Attack[buttonClicked].Enabled = true;
            }


        }


        public string GetHP
        {
            set
            {
                this.label1.Text = value;
                HeroHP = int.Parse(value);

                choose = r.Next(0, 3);

                Image Background = new Bitmap(@"images\fight\" + choose.ToString() + ".jpg");
                this.BackgroundImage = Background;
                BossHP = ArrayBossHp[choose];

                label2.Text = BossHP.ToString();

                for (int i = 0; i <= 2; i++) Attack[i].Enabled = true;
            }

        }
 
    }
}
