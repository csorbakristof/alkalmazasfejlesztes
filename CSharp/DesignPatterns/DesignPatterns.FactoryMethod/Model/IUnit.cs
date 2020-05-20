using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.FactoryMethod.Model
{
    /// <summary>
    /// Egységes interface a factory "Product" osztályainak
    /// </summary>
    public interface IUnit
    {
        public int LVL { get;}
        public int MaxHP { get; }
        public int STR { get; }
        public int DEX { get; }
        public int INT { get; }
        public string SaySomething();
        public string ShowDetails();
    }
}
