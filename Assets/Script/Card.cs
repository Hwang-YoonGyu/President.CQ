using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    AudioSource audioSource;
    public string CardCode = "H13";
    public RectTransform rect;
    private Vector3 wasPosition;
    private bool isInSubmitDeck;
    private bool isInMyDeck;
    public User user;
    public GameManager gameManager;
    private bool isSelected;
    private int count = 0;

    void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameManager.ControlSwitch)
        {
            wasPosition = rect.position;
            rect.localScale = new Vector3(1.3f, 1.3f, 1.3f);
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
            
            if (isInSubmitDeck)
            {
                string lastCard;
                if (gameManager.tempCard.Count == 0)
                {
                    lastCard = gameManager.submittedCard.Count == 0 ? "no" : gameManager.submittedCard[gameManager.submittedCard.Count - 1].Substring(1,2);

                    if (lastCard == "no" || lastCard.CompareTo(CardCode.Substring(1, 2)) <= 0)
                    {
                        gameManager.Temp(CardCode);
                        Destroy(rect.gameObject);
                        user.changeColor(CardCode.Substring(1, 2));
                        GameObject.Find("cardsound").GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        rect.position = wasPosition;
                    }
                }
                else 
                {
                    lastCard = gameManager.tempCard[gameManager.tempCard.Count - 1].Substring(1, 2);
                    //lastCard = gameManager.tempCard[0].Substring(1, 2);
                    if (lastCard == "no" || lastCard == "CR" || lastCard == "BK" || lastCard.CompareTo(CardCode.Substring(1, 2)) == 0)
                    {
                        gameManager.Temp(CardCode);
                        Destroy(rect.gameObject);
                        user.changeColor(CardCode.Substring(1, 2));
                        GameObject.Find("cardsound").GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        rect.position = wasPosition;
                    }


                }

                
            }
            else if(isInMyDeck)
            {
                gameManager.RemoveCard(CardCode);
                user.changeColor(CardCode.Substring(1, 2));


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
            isInSubmitDeck = true;
        }
        if(collision.gameObject.name =="MyDeck")
        {
            isInMyDeck =true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SubmitManager")
        {
            isInSubmitDeck = false;
        }
        if (collision.gameObject.name == "MyDeck")
        {
            isInMyDeck = false;
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
