﻿using System.Text;
using System.Xml.Linq;


namespace XML_NBU
{
    internal class Program
    {

        static void Main()
        {
            Console.Title = "NBU INFORMER";
            Console.OutputEncoding = Encoding.Unicode;
            Console.SetWindowSize(52, 30);

            CurrencyApp app = new();
            app.LoadXMLfromNBU();
            app.Print();


            //TODO
            //3) делегаты на печать с условием
            // меню
            // в конце програмі сохранить данніе в файл
            // механизм загрузки данніх с файла
            //механизм екзепшена если не сработал интернет (+ всплівающее окно ошибки что нет интернета)



            //от большего к меньшему
            //ExchRate.Sort((s2, s1) => s1.Rate.CompareTo(s2.Rate));
            ////от меньшего к большему
            //ExchRate.Sort((s1, s2) => s1.Rate.CompareTo(s2.Rate));
        }
    }
}