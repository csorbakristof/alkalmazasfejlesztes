using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.AbstractFactory.Model
{
    /// <summary>
    /// "ConcreteFactory2" a mintában
    /// </summary>
    public class OrcUnitFactory : UnitFactory
    {
        public override IArcherUnit CreateArcherUnit(int lvl)
        {
            return new OrcArcher(lvl);
        }

        public override IFighterUnit CreateFighterUnit(int lvl)
        {
            return new OrcFighter(lvl);
        }
    }
}
