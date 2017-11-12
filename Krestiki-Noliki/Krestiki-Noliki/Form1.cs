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
        //поле из 9 картинок
        PictureBox[] GamePole = new PictureBox[9];

        public Form1()
        {
            InitializeComponent();
        }

        void MainPole()
        {
            int dx=0, dy=0;
            //размеры картинки
            int
                HeighP = 75, //высота
                WhidthP = 75;  //ширина

            //имя в ячейке будет начинаться с этих символов
            string NAME = "P_";

            //счетчик подсчета картинок
            int IndexPicture = 0;
            for (int yy = 0; yy < 3; yy++)
            {
                for (int xx = 0; xx < 3; xx++)
                {
                    GamePole[0] = new PictureBox()
                    {
                        Name = NAME + IndexPicture,                 //Задаем имя картинки
                        Height = HeighP,
                        Width = WhidthP,
                        Image = Image.FromFile("Blank.png"),        //загружаем изображение пустого поля
                        SizeMode = PictureBoxSizeMode.StretchImage, //сжатие картинки
                        Location = new Point(dx, dy)
                    };

                    panel3.Controls.Add(GamePole[0]);
                    IndexPicture++;
                    dx += HeighP + 1;
                }
                dx = 0;
                dy += WhidthP;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;

            panel1.Visible = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainPole();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();        }
    }
}
