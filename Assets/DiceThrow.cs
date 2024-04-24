using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class DiceThrow : MonoBehaviour
{
    public int diceResult;
    [SerializeField]
    Board Board;
    Die[] dice;
    private void Start()
    {
        dice = transform.GetComponentsInChildren<Die>();
    }
    public void Roll()
    {
        dice[0].launch();
        dice[1].launch();
    }
    public void AddResult(int result)
    {
        if (diceResult >= 0)
        {
            diceResult += result;
            Board.RollDone();
        } else
            diceResult += result;
    }
}
