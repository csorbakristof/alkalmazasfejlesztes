using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.AbstractFactory.Model
{
    /// <summary>
    /// "ProductB1" az abs. factory mintában
    /// </summary>
    public class ElfFighter :IFighterUnit
    {
        public int LVL { get; private set; }
        public int MaxHP { get; private set; }
        public int STR { get; private set; }
        public int DEX { get; private set; }
        public int INT { get; private set; }

        public ElfFighter(int lvl)
        {
            LVL = lvl;
            STR = lvl * 9;
            MaxHP = lvl * 8;
            DEX = lvl +2;
            INT = lvl;
        }

        public string SaySomething()
        {
            return "I'm an Elf Fighter";
        }

        public string ShowDetails()
        {
            return $"This is a level {LVL} elf fighter with {MaxHP} maximum health, {STR} strength, {DEX} dexterity, {INT} intellect";
        }
        public string Punch()
        {
            return $"Elf Fighter punches! ({STR} strength)";
        }
    }
}
