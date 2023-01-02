using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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
        /// Шлях до директорії де зберігати серіалізовані данні
        /// </summary>
        private readonly string dirPath = "../../../../SaveFile";
        /// <summary>
        /// Назва файлу для збереження серіалізованих данних
        /// </summary>
        private readonly string fileName = "/SaveData.xml";

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
            try
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
                                ShortName = new String(GetValueTag(xmlR, "cc")),
                                Date = DateTime.Parse(GetValueTag(xmlR, "exchangedate"))
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
            catch (System.Net.Http.HttpRequestException)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Хост не відповідає, або відсутне інтернет з'єднання");
                Console.ResetColor();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void SerializeData()
        {
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(List<Currency>));

                using (Stream stream = File.Create(dirPath + fileName))
                {
                    serializer.Serialize(stream, ExchRate);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
