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
    [Serializable]
    public class Currency: IComparable<Currency>
    {
        /// <summary>
        /// Параметр абревіатура валюти
        /// </summary>
        public string ShortName { get; set; } = "NULL";
        /// <summary>
        /// Параметр повна назва валюти
        /// </summary>
        public string Name { get; set; } = "NULL";
        /// <summary>
        /// Курс валюти 
        /// </summary>
        public double Rate { get; set; }
        /// <summary>
        /// Дата обмінного курсу 
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Конструктор без параметрів 
        /// </summary>
        public Currency() { }

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
                   $"{Rate} грн.\n" +
                   "Дата курсу".PadRight(18) +
                   $"{Date.ToString("d")}";
        }
        /// <summary>
        /// Порівняння об'єктів
        /// </summary>
        /// <param name="other">Інший об'єкт данного типу</param>
        /// <returns></returns>
        public int CompareTo(Currency? obj)
        {
            if(obj == null || ShortName == null) return 0;
            return ShortName.CompareTo(obj.ShortName);
        }
    }
}
