using System;

namespace DesignPatterns.Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            //create some sample units
            Unit mage = new Unit();
            mage.MaxHP = 10;
            mage.STR = 1;
            mage.DEX = 1;
            mage.INT = 5;
            mage.TypeName = "Mage";

            Unit archer = new Unit();
            archer.MaxHP = 10;
            archer.STR = 1;
            archer.DEX = 5;
            archer.INT = 1;
            archer.TypeName = "Archer";

            Unit fighter = new Unit();
            fighter.MaxHP = 20;
            fighter.STR = 4;
            fighter.DEX = 1;
            fighter.INT = 1;
            fighter.TypeName = "Fighter";

            Console.WriteLine(mage.ShowDetails());
            Console.WriteLine(archer.ShowDetails());
            Console.WriteLine(fighter.ShowDetails());

            //create you default unit
            Unit yourUnit = new Unit();
            Console.WriteLine("Copy from:\n1 mage\n2 archer\n3 fighter");
            string answ = Console.ReadLine();
            switch (answ)
            {
                case "1":
                    yourUnit = mage.Clone();
                    break;
                case "2":
                    yourUnit = archer.Clone();
                    break;
                case "3":
                    yourUnit = fighter.Clone();
                    break;
                default: throw new Exception("Invalid input!");
            }

            Console.WriteLine(yourUnit.ShowDetails());
            //change original
            Console.WriteLine("LVL up!");
            mage.INT++;
            mage.MaxHP += 5;
            archer.DEX++;
            archer.MaxHP += 5;
            fighter.STR++;
            fighter.MaxHP += 5;
            Console.WriteLine(mage.ShowDetails());
            Console.WriteLine(archer.ShowDetails());
            Console.WriteLine(fighter.ShowDetails());
            Console.WriteLine("Yout unit before LVL up");
            Console.WriteLine(yourUnit.ShowDetails());

            Console.WriteLine("LVL up stat:\n1 INT\n2 DEX\n3 STR");
            string answ2 = Console.ReadLine();
            switch (answ2)
            {
                case "1":
                    yourUnit.INT += 2;
                    
                    break;
                case "2":
                    yourUnit.DEX += 2 ;
                    break;
                case "3":
                    yourUnit.STR+=2;
                    break;
                default: throw new Exception("Invalid input!");
            }
            yourUnit.MaxHP += 10;
            Console.WriteLine("Yout unit after LVL up");
            Console.WriteLine(yourUnit.ShowDetails());
        }
    }
}
