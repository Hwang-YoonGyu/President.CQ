using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Me : User
{
    public Text nameText;
    public GameObject myDeck;

    public GameObject card;
    public override string Name { get; set; }

    public override void Pass()
    {
    }

    public override void SetName()
    {
        nameText.text = Name;
    }

    public override void SpreadCard()
    {
        int i = 0;
        foreach (string s in userCard) {

            GameObject temp = Instantiate(card, new Vector3(500 + (i*95),200,0), Quaternion.identity);
            StartCoroutine(UIAnimation.fadeIn(temp));
            temp.GetComponent<RectTransform>().SetParent(myDeck.GetComponent<RectTransform>());
            temp.name = s;
            temp.GetComponent<Card>().CardCode = s;
            temp.GetComponent<Card>().setCardImg();
            i++;
            cardObjList.Add(temp);
        }
    }

    public override void Submit(string cardcode)
    {
        userCard.Remove(cardcode);

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    

}
