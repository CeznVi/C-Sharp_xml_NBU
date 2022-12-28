using System.Text;
using System.Xml;

namespace XML_NBU
{
    internal class Program
    {

        static void PrintNode(XmlNode node)
        {
            Console.WriteLine($"Type={node.NodeType}".PadRight(20) +
                $"Name={node.Name}".PadRight(20) +
                $"Value={node.Value}".PadRight(20));

            if (node.Attributes != null)
            {
                foreach (XmlNode item in node.Attributes)
                {
                    Console.WriteLine($"Type={item.NodeType}".PadRight(20) +
                $"Name={item.Name}".PadRight(20) +
                $"Value={item.Value}".PadRight(20));
                }
            }

            if (node.HasChildNodes)
            {
                foreach (XmlNode item in node.ChildNodes)
                {
                    PrintNode(item);
                }
            }
        }

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
            return null;
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            //лінк за яким працюємо
            string link = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange";
            //поле дати
            string date = "";
            //список данних про валюти
            List<Currency> ExchRate = new();


            using (XmlTextReader xmlR = new(link))
            {
                xmlR.WhitespaceHandling = WhitespaceHandling.None;
                while (xmlR.Read())
                {
                    if (xmlR.NodeType == XmlNodeType.Element &&
                       xmlR.Name == "currency")
                    {
                        Currency currency = new();
                        currency.Name = new String(GetValueTag(xmlR, "txt"));
                        currency.Rate = Convert.ToDouble(GetValueTag(xmlR, "rate").Replace('.', ','));
                        currency.ShortName = new String(GetValueTag(xmlR, "cc"));
                        ExchRate.Add(currency);
                        
                        //встановлення дати
                        if (date.Length == 0)
                            date = new String(GetValueTag(xmlR, "exchangedate"));

                    }
                }
            }

            foreach (Currency item in ExchRate)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"\n\n\nДАТА {date}");


            //TODO
            //1) SORT mekhanizm
            //2) красивый принт
            //3) делегаты на печать с условием

        }
    }
}