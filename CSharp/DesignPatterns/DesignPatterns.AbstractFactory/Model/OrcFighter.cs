using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.AbstractFactory.Model
{
    /// <summary>
    /// "ProductB2" a mintában
    /// </summary>
    public class OrcFighter :IFighterUnit
    {
        public int LVL { get; private set; }
        public int MaxHP { get; private set; }
        public int STR { get; private set; }
        public int DEX { get; private set; }
        public int INT { get; private set; }

        public OrcFighter(int lvl)
        {
            LVL = lvl;
            STR = lvl * 9+2;
            MaxHP = lvl * 8;
            DEX = lvl;
            INT = lvl;
        }

        public string SaySomething()
        {
            return "I'm an Orc Fighter";
        }

        public string ShowDetails()
        {
            return $"This is a level {LVL} orc fighter with {MaxHP} maximum health, {STR} strength, {DEX} dexterity, {INT} intellect";
        }
        public string Punch()
        {
            return $"Orc Fighter punches! ({STR} strength)";
        }
    }
}
