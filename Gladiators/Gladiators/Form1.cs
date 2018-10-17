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
    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label5.Text = damage.ToString();
            label6.Text = health.ToString();

            timer1.Start();
           

        }

        public static int money = (int)Settings.Default["MONEY"];
        public static int health = (int)Settings.Default["HP"];
        public static int damage = (int)Settings.Default["DMG"];


        public Form2 form2;
        public Form3 form3;
        public Form4 form4;
        public Form5 form5; 

 


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            form2.LabelText = money.ToString();
            form2.Show();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            form3.LabelText = money.ToString();
            form3.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e) // forest - form5
        {
            this.Visible = false;
            form5.GetHP = Form1.health.ToString();
            form5.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = Form1.damage.ToString();
            label6.Text = Form1.health.ToString();
        }

        private void pictureBox4_Click(object sender, EventArgs e) // arena - form 4
        {
            this.Visible = false;
            form4.GetHP = Form1.health.ToString();
            form4.Show();

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            form2 = new Form2(this);
            form3 = new Form3(this);
            form4 = new Form4(this);
            form5 = new Form5(this);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Settings.Default["HP"] = Form1.health;
            Settings.Default["MONEY"] = Form1.money;
            Settings.Default["DMG"] = Form1.damage;
            Settings.Default["WEPSAVE"] = form3.LastClicked;
            Settings.Default["HELMSAVE"] = form2.ButtonCLickEnable[0];
            Settings.Default["ARMSAVE"] = form2.ButtonCLickEnable[1];
            Settings.Default["SANSAVE"] = form2.ButtonCLickEnable[2];
            
            
            Settings.Default.Save();

        }
    }
}
