using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MTrainStation : MProperty
{

    private int[] rents;

    public MTrainStation(string name, int morgage, int value, int[] irents)
        : base(name,value,morgage)
    {
        rents = irents;
    }

    protected override int Rent()
    {
        int amountOwend = MPropertys.AllProperteys.Values.Where(x => x.owner == owner && x is MTrainStation).Count();
        return rents[amountOwend];
    }

}
