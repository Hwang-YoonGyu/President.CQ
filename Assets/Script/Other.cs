using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Other :  User
{
    public Text nameText;
    public Text currentCards;

    public override string Name { get; set ; }

    public override void Pass()
    {
    }

    public override void SetName()
    {
        nameText.text = Name;

    }

    public override void SpreadCard()
    {
        Debug.Log("This is not object which is current user owned");
    }

    public override void Submit(string cardcode)
    {
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentCards.text = "남은 카드 : " + userCard.Count;

    }

    public override void StartSpreadCard()
    {
    }
}
