using System.Text;


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
            //1) SORT mekhanizm
            //2) красивый принт
            //3) делегаты на печать с условием

        }
    }
}