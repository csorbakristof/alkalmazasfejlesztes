using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.AbstractFactory.Model
{
    /// <summary>
    /// "ProductA2" a mintában
    /// </summary>
    public class OrcArcher :IArcherUnit
    {
        public int LVL { get; private set; }
        public int MaxHP { get; private set; }
        public int STR { get; private set; }
        public int DEX { get; private set; }
        public int INT { get; private set; }

        public OrcArcher(int lvl)
        {
            LVL = lvl;
            STR = lvl +2;
            MaxHP = lvl * 5;
            DEX = lvl * 12;
            INT = lvl;
        }

        public string SaySomething()
        {
            return "I'm an Orc Archer";
        }

        public string ShowDetails()
        {
            return $"This is a level {LVL} orc archer with {MaxHP} maximum health, {STR} strength, {DEX} dexterity, {INT} intellect";
        }

        public string Shoot()
        {
            return $"Orc Archer shoots! ({DEX} dexterity)";
        }
    }
}
