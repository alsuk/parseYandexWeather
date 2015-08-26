using System;
using System.Xml;

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
            GetID(city, id);
        }

        /// <summary>
        /// Получаем id города
        /// </summary>
        /// <returns>id</returns>
        static string GetID(string city,string id)
        {
            XmlDocument xml = new XmlDocument();
            // Тащим таблицу городов с айдишниками
            xml.Load("https://pogoda.yandex.ru/static/cities.xml");
            XmlNode root = xml.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("//city");
            // Ищем интересующий город и достаем id
            foreach (XmlNode node in nodes)
            {
                if (node.InnerText == city)
                {
                    id = node.Attributes[0].Value;
                    Console.WriteLine(node.Attributes[0].Value + "\t" + node.InnerText);
                }
            }
            // Возвращаем id города
            return id;
        }
    }
}
