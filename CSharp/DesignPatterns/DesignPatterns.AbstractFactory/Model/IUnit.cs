using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.AbstractFactory.Model
{
    /// <summary>
    /// Egységes interface unit-oknak, nincs benne konkrétan a mintában
    /// </summary>
    public interface IUnit
    {
        public int LVL { get; }
        public int MaxHP { get; }
        public int STR { get; }
        public int DEX { get; }
        public int INT { get; }
        public string SaySomething();
        public string ShowDetails();
    }
}
