using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Chip : MonoBehaviour
{
    Board Board;
    public byte pos;

    public void Reload()
    {
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
        ((MBuilding)MPropertys.AllProperteys[pos]).buildingLevel.ToString();
        
    }

    public void Upgrade() {
        Board.BankTransfer(((MBuilding)MPropertys.AllProperteys[pos]).Upgrade());
        Reload();
    }
    public void Downgrade() {
        Board.BankTransfer(((MBuilding)MPropertys.AllProperteys[pos]).DownGrade());
        Reload();
    }
}
