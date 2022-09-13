using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class User : MonoBehaviour
{
    public List<string> userCard = new List<string>();
    public abstract string Name { get; set; }
    public List<GameObject> cardObjList = new List<GameObject>();
    public GameManager gm;

    public abstract void Submit(string cardcode);
    public abstract void Pass();

    public abstract void SetName();
    public abstract void SpreadCard();
    public int rank = 3;
    public int score = 0;

    public Sprite rank1;
    public Sprite rank2;
    public Sprite rank3;
    public Sprite rank4;

    public Image face;

    public void setFace() {

        switch (rank) {
            case 1: face.sprite = rank1; break;
            case 2: face.sprite = rank2; break;
            case 3: face.sprite = rank3; break;
            case 4: face.sprite = rank4; break;
        }
        return;
    }

    public void turnOnLigit() {
        GetComponent<Image>().color = Color.yellow;
    }

    public void turnOffLigit()
    {
        GetComponent<Image>().color = Color.white;
    }


    public int sumCardValue() 
    {
        int sum = 0;

        foreach (string s in userCard) {
            if (s == "JBK" || s == "JCR")
            {
                sum += 14;
            }
            else if (gm.currentDirection)
            {
                sum += int.Parse(s.Substring(1, 2));
            }
            else 
            {
                sum += 14 - int.Parse(s.Substring(1, 2));
            }
        }

        return sum;
    }



    public void changeColor(string lastValue)
    {
        //첫번째 턴일때
        if (lastValue != "no")
        {
            for (int i = 0; i < cardObjList.Count; i++)
            {
                string temp = "";
                //낼 수 없는 카드면 어둡게
                try
                {
                    temp = cardObjList[i].GetComponent<Card>().CardCode.Substring(1, 2);

                } catch {
                    temp = "no";
                }



                if (gm.tempCard.Count == 0)
                {
                    if (gm.currentDirection)
                    {
                        if (temp != "no" && temp.CompareTo(lastValue) > 0)
                        {
                            cardObjList[i].GetComponent<Image>().color = new Color(52f / 255f, 52f / 255f, 52f / 255f, 255f / 255f);

                        }
                    }
                    else
                    {
                        if (temp != "no" && temp.CompareTo(lastValue) < 0)
                        {
                            cardObjList[i].GetComponent<Image>().color = new Color(52f / 255f, 52f / 255f, 52f / 255f, 255f / 255f);

                        }
                    }
                }
                else
                {
                    if (temp != "no" && temp.CompareTo(lastValue) != 0)
                    {
                        cardObjList[i].GetComponent<Image>().color = new Color(52f / 255f, 52f / 255f, 52f / 255f, 255f / 255f);

                    }
                }
            }
        }
    }
}
