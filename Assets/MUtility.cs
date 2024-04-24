using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MUtility : MProperty
{
    public MUtility(string name, int value, int morgage)
        : base(name,value,morgage)
    {
        
    }

    protected override int Rent()
    {
        int amountOwend = MPropertys.AllProperteys.Values.Where(x => x.owner == owner && x is MUtility).Count();

        return amountOwend == 1 ? GameObject.Find("CardTable").GetComponent<DiceThrow>().diceResult * 5 : GameObject.Find("CardTable").GetComponent<DiceThrow>().diceResult * 10;
    }
}
