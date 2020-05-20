using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.AbstractFactory.Model
{
    /// <summary>
    /// "ProductA1" az abs. factory mintában
    /// </summary>
    public class ElfArcher :IArcherUnit
    {
        public int LVL { get; private set; }
        public int MaxHP { get; private set; }
        public int STR { get; private set; }
        public int DEX { get; private set; }
        public int INT { get; private set; }

        public ElfArcher(int lvl)
        {
            LVL = lvl;
            STR = lvl;
            MaxHP = lvl * 5;
            DEX = lvl * 12 +2;
            INT = lvl;
        }

        public string SaySomething()
        {
            return "I'm an Elf Archer";
        }

        public string ShowDetails()
        {
            return $"This is a level {LVL} elf archer with {MaxHP} maximum health, {STR} strength, {DEX} dexterity, {INT} intellect";
        }
        public string Shoot()
        {
            return $"Elf Archer shoots! ({DEX} dexterity)";
        }
    }
}
