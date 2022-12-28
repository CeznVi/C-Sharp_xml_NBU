using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_NBU
{
    /// <summary>
    /// Клас контейнер для зберігання інформації про валюту
    /// </summary>
    public class Currency
    {
        public string? ShortName { get; set; }
        public string? Name { get; set; }
        public double Rate { get; set; }

        /// <summary>
        /// Конструктор без параметрів 
        /// </summary>
        public Currency() { }
        /// <summary>
        /// Конструктор з параметрами
        /// </summary>
        /// <param name="shortName">Коротка назва</param>
        /// <param name="name">Повна назва</param>
        /// <param name="rate">Курс</param>
        public Currency(string? shortName, string? name, double rate)
        {
            ShortName = shortName;
            Name = name;
            Rate = rate;
        }

        /// <summary>
        /// Повертає інформацію про валюту в текстовому вигляді 
        /// </summary>
        /// <returns>string (інфомація в текстовому вигляді про валюту)</returns>
        public override string ToString()
        {
            return $"Абревіатура {ShortName}\n" +
                   $"Повна назва {Name}\n" +
                   $"Курс: {Rate}\n";
        }


    }
}
