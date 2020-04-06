namespace DesignPatterns.AbstractFactory.Model
{
    /// <summary>
    /// "Abs.Factory" a mintában
    /// </summary>
    public abstract class UnitFactory
    {
        public abstract IArcherUnit CreateArcherUnit(int lvl);
        public abstract IFighterUnit CreateFighterUnit(int lvl);
    }
}