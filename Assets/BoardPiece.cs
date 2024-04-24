using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    public
    byte position = 0;
    [SerializeField]
    byte piece;

    void Start()
    {
        //position = 0;
        //SetPosition(position);
    }
    void SetPosition(byte newPosition)
    {
        transform.localPosition = BoardStatic.GetPiecePosition(newPosition, piece);
        position = newPosition;
    }

    IEnumerator JumpToPostion(byte endPosition)
    {
        byte currentPosition = position;
        float time;
        Vector3 srtPos;
        Vector3 endPos = transform.localPosition;
        while (currentPosition != endPosition)
        {
            srtPos = endPos;
            if (currentPosition + 1 >= 40)
                currentPosition = 0;
            else
                currentPosition++;

            endPos = BoardStatic.GetPiecePosition(currentPosition, piece);
            time = 0f;
            while (time < 1f)
            {
                transform.localPosition = Vector3.Lerp(srtPos, endPos, time) + (Vector3.up * (time > 0.5 ? -time + 1 : time));
                time += Time.deltaTime;
                yield return null;
            }
            
        }
        transform.localPosition = endPos;
        position = endPosition;
    }

    public void AddPosition(byte positon)
    {
        position += positon;
        if (position > 40)
        {
            position -= 40;
        }
        StartCoroutine(JumpToPostion(position));
    }
}
