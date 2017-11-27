using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krestiki_Noliki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        XO xx = new XO();


        public void Picture_Click(object sender, EventArgs e)
        {
            if(xx.Stop())
            {
                PictureBox CImage = sender as PictureBox;
                string[] PName = CImage.Name.Split('_');    //номер нажатой ячейки
                int Index = Convert.ToInt32(PName[1]);      //
                xx.GamePole[Index].Image = Image.FromFile(xx.ImgName[xx.P]); //загрузка изображения хода игрока
                xx.GamePoleC[Index] = xx.P;
                if(!xx.Proverka(xx.P))
                {
                    xx.BlockPole();
                    xx.HodPC();
                    xx.RazblockPole();
                }
                else
                {
                    panel3.Visible = false;
                    panel4.Visible = true;
                    label3.Text = "Вы выиграли";
                    xx.BlockPole();
                    xx.Pobeda_P++;
                    xx.KolPob();
                }
            }
            else
            {
                panel3.Visible = false;
                panel4.Visible = true;
                label3.Text = "Ничья";
                xx.KolPob();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Global.form = this;
            xx.MainPole();
            panel2.Location = new Point(12, 12);
            panel3.Location = new Point(12, 12);
            panel4.Location = new Point(12, 12);
        }

        public void Okno()
        {

        }


        public void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e) // выбор хода Х
        {
            xx.P = 1;
            xx.C = 2;
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e) // выбор хода О
        {
            xx.P = 2;
            xx.C = 1;
            panel2.Visible = false;
            panel3.Visible = true;
            xx.HodPC();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            xx.GamePoleC = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (PictureBox P in xx.GamePole)
                    P.Image = Image.FromFile(xx.ImgName[0]);
            xx.P = 0; xx.C = 0;
            xx.RazblockPole();
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            xx.GamePoleC = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (PictureBox P in xx.GamePole)
                    P.Image = Image.FromFile(xx.ImgName[0]);
            xx.P = 0; xx.C = 0;
            xx.RazblockPole();
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 F3 = new Form3();
            F3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
