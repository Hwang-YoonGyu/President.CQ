using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class User : MonoBehaviour
{
    public List<string> userCard = new List<string>();
    public abstract string Name { get; set; }
    public List<GameObject> cardObjList = new List<GameObject>();

    public abstract void Submit(string cardcode);
    public abstract void Pass();

    public abstract void SetName();
    public abstract void SpreadCard();

    public void printCardList() {
        Debug.Log(Name+"의 카드 : ");

        foreach (string s in userCard) {
            Debug.Log(s);
        }
    }

    public bool submitCard(string lastValue, string cardcode)
    {
        //D03


        int subCard = int.Parse(lastValue.Substring(1, 1));
        int myCard = int.Parse(cardcode.Substring(1, 1));


        if (myCard > subCard)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void changeColor(string lastValue)
    {
        if (lastValue == "no")
        {
            for (int i = 0; i < cardObjList.Count; i++)
            {
                Debug.Log("돌았다!");
                cardObjList[i].GetComponentInChildren<SpriteRenderer>().color = new Color(52, 52, 52, 255);
            }
        }

        else
        {
            for (int i = 0; i < cardObjList.Count; i++)
            {

                if (!submitCard(lastValue, cardObjList[i].GetComponent<Card>().CardCode))
                {
                    cardObjList[i].GetComponentInChildren<SpriteRenderer>().color = new Color(52, 52, 52, 255);
                }

            }
        }
    }
}
