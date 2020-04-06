using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.FactoryMethod.Model
{
    /// <summary>
    /// "Concrete product" a tervezési mintában
    /// </summary>
    public class Archer :IUnit
    {
        public int LVL { get; private set; }
        public int MaxHP { get; private set; }
        public int STR { get; private set; }
        public int DEX { get; private set; }
        public int INT { get; private set; }

        public Archer(int lvl)
        {
            LVL = lvl;
            STR = lvl;
            MaxHP = lvl * 5;
            DEX = lvl *12;
            INT = lvl;
        }

        public string SaySomething()
        {
            return "I'm an Archer";
        }

        public string ShowDetails()
        {
            return $"This is a level {LVL} archer with {MaxHP} maximum health, {STR} strength, {DEX} dexterity, {INT} intellect";
        }
    }
}
