using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardVO : MonoBehaviour
{
    public Sprite D01;
    public Sprite D02;
    public Sprite D03;
    public Sprite D04;
    public Sprite D05;
    public Sprite D06;
    public Sprite D07;
    public Sprite D08;
    public Sprite D09;
    public Sprite D10;
    public Sprite D11;
    public Sprite D12;
    public Sprite D13;


    public Sprite H01;
    public Sprite H02;
    public Sprite H03;
    public Sprite H04;
    public Sprite H05;
    public Sprite H06;
    public Sprite H07;
    public Sprite H08;
    public Sprite H09;
    public Sprite H10;
    public Sprite H11;
    public Sprite H12;
    public Sprite H13;


    public Sprite S01;
    public Sprite S02;
    public Sprite S03;
    public Sprite S04;
    public Sprite S05;
    public Sprite S06;
    public Sprite S07;
    public Sprite S08;
    public Sprite S09;
    public Sprite S10;
    public Sprite S11;
    public Sprite S12;
    public Sprite S13;


    public Sprite C01;
    public Sprite C02;
    public Sprite C03;
    public Sprite C04;
    public Sprite C05;
    public Sprite C06;
    public Sprite C07;
    public Sprite C08;
    public Sprite C09;
    public Sprite C10;
    public Sprite C11;
    public Sprite C12;
    public Sprite C13;

    public Sprite JC;
    public Sprite JB;

  
    public void setCardImg(string cardCode)
    {

        switch (cardCode)
        {
            case "D01":
                gameObject.GetComponent<Image>().sprite = D01;
                break;
            case "D02":
                gameObject.GetComponent<Image>().sprite = D02;
                break;
            case "D03":
                gameObject.GetComponent<Image>().sprite = D03;
                break;
            case "D04":
                gameObject.GetComponent<Image>().sprite = D04;
                break;
            case "D05":
                gameObject.GetComponent<Image>().sprite = D05;
                break;
            case "D06":
                gameObject.GetComponent<Image>().sprite = D06;
                break;
            case "D07":
                gameObject.GetComponent<Image>().sprite = D07;
                break;
            case "D08":
                gameObject.GetComponent<Image>().sprite = D08;
                break;
            case "D09":
                gameObject.GetComponent<Image>().sprite = D09;
                break;
            case "D10":
                gameObject.GetComponent<Image>().sprite = D10;
                break;
            case "D11":
                gameObject.GetComponent<Image>().sprite = D11;
                break;
            case "D12":
                gameObject.GetComponent<Image>().sprite = D12;
                break;
            case "D13":
                gameObject.GetComponent<Image>().sprite = D13;
                break;

            case "H01":
                gameObject.GetComponent<Image>().sprite = H01;
                break;
            case "H02":
                gameObject.GetComponent<Image>().sprite = H02;
                break;
            case "H03":
                gameObject.GetComponent<Image>().sprite = H03;
                break;
            case "H04":
                gameObject.GetComponent<Image>().sprite = H04;
                break;
            case "H05":
                gameObject.GetComponent<Image>().sprite = H05;
                break;
            case "H06":
                gameObject.GetComponent<Image>().sprite = H06;
                break;
            case "H07":
                gameObject.GetComponent<Image>().sprite = H07;
                break;
            case "H08":
                gameObject.GetComponent<Image>().sprite = H08;
                break;
            case "H09":
                gameObject.GetComponent<Image>().sprite = H09;
                break;
            case "H10":
                gameObject.GetComponent<Image>().sprite = H10;
                break;
            case "H11":
                gameObject.GetComponent<Image>().sprite = H11;
                break;
            case "H12":
                gameObject.GetComponent<Image>().sprite = H12;
                break;
            case "H13":
                gameObject.GetComponent<Image>().sprite = H13;
                break;

            case "S01":
                gameObject.GetComponent<Image>().sprite = S01;
                break;
            case "S02":
                gameObject.GetComponent<Image>().sprite = S02;
                break;
            case "S03":
                gameObject.GetComponent<Image>().sprite = S03;
                break;
            case "S04":
                gameObject.GetComponent<Image>().sprite = S04;
                break;
            case "S05":
                gameObject.GetComponent<Image>().sprite = S05;
                break;
            case "S06":
                gameObject.GetComponent<Image>().sprite = S06;
                break;
            case "S07":
                gameObject.GetComponent<Image>().sprite = S07;
                break;
            case "S08":
                gameObject.GetComponent<Image>().sprite = S08;
                break;
            case "S09":
                gameObject.GetComponent<Image>().sprite = S09;
                break;
            case "S10":
                gameObject.GetComponent<Image>().sprite = S10;
                break;
            case "S11":
                gameObject.GetComponent<Image>().sprite = S11;
                break;
            case "S12":
                gameObject.GetComponent<Image>().sprite = S12;
                break;
            case "S13":
                gameObject.GetComponent<Image>().sprite = S13;
                break;

            case "C01":
                gameObject.GetComponent<Image>().sprite = C01;
                break;
            case "C02":
                gameObject.GetComponent<Image>().sprite = C02;
                break;
            case "C03":
                gameObject.GetComponent<Image>().sprite = C03;
                break;
            case "C04":
                gameObject.GetComponent<Image>().sprite = C04;
                break;
            case "C05":
                gameObject.GetComponent<Image>().sprite = C05;
                break;
            case "C06":
                gameObject.GetComponent<Image>().sprite = C06;
                break;
            case "C07":
                gameObject.GetComponent<Image>().sprite = C07;
                break;
            case "C08":
                gameObject.GetComponent<Image>().sprite = C08;
                break;
            case "C09":
                gameObject.GetComponent<Image>().sprite = C09;
                break;
            case "C10":
                gameObject.GetComponent<Image>().sprite = C10;
                break;
            case "C11":
                gameObject.GetComponent<Image>().sprite = C11;
                break;
            case "C12":
                gameObject.GetComponent<Image>().sprite = C12;
                break;
            case "C13":
                gameObject.GetComponent<Image>().sprite = C13;
                break;

            case "JCR":
                gameObject.GetComponent<Image>().sprite = JC;
                break;
            case "JBK":
                gameObject.GetComponent<Image>().sprite = JB;
                break;

        }

    }
}
