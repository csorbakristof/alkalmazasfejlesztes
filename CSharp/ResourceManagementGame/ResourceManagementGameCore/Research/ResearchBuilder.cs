using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Research
{
    public class ResearchBuilder
    {
        public RootResearchItem Root { get; }
        public ResearchItem CurrentItem { get; set; }
        public ResearchBuilder()
        {
            Root = new RootResearchItem();
            CurrentItem = Root;
        }
        public void AddLeaf(ResearchType type, Type unlockOn, IResearchParameter parameters)
        {
            if (CurrentItem is LeafResearchItem)
                throw new Exception("cannot add to leaf node");
            if (CurrentItem is RootResearchItem)
                ((RootResearchItem)CurrentItem).AddChildUnlock(new LeafResearchItem(type, unlockOn, parameters));
            else
                ((CompositeResearchItem)CurrentItem).AddChildUnlock(new LeafResearchItem(type, unlockOn, parameters));
        }
        public void AddNode(ResearchType type, Type unlockOn, IResearchParameter parameters)
        {
            if (CurrentItem is LeafResearchItem)
                throw new Exception("cannot add to leaf node");
            if (CurrentItem is RootResearchItem)
                ((RootResearchItem)CurrentItem).AddChildUnlock(new CompositeResearchItem(type, unlockOn, parameters));
            else
                ((CompositeResearchItem)CurrentItem).AddChildUnlock(new CompositeResearchItem(type, unlockOn, parameters));
        }
        public void ChangeToUpper()
        {
            if (CurrentItem != Root)
                CurrentItem = CurrentItem.Parent;
        }
        public void ChangeToLower(int index)
        {
            if (CurrentItem is LeafResearchItem)
                return;
            try
            {
                if (CurrentItem is RootResearchItem)
                    CurrentItem = ((RootResearchItem)CurrentItem).GetChildUnlocks()[index];
                else
                    CurrentItem = ((CompositeResearchItem)CurrentItem).GetChildUnlocks()[index];
            }
            catch
            {
                throw new Exception("cannot nacigate to child node");
            }
        }

    }
}
