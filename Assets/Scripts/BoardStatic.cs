using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardStatic
{
    public static Vector3 GetPosition(byte boardPlacement)
    {
        if (boardPlacement == 0) return new Vector3(-4.35f, 0, 4.35f);
        if (boardPlacement < 10)
        {
            return new Vector3(-3.27f + (0.8175f * (boardPlacement - 1)), 0, 4.5f);
        }
        if (boardPlacement == 10) return new Vector3(4.8f, 0, 4.8f);
        if (boardPlacement < 20) 
        {
            return new Vector3(4.5f, 0, 3.27f - (0.8175f * (boardPlacement - 11)));
        }
        if (boardPlacement == 20) return new Vector3(4.35f, 0, -4.35f);
        if (boardPlacement < 30)
        {
            return new Vector3(3.27f - (0.8175f * (boardPlacement - 21)), 0, -4.5f);
        }
        if (boardPlacement == 30) return new Vector3(-4.35f, 0, -4.35f);
        if (boardPlacement < 40)
        {
            return new Vector3(-4.5f, 0, -3.27f + (0.8175f * (boardPlacement - 31)));
        }
        if (boardPlacement == 40)
        {
            return new Vector3(4.155f, 0, 4.155f);
        }
        return Vector3.zero;
    }
    
    public static Vector3 GetPiecePosition(byte boardPlacement, byte piece)
    {
        Vector3 returnVec = GetPosition(boardPlacement);
        if (boardPlacement == 10)
        {
            switch (piece)
            {
                case 0:
                    return returnVec + new Vector3(-0.8f, 0, 0);
                case 1:
                    return returnVec + new Vector3(-0.4f, 0, 0);
                case 2:
                    return returnVec + new Vector3(0, 0, -0.4f);
                case 3:
                    return returnVec + new Vector3(0, 0, -0.8f);
                default:
                    return returnVec;
            }
        }
        switch (piece)
        {
            case 0:
                return returnVec + new Vector3(0.2f, 0, 0.2f);
            case 1:
                return returnVec + new Vector3(0.2f, 0,-0.2f);
            case 2:
                return returnVec + new Vector3(-0.2f, 0, -0.2f);
            case 3:
                return returnVec + new Vector3(-0.2f, 0, 0.2f);
            default:
                return returnVec;
        }
    }
}
