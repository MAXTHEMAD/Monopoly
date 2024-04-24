using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MBuilding : MProperty
{
    public colour Suite;
    private int buildingPrice;
    private int buildingLevel;
    private int[] rents;
    
    public MBuilding(string name, int value, int morgage, colour colour, int ibuildingPrice, int[] irents)
        : base(name,value,morgage)
    {                                  
        buildingPrice = ibuildingPrice;
        buildingPrice = ibuildingPrice;
        rents = irents;
        Suite = colour;

        buildingLevel = 0;
    }

    protected override int Rent()
    {
        if (buildingLevel == 0) {
            int amountOwend = MPropertys.AllProperteys.Values.Where(x => x.owner == owner && x is MBuilding && ((MBuilding)x).Suite == Suite).Count();
            int amountTotal = MPropertys.AllProperteys.Values.Where(x => x is MBuilding && ((MBuilding)x).Suite == Suite).Count();
            return rents[buildingLevel] * (amountOwend == amountTotal ? 2 : 1);
        }
        return rents[buildingLevel];
    }

    public enum colour
    {
        Red,Yellow,Green,Blue,LightBlue,Pink,Purple,Orange
    }
}
