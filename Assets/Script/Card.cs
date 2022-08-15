using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    public string CardCode = "H13";
    public RectTransform rect;
    private Vector3 wasPosition;
    private bool isInDeck;
    public User user;
    public GameManager gameManager;
    private bool isSelected;
    private int count = 0;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameManager.ControlSwitch)
        {
            wasPosition = rect.position;
            rect.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }

    }

    public void OnPointerClick(PointerEventData eventdata)
    {
        count++;
        //올라가라
        if (count % 2 == 1)
        {
            rect.position = rect.position +  new Vector3(0f, 50f, 0f);
        }
        //내려가라
        else
        {
            rect.position = rect.position + new Vector3(0f, -50f, 0f);
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (gameManager.ControlSwitch)
        {
            rect.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (gameManager.ControlSwitch)
        {
            rect.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            if (isInDeck)
            {
                string lastCard;
                if (gameManager.tempCard.Count == 0)
                {
                    lastCard = "no";
                }
                else 
                {
                    lastCard = gameManager.tempCard[gameManager.tempCard.Count - 1].Substring(1, 2);
                }


                if (lastCard == "no" || lastCard == "CR" || lastCard == "BK" || lastCard == CardCode.Substring(1, 2))
                {
                    gameManager.Temp(CardCode);
                    
                    Destroy(rect.gameObject);
                    user.changeColor(CardCode.Substring(1,2));
                }
                else {
                    rect.position = wasPosition;
                }
            }
            else
            {
                rect.position = wasPosition;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "SubmitManager")
        {
            isInDeck = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SubmitManager")
        {
            isInDeck = false;
        }
    }
    private void Start()
    {
        user = GameObject.Find("Me").GetComponent<User>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void setCardImg() {
        gameObject.GetComponent<CardVO>().setCardImg(CardCode);

    }

    private void OnMouseDown()
    {
        gameObject.GetComponent<GameManager>().myDeck.GetComponent<Collider2D>();
        isSelected = true;
    }
   /* public IEnumerator CardSelect(GameObject c)
    {
        c.SetActive(true);
        RectTransform rect = c.GetComponent<RectTransform>();
        float t = 0.0f;
        if (isSelected == true)
        {
            while (t <= 0.1f)
            {
                rect.position = new Vector3(0, 0 + t, 0);
                yield return null;
            }
        }
        else
        {
            rect.position = new Vector3(0, t - 1, 0);
        }
    }*/
}
