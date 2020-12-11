using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceManagementGameCore.Buildings
{
    public class ProducerAndConverterBuilding : WorkplaceBuilding
    {
        public ProducerAndConverterBuilding(int time, int maxCapacity, Type singleUnit) : base(time)
        {
            MaxCapacity = maxCapacity;
            CurrentCapacity = 0;
            Units = new List<Unit>();
            UnitTypes = new List<Type>();
            UnitTypes.Add(singleUnit);
        }
        public ProducerAndConverterBuilding(int time, int maxCapacity, List<Type> listOfUnits) : base(time)
        {
            MaxCapacity = maxCapacity;
            CurrentCapacity = 0;
            Units = new List<Unit>();
            UnitTypes = new List<Type>(listOfUnits);
        }
        public void AddInput(ResourceAmount input)
        {
            foreach (var item in Units)
            {
                if (item is ConverterUnit)
                    try
                    {
                        var converterUnit = item as ConverterUnit;
                        converterUnit.AddInput(input);
                        break;//sikerült hozzáadni valamelyik unit inputjai-hoz
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Too many input for converter!")
                        {
                            if (item == Units.Last())
                            {
                                throw ex;//nem sikerült hozzáadni az input-ot és ez volt az utolsó unit az épületnél
                            }
                            continue; //nem sikerült hozzáadni az input-ot, nézzük a következő unit-ot
                        }

                        if (ex.Message == "Invalid input for converter!")
                            throw ex;
                    }
                else if(item is ProducerAndConverterUnit)
                    try
                    {
                        var producerAndConverterUnit = item as ProducerAndConverterUnit;
                        producerAndConverterUnit.AddInput(input);
                        break;//sikerült hozzáadni valamelyik unit inputjai-hoz
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Too many input for converter!")
                        {
                            if (item == Units.Last())
                            {
                                throw ex;//nem sikerült hozzáadni az input-ot és ez volt az utolsó unit az épületnél
                            }
                            continue; //nem sikerült hozzáadni az input-ot, nézzük a következő unit-ot
                        }

                        if (ex.Message == "Invalid input for converter!")
                            throw ex;
                    }
            }
        }
        public void RemoveInput(ResourceAmount input)
        {
            foreach (var item in Units)
            {
                if (item is ConverterUnit)
                    try
                    {
                        var converterUnit = item as ConverterUnit;
                        converterUnit.RemoveInput(input);
                        break;//sikerült elvenni valamelyik unit-tól ezt az input-ot
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Too many input remove from converter!")
                        {
                            if (item == Units.Last())
                            {
                                throw ex;//nem sikerült elvenni az input-ot és ez volt az utolsó unit az épületnél
                            }
                            continue; //nem sikerült elvenni az input-ot, nézzük a következő unit-ot
                        }

                        if (ex.Message == "Invalid input for converter!")
                            throw ex;
                    }
                else if (item is ProducerAndConverterUnit)
                    try
                    {
                        var producerAndConverterUnit = item as ProducerAndConverterUnit;
                        producerAndConverterUnit.RemoveInput(input);
                        break;//sikerült elvenni valamelyik unit-tól ezt az input-ot
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Too many input remove from converter!")
                        {
                            if (item == Units.Last())
                            {
                                throw ex;//nem sikerült elvenni az input-ot és ez volt az utolsó unit az épületnél
                            }
                            continue; //nem sikerült elvenni az input-ot, nézzük a következő unit-ot
                        }

                        if (ex.Message == "Invalid input for converter!")
                            throw ex;
                    }
            }
        }

        public override List<ResourceAmount> DoWork()
        {
            List<ResourceAmount> result = new List<ResourceAmount>();
            if (!AbleToFunction) return result;
            foreach (var item in Units)
            {
                if (item is ConverterUnit)
                {
                    var converterUnit = item as ConverterUnit;
                    result.AddRange(converterUnit.Convert());
                }
                if (item is ProducerUnit)
                {
                    var producerUnit = item as ProducerUnit;
                    result.AddRange(producerUnit.Produce());
                }
                if(item is ProducerAndConverterUnit)
                {
                    var producerAndConverterUnit = item as ProducerAndConverterUnit;
                    result.AddRange(producerAndConverterUnit.Convert());
                    result.AddRange(producerAndConverterUnit.Produce());
                }
            }
            return result;
        }
    }
}
