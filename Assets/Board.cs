using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Board : MonoBehaviour
{
    [SerializeField]
    DiceThrow diceThrow;
    [SerializeField]
    BoardPiece[] pieces;
    [SerializeField]
    GameObject[] chips;
    [SerializeField]
    GameObject propertyCard;


    public string state = "active";

    private byte turn;
    int[] bankAccounts;

    TextMeshProUGUI[] moneyUI;
    GameObject propertyPanelobj;
    Canvas canvas;
    Chip chip;
    public void StartGame()
    {
        bankAccounts = new int[4] { 1500, 1500, 1500, 1500 };
        turn = 0;
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        moneyUI = GameObject.Find("Money").GetComponentsInChildren<TextMeshProUGUI>();
        propertyPanelobj = canvas.transform.Find("PropertyPanel").gameObject;
        StartGame();
        /*Debug.Log(AnythingOnSpot(11));
        Debug.Log(MPropertys.AllProperteys[11].owner);
        MProperty property = MPropertys.AllProperteys[11];
        Debug.Log(property.owner);
        BuyProperty();
        Debug.Log(MPropertys.AllProperteys[11].owner);
        Debug.Log(property.owner);*/
        //pieces[turn-1].AddPosition(11);
        //propertyCard.GetComponent<MeshRenderer>().material = Resources.Load<Material>(pieces[turn-1].position.ToString());
        //Debug.Log(pieces[turn].position.ToString());
        //Debug.Log(pieces[turn].position);
        /*pieces[turn].position = 8;
        propertyCard.GetComponent<MeshRenderer>().material = Resources.Load<Material>(pieces[turn - 1].position.ToString());
        Debug.Log(pieces[turn].position.ToString());
        Debug.Log(pieces[turn].position);*/

        char hat = '5';
        byte ham = (byte.Parse)(hat.ToString());
        Debug.Log(ham);
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
                        byte owner = byte.Parse(objectHit.name[9].ToString());
                        if (owner == turn)
                        {
                            Debug.Log("ham");
                            chip = objectHit.GetComponent<Chip>();
                            PropertyPanel();
                        }
                        Debug.Log(owner);
                    }
                    Debug.Log("adwwd");
                }
            }
        }
    }

    public void MovePiece(bool quick = false)
    {
        state = "active";
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
        Debug.Log("rolldone");
        Debug.Log(turn);
        if (!pieces[turn].imprisoned)
        {
            if (pieces[turn].position + diceThrow.diceResult > 39)
                bankAccounts[turn] += 200;
            pieces[turn].AddPosition((byte)diceThrow.diceResult);
        } else
        {

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
                    if (bankAccounts[turn] >= property.rent)
                    {
                        BankTransfer(turn, property.rent, (byte)property.owner);
                        canvas.transform.Find("NextTurn").gameObject.SetActive(true);
                    }
                    else
                        CantAffordRent(property);
                }
            }
            else
            {
                Debug.Log("spotNada");
                if (bankAccounts[turn] >= property.Price) //Can i afford it?
                {
                    offerProperty(property);
                }
            }
        }



        else if (position == 4)
        {
            BankTransfer(turn, 50);
            canvas.transform.Find("NextTurn").gameObject.SetActive(true);
        }
        else if (position == 38)
        {
            BankTransfer(turn, 75);
            canvas.transform.Find("NextTurn").gameObject.SetActive(true);
        }
        else
        {
            canvas.transform.Find("NextTurn").gameObject.SetActive(true);
        }
        state = "idle";
        
    }


    void offerProperty(MProperty property)
    {
        Debug.Log("other");
        showCard();
        canvas.transform.Find("BuyingPanel").gameObject.SetActive(true);
    }

    public void BuyProperty()
    {
        MProperty property = MPropertys.AllProperteys[pieces[turn].position];
        BankTransfer(turn, property.Price);
        property.owner = turn;
        GameObject clone = Instantiate(chips[turn],transform);
        clone.transform.position = BoardStatic.GetPosition(pieces[turn].position);
        clone.GetComponent<Chip>().pos = pieces[turn].position;
    }

    public void UpgradeProperty()
    {
        BankTransfer(((MBuilding)MPropertys.AllProperteys[pieces[turn].position]).Upgrade());
        PropertyPanel();
    }
    public void  DowngradeProperty()
    {
        BankTransfer(((MBuilding)MPropertys.AllProperteys[pieces[turn].position]).DownGrade());
        PropertyPanel();
    }

    void CantAffordRent(MProperty property)
    {
        state = "poor";
        canvas.transform.Find("NextTurn").GetComponent<UnityEngine.UI.Button>().interactable = false;
        // ;)
    } 

    public void PropertyPanel()
    {
        propertyPanelobj.SetActive(true);
        showCard();
        bool poor = state == "poor";
        if (!poor && 
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
        if(poor)
            state = "browsing";
        propertyPanelobj.transform.GetChild(2).GetComponent<UnityEngine.UI.Button>().interactable = ((MBuilding)MPropertys.AllProperteys[chip.pos]).DownGradeable();
        chip.Reload();
    }

    public void showCard()
    {
        propertyCard.GetComponent<MeshRenderer>().material = (Material)Resources.Load(pieces[turn].position.ToString());
        propertyCard.GetComponent<Animator>().SetTrigger("Show");
    }
    public void BacktoIdle()
    {
        state = "idle";
    }
    public void NextTurn()
    {
        if (bankAccounts[turn] <= 0)
        {

        }
        turn = (byte)((turn + 1 >= pieces.Length) ? 0 : turn + 1);
    }
    public void BankTransfer(int money)
    {
        bankAccounts[turn] -= money;
        moneyUI[turn].text = moneyUI[turn].gameObject.name + ": " + bankAccounts[turn];
    }
    void BankTransfer(byte from, int money)
    {
        bankAccounts[from] -= money;
        moneyUI[from].text = moneyUI[from].gameObject.name + ": " + bankAccounts[from];
    }
    void BankTransfer(byte from, int money, byte to)
    {
        bankAccounts[from] -= money;
        bankAccounts[to] += money;
        moneyUI[from].text = moneyUI[from].gameObject.name + ": " + bankAccounts[from];
        moneyUI[to].text = moneyUI[to].gameObject.name + ": " + bankAccounts[to];
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
