using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class MPropertys
{
    public static Dictionary<byte, MProperty> AllProperteys = new Dictionary<byte, MProperty>()
    {
        //key                   name             price  morgage                 buildingprice                  rents
        
        
        {1, new MBuilding("Guwahati",         60,30, MBuilding.colour.Purple, 50, new int[6] {2,10,30,90,160,250})},
        {3, new MBuilding("Bhubaneshwar",     60,30, MBuilding.colour.Purple,110,new int[6]{4,20,60,180,320,450})},
        {5, new MTrainStation("Chennai Train station",100,50,                   new int[4] {25,50,100,200})},
        {6, new MBuilding("Panaji",          100,50,MBuilding.colour.LightBlue,50,new int[6]{6,30,90,270,400,550})},
        {8, new MBuilding("Agra",              100,50,MBuilding.colour.LightBlue,50,new int[6]{6,30,90,270,400,550})},
        {9, new MBuilding("Vadodara",         120,60,MBuilding.colour.LightBlue,50,new int[6]{8,40,100,300,450,600})},
        {11, new MBuilding("Ludhiana",        140,70, MBuilding.colour.Pink,  100,new int[6] {10,50,150,450,625,750})},
        {12, new MUtility("Electric Company", 150,75)},
        {13, new MBuilding("Patma",           140,70, MBuilding.colour.Pink,  100,new int[6] {10,50,150,450,625,750})},
        {14, new MBuilding("Bhopal",          160,80, MBuilding.colour.Pink,  100,new int[6] {12,60,180,500,700,900})},
        {15, new MTrainStation("Howrah Train station",200,100,                   new int[4] {25, 50, 100, 200 })},
        {16, new MBuilding("Indore",          180,90, MBuilding.colour.Orange,100,new int[6] {14,70,200,550,750,950})},
        {18, new MBuilding("Nagpur",          180,90, MBuilding.colour.Orange,100,new int[6] {14,70,200,550,750,950})},
        {19, new MBuilding("Kochi",           200,100,MBuilding.colour.Orange,100,new int[6] {16,80,220,600,800,1000})},
        {21, new MBuilding("Lucknow",         220,120, MBuilding.colour.Red,  150,new int[6] {18,90,250,700,575,1050})},
        {23, new MBuilding("Chandigarh",      220,150, MBuilding.colour.Red,  110,new int[6] {18,90,250,700,875,1050})},
        {24, new MBuilding("Jaipur",          240,150, MBuilding.colour.Red,  120,new int[6] {20,100,300,750,925,1100})},
        {25, new MTrainStation("New Delhi Train station",200,100,                   new int[4] {25, 50, 100, 200 })},
        {26, new MBuilding("Pune",            260,130,MBuilding.colour.Yellow,150,new int[6] {22,110,330,800,975,1150})},
        {27, new MBuilding("Hyderabad",       260,130,MBuilding.colour.Yellow,150,new int[6] {22,110,330,800,975,1150})},
        {28, new MUtility("Water Works",      150,75)},
        {29, new MBuilding("Ahmedabad",       280,140,MBuilding.colour.Yellow,150,new int[6] {24,120,360,850,1025,1200})},
        {31, new MBuilding("Kolkata",         300,150, MBuilding.colour.Green,200,new int[6] {26,130,390,900,1100,1275})},
        {32, new MBuilding("Chennai",         300,150, MBuilding.colour.Green,200,new int[6] {26,130,390,900,1100,1275})},
        {34, new MBuilding("Bengaluru",       320,160, MBuilding.colour.Green,200,new int[6] {28,150,390,900,1200,1400})},
        {35, new MTrainStation("Chhatrapati Shivaji Terminus Train station",200,100,new int[4] {25, 50, 100, 200 })},
        {37, new MBuilding("Delhi",           350,175, MBuilding.colour.Blue, 200,new int[6] {35,175,500,1100,1300,1500})},
        {39, new MBuilding("Mumbai",          400,200, MBuilding.colour.Blue, 200,new int[6] {50,175,500,1100,1700,2000})}
    };

}
