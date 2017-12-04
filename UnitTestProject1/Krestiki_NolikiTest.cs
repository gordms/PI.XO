using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Krestiki_Noliki;

namespace Krestiki_NolikiTest
{
    [TestClass]
    public class Krestiki_NolikiTest
    {
        [TestMethod]
        public void Proverka1()  //Stop() Proverka 
        {
            // исходные данные
            int a = 1;
            bool t = false;

            // получение значения с помощью тестируемого метода
            XO x = new XO();
            for(int i=0;i<9;i++)
                x.GamePoleC[i] = 0;
            bool r = x.Proverka(1);          // сравнение ожидаемого результата с полученным

            Assert.AreEqual(t,r);
        }
        [TestMethod]
        public void Proverka2()  //Stop() Proverka 
        {
            // исходные данные
            int a = 1;
            bool t = false;

            // получение значения с помощью тестируемого метода
            XO x = new XO();
            for (int i = 0; i < 9; i++)
                if (i >= 6)
                    x.GamePoleC[i] = 2;
                else
                    x.GamePoleC[i] = 0;
            bool r = x.Proverka(1)
;            // сравнение ожидаемого результата с полученным

            Assert.AreEqual(t, r);
        }
        [TestMethod]
        public void Proverka3()  //Stop() Proverka 
        {
            // исходные данные
            int a = 2;
            bool t = true;

            // получение значения с помощью тестируемого метода
            XO x = new XO();
            for (int i = 0; i < 9; i++)
                if(i>=6)
                    x.GamePoleC[i] = 2;
                else
                    x.GamePoleC[i] = 0;
            bool r = x.Proverka(2);            // сравнение ожидаемого результата с полученным

            Assert.AreEqual(t, r);
        }
        [TestMethod]
        public void Proverka4()  //Stop() Proverka 
        {
            // исходные данные
            int a = 2;
            bool t = true;

            // получение значения с помощью тестируемого метода
            XO x = new XO();
            for (int i = 0; i < 9; i++)
                if (i >= 6)
                    x.GamePoleC[i] = 2;
                else
                    if(i<=1)
                        x.GamePoleC[i] = 1;
                        else
                        x.GamePoleC[i] = 0;
            bool r = x.Proverka(2);            // сравнение ожидаемого результата с полученным
            Assert.AreEqual(t, r);
        }
        [TestMethod]
        public void Proverka5()  //Stop() Proverka 
        {
            // исходные данные
            int a = 2;
            bool t = true;

            // получение значения с помощью тестируемого метода
            XO x = new XO();
            for (int i = 0; i < 9; i++)
                if (i%2==0)
                    x.GamePoleC[i] = 2;
                else
                    x.GamePoleC[i] = 1;
            bool r = x.Proverka(2);            // сравнение ожидаемого результата с полученным
            Assert.AreEqual(t, r);
        }
        [TestMethod]
        public void Stop1()  //Stop() Proverka 
        {
            // исходные данные
            bool t = true;

            // получение значения с помощью тестируемого метода
            XO x = new XO();
            for (int i = 0; i < 9; i++)
                x.GamePoleC[i] = 0;
            bool r = x.Stop();           // сравнение ожидаемого результата с полученным

            Assert.AreEqual(t, r);
        }
        [TestMethod]
        public void Stop2()  //Stop() Proverka 
        {
            // исходные данные
            bool t = false;

            // получение значения с помощью тестируемого метода
            XO x = new XO();
            for (int i = 0; i < 9; i++)
                x.GamePoleC[i] = 1;
            bool r = x.Stop();         // сравнение ожидаемого результата с полученным

            Assert.AreEqual(t, r);
        }
        [TestMethod]
        public void Stop3()  //Stop() Proverka 
        {
            // исходные данные
            bool t = true;

            // получение значения с помощью тестируемого метода
            XO x = new XO();
            for (int i = 0; i < 8; i++)
                x.GamePoleC[i] = 1;
            x.GamePoleC[8] = 0;
            bool r = x.Stop();           // сравнение ожидаемого результата с полученным

            Assert.AreEqual(t, r);
        }
        [TestMethod]
        public void Stop4()  //Stop() Proverka 
        {
            // исходные данные
            bool t = false;

            // получение значения с помощью тестируемого метода
            XO x = new XO();
            for (int i = 0; i < 9; i++)
                if (i >= 6)
                    x.GamePoleC[i] = 2;
                else
                    if (i%2==0)
                    x.GamePoleC[i] = 1;
                else
                    x.GamePoleC[i] = 2;
            bool r = x.Stop();        // сравнение ожидаемого результата с полученным

            Assert.AreEqual(t, r);
        }
        [TestMethod]
        public void Stop5()  //Stop() Proverka 
        {
            // исходные данные
            bool t = false;

            // получение значения с помощью тестируемого метода
            XO x = new XO();
            for (int i = 0; i < 9; i++)
                if (i % 2 == 0)
                    x.GamePoleC[i] = 1;
                else
                    x.GamePoleC[i] = 2;
            bool r = x.Stop();        // сравнение ожидаемого результата с полученным

            Assert.AreEqual(t, r);
        }
    }
}
