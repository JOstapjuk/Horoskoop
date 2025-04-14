using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horoskoop
{
    public class ZodiacSign
    {
        public string Name { get; set; }
        public string Dates { get; set; }
        public string Symbol { get; set; }
        public string Element { get; set; }
        public string Planet { get; set; }
        public string Traits { get; set; }

        public ZodiacSign(string name, string dates, string symbol, string element, string planet, string traits)
        {
            Name = name;
            Dates = dates;
            Symbol = symbol;
            Element = element;
            Planet = planet;
            Traits = traits;
        }
    }

}
