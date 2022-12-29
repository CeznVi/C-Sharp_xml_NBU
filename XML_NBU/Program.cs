using System.Text;


namespace XML_NBU
{
    internal class Program
    {

        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            CurrencyApp app = new();
            app.LoadXMLfromNBU();


            //foreach (Currency item in ExchRate)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine($"\n\n\nДАТА {date}");


            //TODO
            //1) SORT mekhanizm
            //2) красивый принт
            //3) делегаты на печать с условием

        }
    }
}