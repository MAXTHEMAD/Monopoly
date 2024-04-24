using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class MPropertys
{
    public static Dictionary<byte, MProperty> AllProperteys = new Dictionary<byte, MProperty>()
    {
        //key                   name             price  morgage                 buildingprice                  rents
        {61, new MBuilding("Pune",              260,150,MBuilding.colour.Yellow,130,new int[6] {22,110,330,800,975,1150})},
        {63, new MBuilding("Jaipur",          240,150, MBuilding.colour.Red,  120,new int[6] {20,100,300,750,925,1100})},
        {64, new MBuilding("Chandigarh",  220,150, MBuilding.colour.Red,  110,new int[6] {18,90,250,700,875,1050})},
        {1, new MBuilding("Guwahati",      60,30, MBuilding.colour.Purple, 50, new int[6] {2,10,30,90,160,250})},
        {3, new MBuilding("Bhubaneshwar",60,30, MBuilding.colour.Purple,110,new int[6]{4,20,60,180,320,450})},
        {5, new MTrainStation("Chennai Train station",100,50,                   new int[6] {6,30,90,270,400,550})},
        {6, new MBuilding("Panaji",          100,50,MBuilding.colour.LightBlue,50,new int[6]{6,30,90,270,400,550})},
        {8, new MBuilding("Agra",              100,50,MBuilding.colour.LightBlue,50,new int[6]{6,30,90,270,400,550})},
        {9, new MBuilding("Vadodara",      120,60,MBuilding.colour.LightBlue,50,new int[6]{8,40,100,300,450,600})},
        {11, new MBuilding("Ludhiana",      140,70, MBuilding.colour.Pink,  100,new int[6] {10,50,150,450,625,750})},
        {12, new MUtility("Electric Company",150,75)},
        {59, new MBuilding("Jaipur",          240,150, MBuilding.colour.Red,  110,new int[6] {18,90,250,700,875,1050})},
        {58, new MBuilding("Jaipur",          240,150, MBuilding.colour.Red,  110,new int[6] {18,90,250,700,875,1050})},
        {57, new MBuilding("Jaipur",          240,150, MBuilding.colour.Red,  110,new int[6] {18,90,250,700,875,1050})},
        {60, new MBuilding("Jaipur",          240,150, MBuilding.colour.Red,  110,new int[6] {18,90,250,700,875,1050})}                   


    };

}
