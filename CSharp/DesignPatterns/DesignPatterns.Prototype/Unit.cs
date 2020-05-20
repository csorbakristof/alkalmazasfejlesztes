using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Prototype
{
    public class Unit
    {
        public int LVL { get; }
        public int MaxHP { get; set; }
        public int STR { get; set; }
        public int DEX { get; set; }
        public int INT { get; set; }
        public string TypeName { get; set; }
        public Unit()
        {
            LVL = 1;
        }
        public string ShowDetails()
        {
            return $"This is a level {LVL} {TypeName} unit with {MaxHP} maximum health, {STR} strength, {DEX} dexterity, {INT} intellect";
        }
        public Unit Clone()
        {
            Unit cloneUnit = (Unit) this.MemberwiseClone();
            cloneUnit.TypeName = "Custom";
            return cloneUnit;
        }
    }
}
