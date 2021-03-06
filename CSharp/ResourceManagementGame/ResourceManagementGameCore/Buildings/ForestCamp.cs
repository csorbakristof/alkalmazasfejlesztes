﻿using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using ResourceManagementGameCore.Units.GathererState;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Buildings
{
    public class ForestCamp : ProducerBuilding
    {
        public ForestCamp(int max, int time) : base(max, time, typeof(Gatherer))
        {
        }
        public override void AssignUnit(Unit unit)
        {
            base.AssignUnit(unit); //típus validálás
            Gatherer gatherer = unit as Gatherer;
            gatherer.SwitchState(new WoodcutterState());
        }
        public override Unit RemoveLastUnit()
        {
            var unit = base.RemoveLastUnit(); //elkérni a konkrét unit-ot
            Gatherer gatherer = unit as Gatherer;
            gatherer.SwitchState(new FreeState());
            return unit;
        }
        public override List<ResourceAmount> DoWork()
        {
            return base.DoWork();
        }
    }
}
