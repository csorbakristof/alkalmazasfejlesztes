using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.AbstractFactory.Model
{
    /// <summary>
    /// "Abs.ProductA" a mintában
    /// </summary>
    public interface IArcherUnit :IUnit
    {
        public string Shoot();
    }
}
