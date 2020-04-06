using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.FactoryMethod.Model
{
    /// <summary>
    /// "Concrete product" a tervezési mintában
    /// </summary>
    public class Fighter : IUnit
    {
        public int LVL { get; private set; }
        public int MaxHP { get; private set; }
        public int STR { get; private set; }
        public int DEX { get; private set; }
        public int INT { get; private set; }

        public Fighter(int lvl)
        {
            LVL = lvl;
            STR = lvl * 9;
            MaxHP = lvl * 8;
            DEX = lvl;
            INT = lvl;
        }

        public string SaySomething()
        {
            return "I'm a Fighter";
        }

        public string ShowDetails()
        {
            return $"This is a level {LVL} fighter with {MaxHP} maximum health, {STR} strength, {DEX} dexterity, {INT} intellect";
        }
    }
}
