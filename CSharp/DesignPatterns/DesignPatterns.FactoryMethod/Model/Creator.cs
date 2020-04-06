using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.FactoryMethod.Model
{
    /// <summary>
    /// "Factory"/"Creator" osztály a tervezési mintában
    /// </summary>
    public abstract class Creator
    {
        public abstract IUnit CreateUnit(UnitType Ctype, int lvl);
}
}
