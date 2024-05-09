using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MBuilding : MProperty
{
    public colour Suite;
    public int buildingPrice;
    public int buildingLevel;
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

    /// <summary>
    /// Will upgrade building level regardless of legality
    /// </summary>
    /// <returns></returns>
    public int Upgrade()
    {
        if (morgaged)
        {
            morgaged = false;
            return (int)(Morgage * 1.1f);
        }
        buildingLevel++;
        return buildingPrice;
    }

    /// <summary>
    /// Will downgrade building level regardless of legality
    /// </summary>
    /// <returns></returns>
    public int DownGrade()
    {
        if (buildingLevel == 0)
        {
            morgaged = true;
            return Morgage;
        }

        buildingLevel--;
        return buildingPrice / 2;
    }

    public bool Upgradeable()
    {
        if (buildingLevel == 5)
            return false;
        int amountOwend = MPropertys.AllProperteys.Values.Where(x => x.owner == owner && x is MBuilding && ((MBuilding)x).Suite == Suite).Count();
        MProperty[] buildings = MPropertys.AllProperteys.Values.Where(x => x is MBuilding && ((MBuilding)x).Suite == Suite).ToArray();
        int amountTotal = buildings.Count();
        if (amountOwend != amountTotal)
            return false;
        foreach (MBuilding b in buildings)
        {
            if (b == this)
                continue;
            if (b.buildingLevel < buildingLevel)
                return false;
        }
        return true;
    }
    public bool DownGradeable()
    {
        if(morgaged)
            return false;
        if(buildingLevel == 0)
            return true;
        int amountOwend = MPropertys.AllProperteys.Values.Where(x => x.owner == owner && x is MBuilding && ((MBuilding)x).Suite == Suite).Count();
        MProperty[] buildings = MPropertys.AllProperteys.Values.Where(x => x is MBuilding && ((MBuilding)x).Suite == Suite).ToArray();
        int amountTotal = buildings.Count();
        if (amountOwend != amountTotal)
            return false;
        foreach (MBuilding b in buildings)
        {
            if (b == this)
                continue;
            if (b.buildingLevel > buildingLevel)
                return false;
        }
        return true;
    }


    public enum colour
    {
        Red,Yellow,Green,Blue,LightBlue,Pink,Purple,Orange
    }
}
