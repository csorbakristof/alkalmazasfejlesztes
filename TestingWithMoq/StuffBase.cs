namespace TestingWithMoq
{
    // Base class for Stuff (to be tested)
    public class StuffBase
    {
        private ISource src;
        private ICommand cmd;
        public StuffBase(ISource src, ICommand cmd)
        {
            this.src = src;
            this.cmd = cmd;
        }

        public virtual void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void ExecuteCommandIfSourceIsNonzero()
        {
            if (src.GetValue() != 0)
                cmd.Execute();
        }

        public int GetValueFromInternalSource()
        {
            return src.GetValue();
        }
    }
}
