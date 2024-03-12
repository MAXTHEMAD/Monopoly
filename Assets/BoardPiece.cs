using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    [SerializeField]
    byte position;
    [SerializeField]
    byte piece;

    [SerializeField]
    List<GameObject> hast;
    void setPosition(byte newPosition)
    {

    }

    IEnumerator JumpToPostion(byte endPosition)
    {
        byte currentPosition = position;
        float time;
        Vector3 srtPos;
        Vector3 endPos;
        while (currentPosition != endPosition)
        {
            srtPos = transform.localPosition;
            endPos = BoardStatic.GetPiecePosition((byte)(currentPosition+1), piece);                                       
            time = 0f;
            while (time < 1f)
            {
                transform.localPosition = Vector3.Lerp(srtPos, endPos, time) + (Vector3.up * (time > 0.5 ? -time + 1 : time));
                time += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = endPos;
            currentPosition++;
        }
        Debug.Log("pog");
    }
    private void Start()
    {
        piecePlacmentExample();
        StartCoroutine(JumpToPostion(40));
    }



    private void piecePlacmentExample()
    {
        GameObject clone;
        for (int i = 0; i < 42; i++)
        {
            clone = Instantiate(hast[0], transform.parent);
            clone.transform.localPosition = BoardStatic.GetPiecePosition((byte)i, 0);
            clone.transform.localScale = Vector3.one * 2.5f;
            clone = Instantiate(hast[0], transform.parent);
            clone.transform.localPosition = BoardStatic.GetPiecePosition((byte)i, 1);
            clone.transform.localScale = Vector3.one * 2.5f;
            clone = Instantiate(hast[0], transform.parent);
            clone.transform.localPosition = BoardStatic.GetPiecePosition((byte)i, 2);
            clone.transform.localScale = Vector3.one * 2.5f;
            clone = Instantiate(hast[0], transform.parent);
            clone.transform.localPosition = BoardStatic.GetPiecePosition((byte)i, 3);
            clone.transform.localScale = Vector3.one * 2.5f;
        }
    }
}
