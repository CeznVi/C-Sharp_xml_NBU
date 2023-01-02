using System.Text;
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
            app.Menu();
        }
    }
}