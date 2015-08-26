using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseYandexWeather
{
    class Program
    {
        static void Main(string[] args)
        {
            // id города, нужен для получения ссылок с прогнозом
            string id = "";
            Console.WriteLine("Введите город, погоду в котором хотите узнать");
            string city = Console.ReadLine();
            // Первую букву делаем заглавной, остальные строчные
            city = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);
        }
    }
}
