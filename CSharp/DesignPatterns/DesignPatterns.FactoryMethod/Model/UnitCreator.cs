using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.FactoryMethod.Model
{
	/// <summary>
	/// "Concrete factory"/"Concrete creator" a tervezési mintában
	/// </summary>
    public class UnitCreator : Creator
	{
		public override IUnit CreateUnit(UnitType Ctype, int lvl){

			switch (Ctype)
			{
				case UnitType.Fighter: return new Fighter(lvl);
				case UnitType.Archer: return new Archer(lvl);
				case UnitType.Mage: return new Mage(lvl);
				default: throw new Exception("invalid character type");
			}
		
	}
}
}
