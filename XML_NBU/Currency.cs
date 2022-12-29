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
        /// <summary>
        /// Параметр абревіатура валюти
        /// </summary>
        public string? ShortName { get; set; }
        /// <summary>
        /// Параметр повна назва валюти
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Курс валюти 
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Масив абрівіатур дорогоцінних металів
        /// </summary>
        private readonly string compareName = "XAU XAG  XPT XPD";

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
        public Currency(string? sName, string? name, double rate)
        {
            ShortName = sName;
            Name = name;

            if (compareName.Contains(compareName))
                Rate = ConvertToGramRate(rate);
            else
                Rate = rate;
        }
        /// <summary>
        /// Повертає інформацію про валюту в текстовому вигляді 
        /// </summary>
        /// <returns>string (інфомація в текстовому вигляді про валюту)</returns>
        public override string ToString()
        {
            return "----------------------------------------------------\n" +
                   "Абревіатура: ".PadRight(18) + 
                   $"{ShortName}\n" +
                   "Повна назва: ".PadRight(18) +
                   $"{Name}\n" +
                   "Курс: ".PadRight(18) +
                   $"{Rate} грн.";
        }

        private double ConvertToGramRate(double rateForUncia)
        {
           return rateForUncia = rateForUncia / 28.349523125;
        }
    }
}
