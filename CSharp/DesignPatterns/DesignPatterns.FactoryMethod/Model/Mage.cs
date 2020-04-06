using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.FactoryMethod.Model
{
    /// <summary>
    /// "Concrete product" a tervezési mintában
    /// </summary>
    public class Mage :IUnit
    {
        public int LVL { get; private set; }
        public int MaxHP { get; private set; }
        public int STR { get; private set; }
        public int DEX { get; private set; }
        public int INT { get; private set; }

        public Mage(int lvl)
        {
            LVL = lvl;
            STR = lvl;
            MaxHP = lvl * 5;
            DEX = lvl;
            INT = lvl *12;
        }

        public string SaySomething()
        {
            return "I'm a Mage";
        }

        public string ShowDetails()
        {
            return $"This is a level {LVL} mage with {MaxHP} maximum health, {STR} strength, {DEX} dexterity, {INT} intellect";
        }
    }
}
