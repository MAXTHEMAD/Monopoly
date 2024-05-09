using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEngine.UI.GridLayoutGroup;

public class Board : MonoBehaviour
{
    [SerializeField]
    DiceThrow diceThrow;
    [SerializeField]
    public BoardPiece[] pieces;
    [SerializeField]
    GameObject[] chips;
    [SerializeField]
    GameObject propertyCard;
    [SerializeField]
    GameObject chanceCard;

    public string state = "active";

    
    private byte turn;
    private int turnCount;
    int[] bankAccounts;
    readonly byte[]  chance = {7,22,36};
    readonly byte[] chest =   {2,17,33};

    TextMeshProUGUI[] moneyUI;
    GameObject propertyPanelobj;
    Canvas canvas;
    Chip chip;
    public void StartGame()
    {
        bankAccounts = new int[4] {1500, 1500, 1500, 1500};
        turn = 0;
        turnCount = 1;
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        moneyUI = GameObject.Find("Money").GetComponentsInChildren<TextMeshProUGUI>();
        propertyPanelobj = canvas.transform.Find("PropertyPanel").gameObject;
        StartGame();

        /*  Usefull setup code for testing
        BoardPiece current = pieces[turn];
        current.TelelportPosition(6);
        BuyProperty();
        current.TelelportPosition(8);
        BuyProperty();
        current.TelelportPosition(9);
        BuyProperty();

        turn = 3;
        current = pieces[turn];
        current.TelelportPosition(16);
        BuyProperty();
        current.TelelportPosition(13);
        BuyProperty();
        current.TelelportPosition(39);
        BuyProperty();
        turn = 0;
        current = pieces[turn];

        current.TelelportPosition(37);
        BuyProperty();
        current.TelelportPosition(16);
        BuyProperty();
        current.TelelportPosition(29);
        BuyProperty();
        state = "idle";
        chip = transform.Find("Chips").GetComponentInChildren<Chip>();
        UpgradeProperty();

        bankAccounts[turn] = -89;
        */
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (state == "idle" || state == "poor")
            {
                RaycastHit hit;
                Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(r, out hit))
                {
                    Debug.Log(hit.transform.name);
                    if (hit.transform.GetComponent<Chip>() != null) {
                        Transform objectHit = hit.transform;        
                        byte owner = byte.Parse(objectHit.name[0].ToString());
                        if (owner == turn)
                        {
                            chip = objectHit.GetComponent<Chip>();
                            showCard(chip.pos);
                            canvas.transform.Find("Turn").gameObject.SetActive(false);
                            PropertyPanel();
                        }
                        else
                        {
                            chip = objectHit.GetComponent<Chip>();
                            if (((MBuilding)MPropertys.AllProperteys[chip.pos]).buildingLevel == 0)
                            {
                                canvas.transform.Find("Turn").gameObject.SetActive(false);
                                canvas.transform.Find("Trade").gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
    }

    public void MovePiece(bool quick = false)
    {
        state = "active";
        if (quick)
        {
            diceThrow.diceResulta = UnityEngine.Random.Range(1, 7);
            diceThrow.diceResultb = UnityEngine.Random.Range(1, 7);
            diceThrow.diceResult = diceThrow.diceResulta + diceThrow.diceResultb;
            RollDone();
        } else
        {
            diceThrow.Roll();
        }
    }

    public void RollDone()
    {
        Debug.Log("rolldone");
        Debug.Log(turn);
        if (!pieces[turn].imprisoned)
        {
            if (pieces[turn].position + diceThrow.diceResult > 39)
                bankAccounts[turn] += 200;
            if(diceThrow.diceResulta == diceThrow.diceResultb)
            {
                pieces[turn].threeTimer++;
                
            }
            if (pieces[turn].threeTimer == 3)
            {
                Debug.Log("TurnTimer");
                pieces[turn].imprisoned = true;
                pieces[turn].SetPosition(40);
                pieces[turn].threeTimer = 0;
                NextTurn();
            }
            else
                pieces[turn].AddPosition((byte)diceThrow.diceResult);
        } else
        {
            if(pieces[turn].threeTimer == 2 || diceThrow.diceResulta == diceThrow.diceResultb)
            {
                pieces[turn].imprisoned = false;
                pieces[turn].SetPosition(10);
                pieces[turn].threeTimer = 0;
                NextTurn();
            }
            else
            {
                pieces[turn].threeTimer++;
                canvas.transform.Find("Bail").gameObject.SetActive(true);
                canvas.transform.Find("Turn").gameObject.SetActive(true);
            }
        }

    }

    public void WhatsOnPos()
    {
        byte position = pieces[turn].position;
        if (MPropertys.AllProperteys.ContainsKey(position)) //is it a property?
        {
            Debug.Log("property");
            MProperty property = MPropertys.AllProperteys[position];
            if (property.owner != -1) //does anyone own it?
            {
                if (property.owner != turn && !property.morgaged) //do i own it? and is it morgaged?
                {
                    BankTransfer(turn, property.rent, (byte)property.owner);
                    canvas.transform.Find("Turn").gameObject.SetActive(true);
                }
                else
                {
                    canvas.transform.Find("Turn").gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.Log("spotNada");
                if (bankAccounts[turn] >= property.Price) //Can i afford it?
                {
                    offerProperty();
                }
                else
                {
                    canvas.transform.Find("Turn").gameObject.SetActive(true);
                }
            }
        }

        else if (Array.IndexOf(chest,position) != -1)
        {
            Chance(true);
        }
        else if (Array.IndexOf(chance, position) != -1)
        {
            Chance(true);
        }

        else if (position == 4)
        {
            BankTransfer(turn, 50);
            canvas.transform.Find("Turn").gameObject.SetActive(true);
        }
        else if (position == 38)
        {
            BankTransfer(turn, 75);
            canvas.transform.Find("Turn").gameObject.SetActive(true);
        }
        else if (position == 30)
        {
            pieces[turn].imprisoned = true;
        }
        else
        {
            canvas.transform.Find("Turn").gameObject.SetActive(true);
        }

        if (position == 30)
        {
            pieces[turn].SetPosition(40);
            pieces[turn].threeTimer = 0;
            NextTurn();
        }

        else if (bankAccounts[turn] <= 0)
        {
            OuttaMoney();
        }
        else
            state = "idle";
        
    }


    void offerProperty()
    {
        Debug.Log("other");
        showCard();
        canvas.transform.Find("BuyingPanel").gameObject.SetActive(true);
    }

    public void BuyProperty()
    {
        byte position = pieces[turn].position;
        MProperty property = MPropertys.AllProperteys[position];
        BankTransfer(-property.Price);
        property.owner = turn;
        GameObject clone = Instantiate(chips[turn],transform.Find("Chips"));
        clone.transform.position = BoardStatic.GetPosition(position);
        clone.GetComponent<Chip>().pos = position;
        clone.name = turn.ToString() + position.ToString();
    }

    public void UpgradeProperty()
    {
        BankTransfer(-((MBuilding)MPropertys.AllProperteys[chip.pos]).Upgrade());
        PropertyPanel();
    }
    public void  DowngradeProperty()
    {
        BankTransfer(((MBuilding)MPropertys.AllProperteys[chip.pos]).DownGrade());
        PropertyPanel();
    }

    public void Bail()
    {
        bankAccounts[turn] -= 50;
        pieces[turn].imprisoned = false;
        pieces[turn].SetPosition(10);
        NextTurn();
    }

    void OuttaMoney()
    {
        state = "poor";
        canvas.transform.Find("Turn").Find("GiveUp").gameObject.SetActive(true);                                                                             
    } 

    public void PropertyPanel()
    {
        propertyPanelobj.SetActive(true); 
        if (
            (MPropertys.AllProperteys[chip.pos].morgaged ? 
            (int)(MPropertys.AllProperteys[chip.pos].Morgage * 1.1f) : 
            ((MBuilding)MPropertys.AllProperteys[chip.pos]).buildingPrice)
            < bankAccounts[turn]) {
            state = "browsing";
            propertyPanelobj.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().interactable = ((MBuilding)MPropertys.AllProperteys[chip.pos]).Upgradeable();
        }
        else
        {
            propertyPanelobj.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        state = "browsing";
        propertyPanelobj.transform.GetChild(2).GetComponent<UnityEngine.UI.Button>().interactable = ((MBuilding)MPropertys.AllProperteys[chip.pos]).DownGradeable();
        chip.Reload();
    }
    public void Chance(bool chest)
    {
        int card = chest ? UnityEngine.Random.Range(0, 15) : UnityEngine.Random.Range(15, 30);
        chanceCard.GetComponent<MeshRenderer>().material = (Material)Resources.Load("ChanceChest/Materials/"+card);
        chanceCard.GetComponent<Animator>().SetTrigger("ShowOff");

        if (ChanceCards.Chance(card, turn))
            WhatsOnPos();
        else
        {
            canvas.transform.Find("Turn").gameObject.SetActive(true);
        }
        
    }
    public void showCard()
    {
        propertyCard.GetComponent<MeshRenderer>().material = (Material)Resources.Load(pieces[turn].position.ToString());
        propertyCard.GetComponent<Animator>().SetTrigger("Show");
    }
    public void showCard(byte pos)
    {
        propertyCard.GetComponent<MeshRenderer>().material = (Material)Resources.Load(pos.ToString());
        propertyCard.GetComponent<Animator>().SetTrigger("Show");
    }
    public void BacktoIdle()
    {
        state = "idle";
    }
    public void Trade()
    {
        int payment = int.Parse(canvas.transform.Find("Trade").GetComponentInChildren<TMP_InputField>().text);
        byte position = chip.pos;
        MProperty property = MPropertys.AllProperteys[position];
        BankTransfer(turn,payment,(byte)property.owner);
        property.owner = turn;
        Destroy(chip.gameObject);
        GameObject clone = Instantiate(chips[turn], transform.Find("Chips"));
        clone.transform.position = BoardStatic.GetPosition(position);
        clone.GetComponent<Chip>().pos = position;
        clone.name = turn.ToString() + position.ToString();
        if (bankAccounts[turn] <= 0)
            OuttaMoney();
    }
    public void NextTurn()
    {
        do
        {
            turn = (byte)((turn + 1 >= pieces.Length) ? 0 : turn + 1);
        } while (bankAccounts[turn] < 0);
        Debug.Log("sup");
        canvas.transform.Find("Roll").gameObject.SetActive(true);
        canvas.transform.Find("CurrentTurn").GetComponent<TextMeshProUGUI>().text = new string[4] { "Blue", "Green", "Red", "Yellow" }[turn] + "'s turn";
        canvas.transform.Find("TotalTurns").GetComponent<TextMeshProUGUI>().text = "Turn: " + ++turnCount;
    }
    public void BankTransfer(int money)
    {
        bankAccounts[turn] += money;
        moneyUI[turn].text = moneyUI[turn].gameObject.name + ": " + bankAccounts[turn];
    }
    public void BankTransfer(byte from, int money)
    {
        bankAccounts[from] -= money;
        moneyUI[from].text = moneyUI[from].gameObject.name + ": " + bankAccounts[from];
    }
    public void BankTransfer(byte from, int money, byte to)
    {
        bankAccounts[from] -= money;
        bankAccounts[to] += money;
        moneyUI[from].text = moneyUI[from].gameObject.name + ": " + bankAccounts[from];
        moneyUI[to].text = moneyUI[to].gameObject.name + ": " + bankAccounts[to];
    }


    public void Death()
    {
        if (bankAccounts[turn] < 0)
        {
            foreach (MProperty property in MPropertys.AllProperteys.Values.Where(x => x.owner == turn))
            {
                property.owner = -1;
                if (property is MBuilding)
                {
                    ((MBuilding)property).buildingLevel = 0;
                }
            }
            bankAccounts[turn] = -1;
            for (int i = 0; i < transform.Find("Chips").childCount; i++)
            {
                Transform chip = transform.Find("Chips").GetChild(i);
                byte owner = byte.Parse(chip.name[0].ToString());

                if (owner == turn)
                {
                    Destroy(chip.gameObject);
                }
            }
        }
        canvas.transform.Find("Turn").gameObject.SetActive(false);
        byte acountsAboveMinus = 0;
        for(int i = 0; i < 4 ; i++)
        {
            if (bankAccounts[i] >= 0)
            {
                acountsAboveMinus++;
            }
        }
        if (acountsAboveMinus == 1)
            GameWon();
        else
            NextTurn();
    }

    void GameWon()
    {
        Debug.Log("oyoyoyo");
        state = "winin";
        byte winner = 0;
        for (byte i = 0; i < 4; i++)
        {
            if (bankAccounts[i] >= 0)
            {
                winner = i; break;
            }
        }
        canvas.transform.Find("Win").GetChild(0).GetComponent<TextMeshProUGUI>().text = new string[4] { "Blue", "Green", "Red", "Yellow" }[winner] + " Wins!!!";
        canvas.transform.Find("Win").gameObject.SetActive(true);
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
