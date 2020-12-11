using ResourceManagementGameCore.Factories;
using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Research
{
    public abstract class ResearchItem
    {
        public Type UnlockOn { get; protected set; }
        public ResearchItem Parent { get;  set; }
        protected ResearchType ResearchType;
        public bool IsApplied { get; protected set; }
        public void Unlock(object applyTo)
        {
            if (applyTo.GetType() != UnlockOn)
                throw new Exception("Invalid research unlock on type");
            if (IsApplied)
                return;
            Apply(applyTo);
            IsApplied = true;
            UnitFactory.ModifyFutureUnitsWithResearch(UnlockOn, this);
        }
        public void Apply(object applyTo)
        {
            switch (ResearchType)
            {
                case (ResearchType.ProducerAmountBoost):
                    if (applyTo is IProducerAmountUpgradeable)
                    {
                        var tmp = (IProducerAmountUpgradeable)applyTo;
                        var p = parameters as ProducerAmountParameters;
                        tmp.UpgradeProducerAmount(p.Resource);
                    }
                    break;
                case (ResearchType.ConverterInputResourceUnlock):
                    if (applyTo is IConverterInputResourceUpgradeable)
                    {
                        var tmp = (IConverterInputResourceUpgradeable)applyTo;
                        var p = parameters as ConverterInputResourceParameters;
                        tmp.UpgradeConverterOutputResource(p.ResourceName);
                    }
                    break;
                case (ResearchType.ProducerResourceUnlock):
                    if (applyTo is IProducerProductsUpgradeable)
                    {
                        var tmp = (IProducerProductsUpgradeable)applyTo;
                        var p = parameters as ProducerProductParameters;
                        tmp.UpgradeProducerProduct(p.ResourceName);
                    }
                    break;
                case (ResearchType.ConverterOutputUnlock):
                    if (applyTo is IConverterOutputForInputResourceUpgradeable)
                    {
                        var tmp = (IConverterOutputForInputResourceUpgradeable)applyTo;
                        var p = parameters as ConverterOutputForInputResourceParameter;
                        tmp.UpgradeConverterInputMapping(p.InputResourceName, p.OutputResources);
                    }
                    break;
            }
        }
        private IResearchParameter parameters;
        public ResearchItem(ResearchType type, Type unlockOn, IResearchParameter parameters)
        {
            switch (type)
            {
                case (ResearchType.ConverterInputResourceUnlock):
                    if (!(parameters is ConverterInputResourceParameters))
                        throw new Exception("Invalid research type and parameter pairing");
                    break;
                case (ResearchType.ProducerAmountBoost):
                    if (!(parameters is ProducerAmountParameters))
                        throw new Exception("Invalid research type and parameter pairing");
                    break;
                case (ResearchType.ProducerResourceUnlock):
                    if (!(parameters is ProducerProductParameters))
                        throw new Exception("Invalid research type and parameter pairing");
                    break;
                case (ResearchType.ConverterOutputUnlock):
                    if (!(parameters is ConverterOutputForInputResourceParameter))
                        throw new Exception("Invalid research type and parameter pairing");
                    break;
            }
            ResearchType = type;
            UnlockOn = unlockOn;
            IsApplied = false;
            this.parameters = parameters;
        }
        public bool Contains(ResearchItem research)
        {
            if (this == research)
                return true;
            if (this is CompositeResearchItem)
                foreach (var item in ((CompositeResearchItem)this).GetChildUnlocks())
                {
                    var result = item.Contains(research);
                    if (result) return result;
                }
            return false;
        }
    }
    public class RootResearchItem : ResearchItem
    {
        private List<ResearchItem> childUnlocks;
        public RootResearchItem()
            : base(ResearchType.ProducerAmountBoost,
                  null, new ProducerAmountParameters(null))
        {
            IsApplied = true;
            childUnlocks = new List<ResearchItem>();
            Parent = null;
        }
        public void AddChildUnlock(ResearchItem child)
        {
            child.Parent = this;
            childUnlocks.Add(child);
        }
        public List<ResearchItem> GetChildUnlocks()
        {
            return childUnlocks;
        }
    }
    public class LeafResearchItem : ResearchItem
    {
        public LeafResearchItem(ResearchType type, Type unlockOn, IResearchParameter parameters) : base(type, unlockOn, parameters)
        {
        }
    }
    public class CompositeResearchItem : ResearchItem
    {
        private List<ResearchItem> childUnlocks;
        public CompositeResearchItem(ResearchType type,Type unlockOn, IResearchParameter parameters) :base(type, unlockOn, parameters)
        {
            childUnlocks = new List<ResearchItem>();
        }
        public void AddChildUnlock(ResearchItem child)
        {
            child.Parent = this;
            childUnlocks.Add(child);
        }
        public List<ResearchItem> GetChildUnlocks()
        {
            return childUnlocks;
        }
    }
}
