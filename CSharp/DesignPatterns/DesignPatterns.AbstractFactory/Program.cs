using DesignPatterns.AbstractFactory.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DesignPatterns.AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create elves");
            FactoryMethodExample(new ElfUnitFactory());
            Console.WriteLine("Create orcs");
            FactoryMethodExample(new OrcUnitFactory());

            Console.ReadKey();

        }

        static void FactoryMethodExample(UnitFactory factory)
        {
            List<IUnit> unitList = new List<IUnit>();
            unitList.Add(factory.CreateArcherUnit(5));
            unitList.Add(factory.CreateFighterUnit(5));
            unitList.Add(factory.CreateArcherUnit(3));
            unitList.Add(factory.CreateFighterUnit(3));

            foreach (var unit in unitList)
            {
                Console.WriteLine(unit.SaySomething());
            }

            foreach (var unit in unitList)
            {
                Console.WriteLine(unit.ShowDetails());
            }

            foreach (var unit in unitList)
            {
                if(unit is IArcherUnit)
                {
                    Console.WriteLine(((IArcherUnit)unit).Shoot()); 
                }
                if(unit is IFighterUnit)
                {
                    Console.WriteLine(((IFighterUnit)unit).Punch());
                }
            }
        }
    }
}
