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

        int P = 0, C = 0;       //игрок, компьютер

        int Pobeda_P = 0;
        int Pobeda_C = 0;

        int[] GamePoleC = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        
        string[] ImgName =
        {
            "Blank.png", //пустой блок
            "x.png", //крестик
            "o.png"  //нолик
        };
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

            //имя в ячейке 
            string NAME = "P_";

            //счетчик подсчета картинок
            int IndexPicture = 0;
            for (int yy = 0; yy < 3; yy++)
            {
                for (int xx = 0; xx < 3; xx++)
                {
                    GamePole[IndexPicture] = new PictureBox()
                    {
                        Name = NAME + IndexPicture,                 //Задаем имя картинки
                        Height = HeighP,
                        Width = WhidthP,
                        Image = Image.FromFile("Blank.png"),        //загружаем изображение пустого поля
                        SizeMode = PictureBoxSizeMode.StretchImage, //сжатие картинки
                        Location = new Point(dx, dy)
                    };
                    GamePole[IndexPicture].Click += Picture_Click;
                    panel3.Controls.Add(GamePole[IndexPicture]);
                    IndexPicture++;
                    dx += HeighP + 1;
                }
                dx = 0;
                dy += WhidthP+1;
            }

        }

        void KolPob()
        {
            label6.Text = Convert.ToString(Pobeda_C);
            label7.Text = Convert.ToString(Pobeda_P);
        }

        void BlockPole()    //Блокировка поля
        {
            foreach (PictureBox P in GamePole) P.Enabled = false;
        }

        void RazblockPole() //Разблокировка поля
        {
            int I = 0;
            foreach(PictureBox P in GamePole)
            {
                if (GamePoleC[I++] == 0) P.Enabled = true;
            }
        }

        bool Stop()     //Проверка свободных ходов
        {
            foreach (int s in GamePoleC)
                if (s == 0) return true;
            return false;
            
            if (Proverka(P))        //проверяем не выиграл ли игрок
            {
                label3.Text = "Вы выиграли";
                Pobeda_P++;
                KolPob();
                BlockPole();
                panel3.Visible = false;
                panel4.Visible = true;
                //если не нашли то ходить больше нельзя
                return false;
            }
            //проверяем не выиграл ли игрок
            if (Proverka(C))
            {
                label3.Text = "Вы проиграли";
                panel4.Visible = true;
                Pobeda_C++;
                KolPob();
                panel3.Visible = false;
                panel4.Visible = true;
                //прячем панель игры
                BlockPole();
                return false;
            }

            return false;
            //если ходить больше нельзя и никто не выиграл значит пишем что ничья
            label3.Text = "Ничья";
            //прячем панель игры
            panel3.Visible = false;
            panel4.Visible = true;
            KolPob();
            BlockPole();
            
        }

        bool Proverka(int hod)  //Проверка победы
        {
            //список вариантов выигрышных комбинаций
            int[,] Variant =
            {      {    //1 вариант
                    1,1,1,  //Х Х Х
                    0,0,0,  //_ _ _
                    0,0,0   //_ _ _
                }, {    //2 вариант
                    0,0,0,  //_ _ _
                    1,1,1,  //Х Х Х
                    0,0,0   //_ _ _
                }, {    //3 вариант
                    0,0,0,  //_ _ _
                    0,0,0,  //_ _ _
                    1,1,1   //Х Х Х
                }, {    //4 вариант
                    1,0,0,  //Х _ _
                    1,0,0,  //Х _ _
                    1,0,0   //Х _ _
                }, {    //5 вариант
                    0,1,0,  //_ Х _
                    0,1,0,  //_ Х _
                    0,1,0   //_ Х _
                }, {    //6 вариант
                    0,0,1,  //_ _ Х
                    0,0,1,  //_ _ Х
                    0,0,1   //_ _ Х
                }, {    //7 вариант
                    1,0,0,  //Х _ _
                    0,1,0,  //_ Х _
                    0,0,1   //_ _ Х
                }, {    //8 вариант
                    0,0,1,   //_ _ Х
                    0,1,0,   //_ Х _
                    1,0,0    //Х _ _
                }
            };
            int[] TestMap = new int[GamePoleC.Length];
            for (int I = 0; I < GamePoleC.Length; I++)
                if (GamePoleC[I] == hod) TestMap[I] = 1;    //если число из поля равен ходу
 
            for (int Variant_Index = 0; Variant_Index < Variant.GetLength(0); Variant_Index++) //вариант для сравненич
            {
                int f = 0;        //счетчик для подсчета 
                for (int TestIndex = 0; TestIndex < TestMap.Length; TestIndex++)
                {
                    if (Variant[Variant_Index, TestIndex] == 1)
                    {
                        if (Variant[Variant_Index, TestIndex] == TestMap[TestIndex]) f++;    //если в параметр  в варианте выигрыша совпал с вариантом на поле
                    }
                    if (f == 3) return true;        //найдены 3 варианта
                }
            }
            return false;
        }

        void HodPC()    //ход компьютера
        {
            Random Rand = new Random();
            GENER:
            if (Stop())
            {
                int Step = Rand.Next(0, 9);
                if (GamePoleC[Step] == 0)
                {
                    GamePole[Step].Image = Image.FromFile(ImgName[C]);
                    GamePoleC[Step] = C;

                }
                else goto GENER;
                if (Proverka(C))
                {
                    panel3.Visible = false;
                    panel4.Visible = true;
                    label3.Text = "Вы проиграли";
                    Pobeda_C++;
                    KolPob();
                }
            }
            else
            {
                panel3.Visible = false;
                panel4.Visible = true;
                label3.Text = "Ничья";
                KolPob();
            }

            }

        private void Picture_Click(object sender, EventArgs e)
        {
            if(Stop())
            {
                PictureBox CImage = sender as PictureBox;
                string[] PName = CImage.Name.Split('_');    //номер нажатой ячейки
                int Index = Convert.ToInt32(PName[1]);      //
                GamePole[Index].Image = Image.FromFile(ImgName[P]); //загрузка изображения хода игрока
                GamePoleC[Index] = P;
                if(!Proverka(P))
                {
                    BlockPole();
                    HodPC();
                    RazblockPole();
                }
                else
                {
                    panel3.Visible = false;
                    panel4.Visible = true;
                    label3.Text = "Вы выиграли";
                    BlockPole();
                    Pobeda_P++;
                    KolPob();
                }
            }
            else
            {
                panel3.Visible = false;
                panel4.Visible = true;
                label3.Text = "Ничья";
                KolPob();
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
            MainPole();
            panel2.Location = new Point(12, 12);
            panel3.Location = new Point(12, 12);
            panel4.Location = new Point(12, 12);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e) // выбор хода Х
        {
            P = 1;
            C = 2;
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e) // выбор хода О
        {
            P = 2;
            C = 1;
            panel2.Visible = false;
            panel3.Visible = true;
            HodPC();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GamePoleC = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (PictureBox P in GamePole) P.Image = Image.FromFile(ImgName[0]);
            P = 0; C = 0;
            RazblockPole();
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GamePoleC = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (PictureBox P in GamePole) P.Image = Image.FromFile(ImgName[0]);
            P = 0; C = 0;
            RazblockPole();
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
