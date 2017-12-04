using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Krestiki_Noliki
{
    public class XO //: Form1
    {
        //поле из 9 картинок
        public PictureBox[] GamePole = new PictureBox[9];

       /* public PictureBox this[int index]
        {
            set { GamePole[index]=value ; }
            get { return GamePole[index]; }
        }*/

        //Form1 f = new Form1();
        public int P = 0, C = 0;       //игрок, компьютер

        public int Pobeda_P = 0;
        public int Pobeda_C = 0;

        public int[] GamePoleC= { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public string[] ImgName =
        {
            "Blank.png", //пустой блок
            "x.png", //крестик
            "o.png"  //нолик
        };

        public void MainPole()
        {
            int dx = 0, dy = 0;
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
                    GamePole[IndexPicture].Click += Global.form.Picture_Click;
                    Global.form.panel3.Controls.Add(GamePole[IndexPicture]);
                    IndexPicture++;
                    dx += HeighP + 1;
                }
                dx = 0;
                dy += WhidthP + 1;
            }

        }

        public void KolPob()
        {
            Global.form.label6.Text = Convert.ToString(Pobeda_C);
            Global.form.label7.Text = Convert.ToString(Pobeda_P);
        }

        public void BlockPole()    //Блокировка поля
        {
            foreach (PictureBox P in GamePole) P.Enabled = false;
        }

        public void RazblockPole() //Разблокировка поля
        {
            int I = 0;
            foreach (PictureBox P in GamePole)
            {
                if (GamePoleC[I++] == 0) P.Enabled = true;
            }
        }

        public bool Stop()     //Проверка свободных ходов
        {
            foreach (int s in GamePoleC)
                if (s == 0) return true;
            return false;

            if (Proverka(P))        //проверяем не выиграл ли игрок
            {
                Global.form.label3.Text = "Вы выиграли";
                Pobeda_P++;
                KolPob();
                BlockPole();
                Global.form.panel3.Visible = false;
                Global.form.panel4.Visible = true;
                //если не нашли то ходить больше нельзя
                return false;
            }
            //проверяем не выиграл ли игрок
            if (Proverka(C))
            {
                Global.form.label3.Text = "Вы проиграли";
                Global.form.panel4.Visible = true;
                Pobeda_C++;
                KolPob();
                Global.form.panel3.Visible = false;
                Global.form.panel4.Visible = true;
                //прячем панель игры
                BlockPole();
                return false;
            }

            return false;
            //если ходить больше нельзя и никто не выиграл значит пишем что ничья
            Global.form.label3.Text = "Ничья";
            //прячем панель игры
            Global.form.panel3.Visible = false;
            Global.form.panel4.Visible = true;
            KolPob();
            BlockPole();

        }

        public bool Proverka(int hod)  //Проверка победы
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

        public void HodPC()    //ход компьютера
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
                    Global.form.panel3.Visible = false;
                    Global.form.panel4.Visible = true;
                    Global.form.label3.Text = "Вы проиграли";
                    Pobeda_C++;
                    KolPob();
                }
            }
            else
            {
                Global.form.panel3.Visible = false;
                Global.form.panel4.Visible = true;
                Global.form.label3.Text = "Ничья";
                KolPob();
            }

        }
    }
}
