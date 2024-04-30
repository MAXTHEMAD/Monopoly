using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MProperty
{
    public string Name { get; }
    //public readonly string Name;
    public int Price { get; }
    public int Morgage { get; }

    public int rent { get => Rent(); }

    public int owner = -1;

    public bool morgaged;
    public MProperty() { }
    public MProperty(string name, int value, int morgage)
    {
        Name = name;
        Price = value;
        Morgage = morgage;
    }



    protected virtual int Rent()
    {
        return Price;
    }

    public enum BuildingType
    {
        publicWorks,
    }
}
