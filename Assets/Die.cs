using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    Vector3 strPos;
    Quaternion strRot;
    // Start is called before the first frame update
    void Start()
    {
        strPos = transform.position;
        strRot = transform.rotation;
    }

     public void launch()
    {
        transform.position = strPos;
        transform.rotation = strRot;
        GetComponent<Rigidbody>().AddForce(transform.forward * 32);
        transform.rotation = Random.rotation;
        StartCoroutine(WaitForDiceToStop());
    }

    byte ReadDice()
    {
        if (transform.forward == -Vector3.up)
        {
            return 1;
        }
        else if (transform.right == Vector3.up)
        {
            return 2;
        }
        else if (transform.up == Vector3.up)
        {
            return 3;
        }
        else if (transform.up == -Vector3.up)
        {
            return 4;
        }
        else if (transform.right == -Vector3.up)
        {
            return 5;
        }
        else if (transform.forward == Vector3.up)
        {
            return 6;
        }
        return 0;
    }

    IEnumerator WaitForDiceToStop()
    {
        yield return new WaitForSeconds(3f);
        byte i = 0;
        while (GetComponent<Rigidbody>().angularVelocity.magnitude != 0 || GetComponent<Rigidbody>().velocity.magnitude != 0) {
            if (i >= 255)
            {
                stuck();
            }
            yield return new WaitForSeconds(0.2f);
        }
        i = 0;
        while (ReadDice() == 0)
        {
            if (i >= 4)
            {
                Debug.Log(ReadDice().ToString() + " ROT: " + transform.rotation.ToString());
            }
            
            GetComponent<Rigidbody>().AddForce(Vector3.up);
            if(i >= 128)
            {
                stuck();
            }
            i++;
            yield return new WaitForSeconds(0.1f);
        }
        printy();
    }
    void done()
    {
        transform.parent.GetComponent<DiceThrow>().diceResult += ReadDice();
    }
    void printy()
    {
        byte outy = ReadDice();
        Debug.Log(outy);
    }
    void stuck()
    {
        Debug.LogWarning("Die Stuck");
        StopAllCoroutines();
        launch();
    }
}
