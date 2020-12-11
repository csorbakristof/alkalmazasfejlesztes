using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceManagementGameCore.Units
{
    public abstract class ConverterUnit : Unit
    {
        public Dictionary<string, List<ResourceAmount>>  InputOutputMapping { get; protected set; }
        public int MaxInput { get; protected set; }
        public ConverterUnit(int food, Dictionary<string, List<ResourceAmount>> mapping, int maxInput) : base(food)
        {
            MaxInput = maxInput;
            InputOutputMapping = new Dictionary<string, List<ResourceAmount>>();
            CurrentInput = new List<ResourceAmount>();
            foreach (var item in mapping)
            {
                InputOutputMapping.Add(item.Key, item.Value.Select(m => new ResourceAmount(m.Type, m.Amount)).ToList());
                CurrentInput.Add(new ResourceAmount(item.Key, 0));
            }
        }
        protected List<ResourceAmount> CurrentInput { get; set; }
        public virtual void AddInput(ResourceAmount input)
        {
            if (!CurrentInput.Any(i => i.Type == input.Type))
                throw new Exception("Invalid input for converter!");
            if (CurrentInput.Any(i => i.Amount + input.Amount > MaxInput))
                throw new Exception("Too many input for converter!");
            CurrentInput.Where(i => i.Type == input.Type).Single().Amount += input.Amount;
        }
        public virtual void RemoveInput(ResourceAmount input)
        {
            if (CurrentInput.SingleOrDefault(i => i.Type == input.Type) == null)
                throw new Exception("Invalid input remove from converter!");
            if (CurrentInput.Any(i => i.Type==input.Type && i.Amount - input.Amount <0))
                throw new Exception("Too many input remove from converter!");
            CurrentInput.Where(i => i.Type == input.Type).Single().Amount -= input.Amount;
        }
        public virtual List<ResourceAmount> Convert()
        { 
            List<ResourceAmount> result = new List<ResourceAmount>();
            if (this.Satisfaction==Satisfaction.Unsatisfied) return result;
            Dictionary<string, int> tmp = new Dictionary<string, int>();
            foreach (var item in CurrentInput.Where(i=>i.Amount>0))
            {
                var outputForOne = InputOutputMapping[item.Type];
                foreach (var resource in outputForOne)
                {
                    if (tmp.ContainsKey(resource.Type)) tmp[resource.Type] += (resource.Amount*item.Amount);
                    else tmp.Add(resource.Type, (resource.Amount * item.Amount));
                }
                item.Amount=0;
            }
            result.AddRange(tmp.Select(t => new ResourceAmount(t.Key, t.Value)).ToList());
            if(this.Satisfaction==Satisfaction.Ok)
                return result;
            result.ForEach(r => r.Amount *= 2);
            return result;
        }
    }
}
