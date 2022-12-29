using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XML_NBU
{
    public class CurrencyApp
    {
        /// <summary>
        /// Лінк на XML файл сайту НБУ
        /// </summary>
        private readonly string link = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange";
        /// <summary>
        /// Дата обмінного курсу
        /// </summary>
        private string date = "";
        /// <summary>
        /// Контейнер валют 
        /// </summary>
        private List<Currency> ExchRate = new();


        /// <summary>
        /// Конструктор без параметрів
        /// </summary>
        public CurrencyApp() { }

        static string GetValueTag(XmlTextReader xml, string Tag)
        {
            while (xml.Read())
            {
                if (xml.NodeType == XmlNodeType.Element &&
                       xml.Name == Tag)
                {
                    xml.Read();
                    return xml.Value;
                }
            }
            return "";
        }
        /// <summary>
        /// Завантажити данні ХМЛ з сайту НБУ
        /// </summary>
        public void LoadXMLfromNBU()
        {
            using (XmlTextReader xmlR = new(link))
            {
                xmlR.WhitespaceHandling = WhitespaceHandling.None;
                while (xmlR.Read())
                {
                    if (xmlR.NodeType == XmlNodeType.Element &&
                       xmlR.Name == "currency")
                    {
                        Currency currency = new()
                        {
                            Name = new String(GetValueTag(xmlR, "txt")),
                            Rate = Convert.ToDouble(GetValueTag(xmlR, "rate").Replace('.', ',')),
                            ShortName = new String(GetValueTag(xmlR, "cc"))  
                        };
                        ExchRate.Add(currency);
                        //встановлення дати
                        if (date.Length == 0)
                            date = new String(GetValueTag(xmlR, "exchangedate"));
                    }
                }
                ExchRate.Sort();
            }
        }
        /// <summary>
        /// Надрукувати курс валют
        /// </summary>
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Дата оновлення курсу {date}".PadLeft(36));

            Console.ForegroundColor = ConsoleColor.Green;

            foreach (Currency item in ExchRate)
                Console.WriteLine(item);

            Console.WriteLine("----------------------------------------------------");
            Console.ResetColor();
            Console.SetCursorPosition(0,0);
            Console.ReadKey();
        }

    }
}
