using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.AbstractFactory.Model
{
    /// <summary>
    /// "ConcreteFactory1" az abs factory mintában
    /// </summary>
    public class ElfUnitFactory :UnitFactory
    {
        public override IArcherUnit CreateArcherUnit(int lvl)
        {
            return new ElfArcher(lvl);
        }
        public override IFighterUnit CreateFighterUnit(int lvl)
        {
            return new ElfFighter(lvl);
        }
    }
}
