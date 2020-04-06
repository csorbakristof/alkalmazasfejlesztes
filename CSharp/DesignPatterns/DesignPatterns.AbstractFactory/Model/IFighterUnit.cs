using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.AbstractFactory.Model
{
    /// <summary>
    /// "Abs.ProductB" a mintában
    /// </summary>
    public interface IFighterUnit :IUnit
    {        
        public string Punch();
    }
}
