using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class ChanceCards
{
    public const int TOTAL = 30;
    public static bool Chance(int card, byte player)
    {
        Board board = GameObject.Find("Board").GetComponent<Board>();
        int bill;
        switch (card)
        {
            case 0:
                board.BankTransfer(50);
                break;
            case 1:
                board.pieces[player].SetPosition(5);
                break;
            case 2:
                board.pieces[player].SetPosition((byte)(board.pieces[player].position < 20 ? 12 : 28));
                return true;
            case 3:
                bill = 0;
                foreach(MBuilding mBuilding in MPropertys.AllProperteys.Values.Where(x => x.owner == player && x is MBuilding && ((MBuilding)x).buildingLevel > 0).ToArray())
                {
                    bill += mBuilding.buildingLevel == 5 ? 100 : mBuilding.buildingLevel * 25;
                }
                board.BankTransfer(bill);
                break;
            case 4:
                board.pieces[player].SetPosition(Rail(board.pieces[player].position));
                return true;
            case 5:
                board.BankTransfer(-15);
                break;
            case 6:
                board.pieces[player].SetPosition((byte)(board.pieces[player].position - 3));
                break;
            case 7:
                for(byte i = 0; i < 4; i++)
                {
                    if (i == player)
                        continue;
                    board.BankTransfer(player, 50, i);
                }
                break;
            case 8:
                board.pieces[player].SetPosition(24);
                break;
            case 9:
                board.pieces[player].SetPosition(0);
                board.BankTransfer(200);
                break;
            case 10:
                board.pieces[player].imprisoned = true;
                board.pieces[player].SetPosition(40);
                board.pieces[player].threeTimer = 0;
                break;
            case 11:
                board.BankTransfer(150);
                break;
            case 12:
                board.pieces[player].SetPosition(39);
                break;
            case 13:
                board.pieces[player].SetPosition(Rail(board.pieces[player].position));
                return true;
            case 14:
                if(board.pieces[player].position > 11)
                    board.BankTransfer(200);
                board.pieces[player].SetPosition(11);
                break;
            case 15:
                board.BankTransfer(-100);
                break;
            case 16:
                board.BankTransfer(100);
                break;
            case 17:
                board.BankTransfer(100);
                break;
            case 18:
                board.BankTransfer(-150);
                break;
            case 19:
                board.pieces[player].imprisoned = true;
                board.pieces[player].SetPosition(40);
                board.pieces[player].threeTimer = 0;
                break;
            case 20:
                board.BankTransfer(45);
                break;
                    
            case 21:
                bill = 0;
                foreach (MBuilding mBuilding in MPropertys.AllProperteys.Values.Where(x => x.owner == player && x is MBuilding && ((MBuilding)x).buildingLevel > 0).ToArray())
                {
                    bill += mBuilding.buildingLevel == 5 ? 115 : mBuilding.buildingLevel * 40;
                }
                board.BankTransfer(bill);
                break;
            case 22:
                for (byte i = 0; i < 4; i++)
                {
                    if (i == player)
                        continue;
                    board.BankTransfer(i, 50, player);
                }
                break;
            case 23:
                board.BankTransfer(20);
                break;
            case 24:
                board.BankTransfer(200);
                break;
            case 25:
                board.BankTransfer(10);
                break;
            case 26:
                board.BankTransfer(-50);
                break;
            case 27:
                board.BankTransfer(100);
                break;
            case 28:
                board.pieces[player].SetPosition(0);
                board.BankTransfer(200);
                break;
            case 29:
                board.BankTransfer(25);
                break;
            
            
        }
        return false;
    }
    static byte Rail(byte pos)
    {
        if (pos < 10)
            return 5;
        else if (pos < 20)
            return 15;
        else if (pos < 30)
            return 25;
        else
            return 35;
    }
}
