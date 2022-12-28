using System.Net;
using System.Text;
using System.Xml;

namespace XMLNBU
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


        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            //XmlDocument xml = new();
            //xml.Load("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange");
            //PrintNode(xml);

            //Console.WriteLine("Тест загрузки ХМЛ");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string data = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                data = reader.ReadToEnd();

            Console.WriteLine(data);

        }
    }
}