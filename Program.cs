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
            GetID(city,ref id);
            GetWeather(id);
        }

        /// <summary>
        /// Получаем id города
        /// </summary>
        /// <returns>id</returns>
        static string GetID(string city,ref string id)
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

        static void GetWeather(string id)
        {
            Console.WriteLine("Привет, {0}", id);
            // Берем xml с погодой для нужного города
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Format("https://export.yandex.ru/weather-ng/forecasts/{0}.xml", id));
            XmlElement xRoot = doc.DocumentElement;
            // Берем сегодняшний день
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Name == "fact")
                {
                    if (xnode.Attributes.Count > 0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem("fact");
                        if (attr != null) Console.WriteLine(attr.Value);
                    }
                    // Вывод погоды
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        // Небо (пасмурно, ясно, облачно и т.д.
                        string sky = "";
                        // Влажность
                        string hum = "";
                        // Скорость ветра
                        string ws = "";
                        // Давление
                        string pres = "";
                        // Температура воздуха
                        string temp = "";
                        // Отображаем результат
                        if (childnode.Name == "weather_type") { sky = childnode.InnerText; Console.WriteLine("На улице {0}.", sky); }
                        if (childnode.Name == "humidity") { hum = childnode.InnerText; Console.WriteLine("Влажность: {0}.", hum); }
                        if (childnode.Name == "wind_speed") { ws = childnode.InnerText; Console.WriteLine("Скорость ветра {0}.", ws); }
                        if (childnode.Name == "pressure") { pres = childnode.InnerText; Console.WriteLine("Давление: {0}.", pres); }
                        if (childnode.Name == "temperature") { temp = childnode.InnerText; Console.WriteLine("Температура воздуха {0} градусов", temp); }
                    }
                }
            }
        }
    }
}
