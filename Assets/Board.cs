using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    [SerializeField]
    DiceThrow diceThrow;
    [SerializeField]
    BoardPiece[] pieces;
    [SerializeField]
    GameObject propertyCard;

    private byte turn;
    int[] bankAccounts;


    public void StartGame()
    {
        bankAccounts = new int[4] { 1500, 1500, 1500, 1500 };
        turn = 1;
    }

    public bool AnythingOnSpot(byte position)
    {
        return MPropertys.AllProperteys[position].owner != 0;
    }

    private void Start()
    {
        StartGame();
        /*Debug.Log(AnythingOnSpot(11));
        Debug.Log(MPropertys.AllProperteys[11].owner);
        MProperty property = MPropertys.AllProperteys[11];
        Debug.Log(property.owner);
        BuyProperty();
        Debug.Log(MPropertys.AllProperteys[11].owner);
        Debug.Log(property.owner);*/
        pieces[turn-1].AddPosition(11);
        propertyCard.GetComponent<MeshRenderer>().material = Resources.Load<Material>(pieces[turn-1].position.ToString());
        Debug.Log(pieces[turn].position.ToString());
        Debug.Log(pieces[turn].position);
        /*pieces[turn].position = 8;
        propertyCard.GetComponent<MeshRenderer>().material = Resources.Load<Material>(pieces[turn - 1].position.ToString());
        Debug.Log(pieces[turn].position.ToString());
        Debug.Log(pieces[turn].position);*/
    }

    public void MovePiece(bool quick = false)
    {
        if (quick)
        {
            diceThrow.diceResult = Random.Range(1, 7) + Random.Range(1, 7);
            RollDone();
        } else
        {
            diceThrow.Roll();
        }
    }

    public void RollDone()
    {
        pieces[turn-1].AddPosition((byte)diceThrow.diceResult);
        if (MPropertys.AllProperteys.ContainsKey(pieces[turn - 1].position)) //is it a property?
        {
            if (AnythingOnSpot(pieces[turn - 1].position)) //does anyone own it?
            {
                MProperty property = MPropertys.AllProperteys[pieces[turn-1].position];
                if (property.owner != turn) //do i own it?
                {
                    if (bankAccounts[turn] >= property.rent)
                    {
                        bankAccounts[turn] -= property.rent;
                        bankAccounts[property.owner] += property.rent;
                    }
                    else
                        CantAffordRent(property);
                }
            } else {
                if (bankAccounts[turn] >= MPropertys.AllProperteys[pieces[turn-1].position].Price) //Can i afford it?
                {
                    offerProperty(MPropertys.AllProperteys[pieces[turn - 1].position]);
                }
            }
        }

        
    }


    void offerProperty(MProperty property)
    {
        propertyCard.GetComponent<MeshRenderer>().materials[0] = (Material)Resources.Load(pieces[turn].position.ToString());
        propertyCard.GetComponent<Animator>().SetTrigger("Show");
        GameObject.Find("Canvas").transform.Find("BuyingPanel").gameObject.SetActive(true);
    }

    public void BuyProperty()
    {
        MProperty property = MPropertys.AllProperteys[pieces[turn].position];
        bankAccounts[turn] -= property.Price;
        property.owner = turn;
    }

    void CantAffordRent(MProperty property)
    {

    }

    public void NextTurn()
    {
        if (bankAccounts[turn] <= 0)
        {

        }
    }


    [SerializeField]
    List<GameObject> hast;

    private void piecePlacmentExample()
    {
        GameObject clone;
        for (int i = 0; i < 42; i++)
        {
            clone = Instantiate(hast[0], transform);
            clone.transform.localPosition = BoardStatic.GetPiecePosition((byte)i, 0);
            clone.transform.localScale = Vector3.one * 2.5f;
            clone = Instantiate(hast[1], transform);
            clone.transform.localPosition = BoardStatic.GetPiecePosition((byte)i, 1);
            clone.transform.localScale = Vector3.one * 2.5f;
            clone = Instantiate(hast[2], transform);
            clone.transform.localPosition = BoardStatic.GetPiecePosition((byte)i, 2);
            clone.transform.localScale = Vector3.one * 2.5f;
            clone = Instantiate(hast[3], transform);
            clone.transform.localPosition = BoardStatic.GetPiecePosition((byte)i, 3);
            clone.transform.localScale = Vector3.one * 2.5f;
        }
    }
}        
