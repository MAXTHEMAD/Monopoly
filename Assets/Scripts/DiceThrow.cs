using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class DiceThrow : MonoBehaviour
{
    public int diceResult;
    public int diceResulta;
    public int diceResultb;
    [SerializeField]
    GameObject rollCam;
    Board Board;
    Die[] dice;
    private void Start()
    {
        Board = GameObject.Find("Board").GetComponent<Board>();
        dice = transform.GetComponentsInChildren<Die>();
    }
    public void Roll()
    {
        diceResult = 0;
        dice[0].launch();
        dice[1].launch();
        
    }
    public void AddResult(int result)
    {
        if (diceResult > 0)
        {
            diceResultb = result;
            diceResult += result;
            rollCam.SetActive(false);
            Board.RollDone();
        }
        else
        {
            diceResulta = result;
            diceResult += result;
        }
    }
}
