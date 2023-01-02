using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private void LoadXMLfromNBU()
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Хост не відповідає, або відсутне інтернет з'єднання");
                Console.WriteLine("Данні будуть відтворені із збереженого файлу");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                DeserializeData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        /// <summary>
        /// Збереження(серіалізація) данних у ХМЛ файл за допомогою XmlSerializer
        /// </summary>
        private void SerializeData()
        {
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                XmlSerializer serializer = new(typeof(List<Currency>));

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
        /// Завантаження серіалізованих данних з ХМЛ файлу
        /// </summary>
        private void DeserializeData()
        {
            try
            {
                if (!File.Exists(dirPath+fileName))
                    throw new FileNotFoundException($"Файл: {fileName} не створений. Робота додатка не можлива!");

                XmlSerializer serializer = new(typeof(List<Currency>));
                using (Stream stream = File.OpenRead(dirPath + fileName))
                {
                    ExchRate = (List<Currency>)serializer.Deserialize(stream);
                }

                if(ExchRate != null)
                    date = (ExchRate.ElementAt(0).Date.ToShortDateString()).ToString();
            }
            catch(FileNotFoundException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Надрукувати курс валют
        /// </summary>
        private void Print()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Дата оновлення курсу {date}".PadLeft(36));
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (Currency item in ExchRate)
                Console.WriteLine(item);

            Console.WriteLine("----------------------------------------------------");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.ReadKey();
        }
        /// <summary>
        /// Надрукувати курс валют за умовою по курсу
        /// </summary>
        private void PrintForConditionsRate(Func<double, bool> func)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Дата оновлення курсу {date}".PadLeft(36));
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (Currency item in ExchRate)
                if(func(item.Rate))
                Console.WriteLine(item);

            Console.WriteLine("----------------------------------------------------");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.ReadKey();
        }
        /// <summary>
        /// Меню додатку
        /// </summary>
        public void Menu()
        {
            LoadXMLfromNBU();
            if(ExchRate != null && ExchRate.Count > 0)
            { 
            List<string> listmenu = new()
            {
                "       Показати курс всіх валют      ",
                "Показати валюти (ціна яких  >= 30 грн.",
                "Показати валюти (ціна яких  <= 1 грн.",
                "Відсортувати валюти (спочатку дорогі)",
                "Відсортувати валюти (спочатку дешеві)",
                " Відсортувати валюти за абревіатурою ",
                "Вихід"
            };

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                int a = ConsoleMenu.SelectVertical(HPosition.Center,
                                    VPosition.Top,
                                    HorizontalAlignment.Center,
                                    listmenu);

                switch (a)
                {
                    case 0:
                        Print();
                        break;
                    case 1:
                        PrintForConditionsRate(x => x >= 30);
                        break;
                    case 2:
                        PrintForConditionsRate(x => x <= 1);
                        break;
                    case 3:
                        ExchRate.Sort((s2, s1) => s1.Rate.CompareTo(s2.Rate));
                        break;
                    case 4:
                        ExchRate.Sort((s1, s2) => s1.Rate.CompareTo(s2.Rate));
                        break;
                    case 5:
                        ExchRate.Sort();
                        break;
                    case 6:
                        exit = true;
                        break;
                    default:
                        exit = true;
                        break;
                }
            }
            Console.Clear();
            SerializeData();
            }
        }
    }
}
