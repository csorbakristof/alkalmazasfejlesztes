using DesignPatterns.FactoryMethod.Model;
using System;
using System.Collections.Generic;

namespace DesignPatterns.FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Creator creator = new UnitCreator();

            List<IUnit> characters = new List<IUnit>();

            characters.Add(creator.CreateUnit(UnitType.Fighter, 2));
            characters.Add(creator.CreateUnit(UnitType.Archer, 5));
            characters.Add(creator.CreateUnit(UnitType.Mage, 8));
            characters.Add(creator.CreateUnit(UnitType.Archer, 4));
            characters.Add(creator.CreateUnit(UnitType.Fighter, 6));

            
            foreach (var c in characters)
            {
                Console.WriteLine(c.SaySomething());
            }

            foreach (var c in characters)
            {
                Console.WriteLine(c.ShowDetails());
            }

            Console.ReadKey();
        }
    }
}
