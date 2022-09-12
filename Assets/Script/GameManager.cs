using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    public List<User> userList;

    [SerializeField]
    List<string> attenderList;

    List<string> CardDeck = new List<string>();

    public List<string> submittedCard = new List<string>();
    public List<string> tempCard = new List<string>();

    public GameObject myDeck;
    public GameObject deck;
    public GameObject card;
    public GameObject collectMoneyPanel;

    public RectTransform deckPoint;
    public RectTransform myDeckPoint;

    public Button start_button;
    public Button end_button;
    public Button submit_button;
    public Button pass_button;

    public Text timeText;
    public Text turnText;
    public Text directionText;
    public Text submitLimitText;


    public PhotonView pv;
    public User user;
    //public RoomManager rm;
    public bool ControlSwitch = false;// if not my turn, do not controll the card
    public bool stopSwitch = false;// if value is true, timer will stop and pass the trun to other user

    public string currentTurnTime = "";
    public bool currentDirection = true; // when this value is true, the direction 3 to 2. and false is reverse direction
    public int lastCardSubmitCount = 0;


    public int cot = 0; //count of turn
    int index;

    int count;

    int roundCount = 0;
    int passCount = 0;



    Dictionary<string, int> ranking = new Dictionary<string, int>();

    private void init()
    {
        int i = 1;
        foreach (string s in attenderList)
        {
            if (PhotonNetwork.NickName == s) //when start test on PhotonNetwork, it must change PhotonNetwork.Nickname 
            {
                user.Name = s;
                user.SetName();
            }
            else
            {
                userList[i].Name = s;
                userList[i].SetName();
                i++;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        attenderList = GameObject.Find("RoomManager").GetComponent<RoomManager>().attenderList;
        if (attenderList.Count != 4) {
            for (int i = attenderList.Count; i < 4; i++) {
                attenderList.Add("AI_"+i);
            }
        }
        user = GameObject.Find("Me").GetComponent<User>();
        //rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();

        init();


        start_button.onClick.AddListener(() =>
        {
            RoundStart();
        });
        end_button.onClick.AddListener(() =>
        {
            RoundEnd();
        });
        submit_button.onClick.AddListener(() =>
        {
            Submit();
        });
        pass_button.onClick.AddListener(() =>
        {
            Pass();
        });
        if (PhotonNetwork.MasterClient.NickName == PhotonNetwork.NickName) {
            initCardDeck();
            giveCardToUser();
            pv.RPC("RoundStart",RpcTarget.All);
        }

    }


    public void initCardDeck()
    {
        CardDeck.Clear();
        CardDeck.Add("D01");
        CardDeck.Add("D02");
        CardDeck.Add("D03");
        CardDeck.Add("D04");
        CardDeck.Add("D05");
        CardDeck.Add("D06");
        CardDeck.Add("D07");
        CardDeck.Add("D08");
        CardDeck.Add("D09");
        CardDeck.Add("D10");
        CardDeck.Add("D11");
        CardDeck.Add("D12");
        CardDeck.Add("D13");

        CardDeck.Add("H01");
        CardDeck.Add("H02");
        CardDeck.Add("H03");
        CardDeck.Add("H04");
        CardDeck.Add("H05");
        CardDeck.Add("H06");
        CardDeck.Add("H07");
        CardDeck.Add("H08");
        CardDeck.Add("H09");
        CardDeck.Add("H10");
        CardDeck.Add("H11");
        CardDeck.Add("H12");
        CardDeck.Add("H13");

        CardDeck.Add("C01");
        CardDeck.Add("C02");
        CardDeck.Add("C03");
        CardDeck.Add("C04");
        CardDeck.Add("C05");
        CardDeck.Add("C06");
        CardDeck.Add("C07");
        CardDeck.Add("C08");
        CardDeck.Add("C09");
        CardDeck.Add("C10");
        CardDeck.Add("C11");
        CardDeck.Add("C12");
        CardDeck.Add("C13");

        CardDeck.Add("S01");
        CardDeck.Add("S02");
        CardDeck.Add("S03");
        CardDeck.Add("S04");
        CardDeck.Add("S05");
        CardDeck.Add("S06");
        CardDeck.Add("S07");
        CardDeck.Add("S08");
        CardDeck.Add("S09");
        CardDeck.Add("S10");
        CardDeck.Add("S11");
        CardDeck.Add("S12");
        CardDeck.Add("S13");
    }
    public IEnumerator TurnCountTime()
    {
        float time = 1500f;

        while (true) {
            time -= Time.deltaTime;
            if (time <= 0.0f || stopSwitch) {

                break;
            }

            timeText.text = $"{time:N0}";            
            yield return null;
        }
        stopSwitch = false;
    }

    public IEnumerator GameOverCountTime()
    {
        float time = 5f;

        while (true)
        {
            time -= Time.deltaTime;
            if (time <= 0.0)
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene("Lobby_Scene");
                break;
            }


            yield return null;
        }

    }



    /*---------------------------------------------------------------------------------------*/
    /*---------------------------------------------------------------------------------------*/
    /*---------------------------------------------------------------------------------------*/


    //1. 카드를 내면 (turn인 대상이 카드가 isIndeck)
    //1.1 temp Card에 카드가 추가되고
    //1.2 낸카드 정렬(deck)

    //2. 제출 버튼 누르면

    //3. 제출 버튼을 누르지 않으면(pass or countdown)

    public void Temp(string cardcode)
    {
        Debug.Log(cardcode + "덱에 올림");
        tempCard.Add(cardcode); //1.1
        user.userCard.Remove(cardcode);
        if (submittedCard.Count==0)
        {
            ArrangeCard(deckPoint, cardcode);//1.2
        }
        else ArrangeCard(deckPoint, cardcode);//1.2
    }
    public void Submit()
    {
        //2. 제출 버튼 누르면
        //2.1 User 카드리스트의 낸 카드는 비워지고

        //2.2 tempCard로 SubmittedCard는 채워지고
        //2.3 tempCard는 비워지고
        //2.4 SubmittedCard에 있는 카드들을 deck의 child로 
        //2.5 nextTurn
        
        if (lastCardSubmitCount > tempCard.Count) {
            Debug.Log("카드 장수가 너무 적음");
            return;
        }

        int sameCardCount = 0;
        string sameCardTemp = null;
        foreach (string s in tempCard) {
            if (null == sameCardTemp || s.Substring(1,2) != sameCardTemp)
            {
                sameCardTemp = s.Substring(1, 2);
            }
            else if (sameCardTemp == s.Substring(1, 2) || s.Substring(1, 2) == "CR" || s.Substring(1, 2) == "BK") 
            {
                sameCardCount++;
            }     
        }

        if (sameCardCount >= 4) {
            Debug.Log("It's Revolution!");
            if (currentDirection)
            {
                currentDirection = false;
                directionText.text = "2 -> 3";
            }
            else
            {
                currentDirection = true;
                directionText.text = "3 -> 2";
            }
        }


        for(int i=0; i<tempCard.Count; i++)
        {  
            pv.RPC("Submitted", RpcTarget.All, tempCard[i], PhotonNetwork.NickName, tempCard.Count);
        }//2.1, 2.2, 2.4

        tempCard.Clear();//2.3
        Transform[] myDeckChildren = myDeck.GetComponentsInChildren<Transform>();
        foreach (Transform child in myDeckChildren)
        {
            if (child.name != myDeck.name)
            {
                Destroy(child.gameObject);
            }
        }//나의덱에 있는 카드 먼저 싹 지워버리고
        user.SpreadCard();//가지고 있는 카드로 업데이트
        stopSwitch = true;  //2.5
        if (user.userCard.Count == 0)
        {
            pv.RPC("RoundEnd", RpcTarget.All);

        }
        else
        {
            pv.RPC("TurnEnd", RpcTarget.All, PhotonNetwork.NickName);
        }
    }
    public void Pass()
    {
        //3. 제출 버튼을 누르지 않으면(pass or countdown)
        //3.1 이동된 카드(tempCard에 있던 카드들 다시 mydeck의 child로 원 위치)
        //3.2 tempCard는 비워지고
        //3.3 nextTurn

        pv.RPC("PassCount", RpcTarget.All);

        Transform[] deckChildren = deck.GetComponentsInChildren<Transform>();
        foreach (Transform child in deckChildren)
        {
            if (child.name != deck.name )
            {
                if (tempCard.Contains(child.name))
                {
                    Destroy(child.gameObject);
                }
            }
        }//덱에 올라와 있는 카드 안낼것이니 지워버리고(temp의 담겨있는 카드들만 골라서 지우기)
        Transform[] myDeckChildren = myDeck.GetComponentsInChildren<Transform>();
        foreach (Transform child in myDeckChildren)
        {
            if (child.name != myDeck.name)
            {
                Destroy(child.gameObject);
            }
        }//나의덱에 있는 카드 먼저 싹 지워버리고
        
        foreach(string cardcode in tempCard)
        {
            user.userCard.Add(cardcode);
        }
        user.SpreadCard();//가지고 있는 카드로 업데이트
        tempCard.Clear();//3.2
        pv.RPC("TurnEnd", RpcTarget.All, PhotonNetwork.NickName);

        stopSwitch = true;
        
    }
    public void RemoveCardRPC(string cardcode)
    {
        pv.RPC("RemoveCard", RpcTarget.All, cardcode);
    }

    [PunRPC]
    public void PassCount()
    {
        passCount++;
        Debug.Log("누적 " + passCount + " pass");
        Transform[] deckChildren = deck.GetComponentsInChildren<Transform>();
        if (passCount == 3)
        {
            Debug.Log("누적 3pass => 덱초기화");
            foreach (Transform child in deckChildren)
            {
                if (child.name != deck.name)
                {

                    Destroy(child.gameObject);

                }
            }
            submittedCard.Clear();
            passCount = 0;
            lastCardSubmitCount = 0;
            count = 0;
        }
        submitLimitText.text = lastCardSubmitCount.ToString();
    }

    [PunRPC]
    public void RemoveCard(string cardcode)
    {
        Transform[] DeckChildren = deck.GetComponentsInChildren<Transform>();
        foreach (Transform child in DeckChildren)
        {
            if (child.name == cardcode)
            {
                Destroy(child.gameObject);
            }
        }
        Transform[] myDeckChildren = myDeck.GetComponentsInChildren<Transform>();
        foreach (Transform child in myDeckChildren)
        {
            if (child.name != myDeck.name)
            {
                Destroy(child.gameObject);
            }
        }//나의덱에 있는 카드 먼저 싹 지워버리고

        tempCard.Remove(cardcode);
        user.userCard.Add(cardcode);
        user.SpreadCard();
        count--;

    }
    [PunRPC]
    public void Submitted(string cardcode, string name, int lastCardSubmitCount)
    {

        this.lastCardSubmitCount = lastCardSubmitCount;
        submitLimitText.text = lastCardSubmitCount.ToString();
        if (name == PhotonNetwork.NickName)
        {
            submittedCard.Add(cardcode);
        }
        else
        {
            Debug.Log(cardcode + "카드가 제출됨");
            submittedCard.Add(cardcode);
            ArrangeCard(deckPoint, cardcode);
            foreach (User u in userList) {
                if (u.Name == name) {
                    u.userCard.Remove(cardcode);
                }
            }
        }
        passCount = 0;
        
    }

    [PunRPC]
    public void ArrangeCard(RectTransform rect, string cardcode )
    {
        GameObject temp = Instantiate(card, rect.position, Quaternion.identity); //재생성

        temp.gameObject.GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(rect.position.x + (count - 1) * 30, rect.position.y, 0), Quaternion.identity);
        temp.GetComponent<RectTransform>().SetParent(deck.GetComponent<RectTransform>());
        temp.name = cardcode;
        temp.GetComponent<Card>().CardCode = cardcode;
        temp.GetComponent<Card>().setCardImg();
        count++;

    }

    /*---------------------------------------------------------------------------------------*/
    /*---------------------------------------------------------------------------------------*/
    /*---------------------------------------------------------------------------------------*/

    [PunRPC] //01
    public void RoundStart()
    {
        //새로운 라운드 시작
        roundCount++;
        Debug.Log(roundCount + "라운드 시작");
        user.SpreadCard(); // 나의 덱에 카드를 뿌리고
        /*if (userList[0].userCard.Contains("D01")) {
            pv.RPC("TurnStart", RpcTarget.All, PhotonNetwork.NickName);
        }*/

        foreach (User u in userList) {
            u.setFace();
        }

        if(PhotonNetwork.MasterClient.NickName == PhotonNetwork.NickName)
        {
            pv.RPC("TurnStart", RpcTarget.All, PhotonNetwork.NickName);
        }

    }

    [PunRPC] //02
    public void TurnStart(string userName)
    {
        if (userName == PhotonNetwork.NickName)
        {
            ControlSwitch = true;
            pv.RPC("setTurn", RpcTarget.All, PhotonNetwork.NickName);
            user.changeColor(submittedCard.Count == 0 ? "no" : submittedCard[submittedCard.Count-1].Substring(1, 2));
            Debug.Log(submittedCard.Count == 0 ? "no" : submittedCard[submittedCard.Count - 1]);
        }
        else
        {
            ControlSwitch = false;
        }

        foreach (User u in userList) {
            if (u.Name == userName) {
                u.turnOnLigit();
                break;
            }
        }
    }

    [PunRPC] //03
    public void setTurn(string username)
    {
        //currentTurnUser = username;
        turnText.text = username;
        StopCoroutine(TurnCountTime());
        StartCoroutine(TurnCountTime());
        if(ControlSwitch)
        {
            submit_button.interactable = true;
            pass_button.interactable = true;
        }
        else
        {
            submit_button.interactable = false;
            pass_button.interactable = false;

        }
    }



    [PunRPC]
    public void TurnNext(string username)
    {
        
        TurnStart(username);
    }
    [PunRPC]
    public void TurnEnd(string username)
    {
        index = userList.FindIndex(x => x.Name == turnText.text)+1;
        if (PhotonNetwork.IsMasterClient)
        {
            //need manage sequence of turn method
            
            pv.RPC("TurnStart", RpcTarget.All, userList[(index) % 4].Name);
            //TurnNext(userList[(index + 1) % 4].Name);
            Debug.Log("다음차례는 " + userList[(index) % 4].Name);
            
        }

        foreach(User u in userList) {
            if (u.Name == username)
            {
                u.turnOffLigit();
                break;
            }
        }

    }


    [PunRPC]
    public void RoundEnd()
    {
        if (roundCount == 4)
        {
            Debug.Log("5초 후에 Lobby_Scene으로 이동");
            StartCoroutine(GameOverCountTime());
            //rm.RoomSetting();
        }

        submittedCard.Clear();// 제출된 카드 리스트 clear
        //정보 초기화
        passCount = 0;
        lastCardSubmitCount = 0;
        count = 0;
        currentDirection = true;

        submitLimitText.text = lastCardSubmitCount.ToString();
        directionText.text = "3 -> 2";


        //호스트만 돌아용~
        if (PhotonNetwork.MasterClient.NickName == PhotonNetwork.NickName)
        { 
            checkRanking();
        }

        List<User> tempList = new List<User>();

        for (int i = 1; i < 5; i++) {
            for (int j=0; j < 4; j++) {
                if (userList[j].rank == i) {
                    tempList.Add(userList[j]);
                    break;
                }
            }
        }

        userList = tempList;

        for (int i = 0; i < userList.Count; i++)
        {
            userList[i].userCard.Clear();
        }
        //모든 유저의 카드 리스트 clear

        Transform[] deckChildren = deck.GetComponentsInChildren<Transform>();
        foreach (Transform child in deckChildren)
        {
            if (child.name != deck.gameObject.name)
            {
                Destroy(child.gameObject);
            }
        }
        Transform[] myDeckChildren = myDeck.GetComponentsInChildren<Transform>();
        foreach (Transform child in myDeckChildren)
        {
            if (child.name != myDeck.gameObject.name)
            {
                Destroy(child.gameObject);
            }
        }

        if (PhotonNetwork.MasterClient.NickName == PhotonNetwork.NickName)
        {
            initCardDeck();
            giveCardToUser();

        }
        StartCoroutine(showCollectMoneyPanel());
    }




    /*---------------------------------------------------------------------------------------*/
    /*---------------------------------------------------------------------------------------*/
    /*---------------------------------------------------------------------------------------*/

    

    public void checkRanking()
    {
        for(int i = 0; i < userList.Count; i++)
        {
            userList[i].rank = 1;
        }


        for (int i = 0; i < userList.Count; i++)
        {
            for (int j = 0; j < userList.Count; j++)
            {
                if (userList[i].userCard.Count > userList[j].userCard.Count)
                {
                    userList[i].rank++;
                }
                //카드수가 같다면 && 내가 아닐때 
                else if ((userList[i].userCard.Count == userList[j].userCard.Count) && (userList[i].Name != userList[j].Name))
                {
                    //내가아닌 동점자 발생시
                   //남은 카드 코드의 숫자를 합쳐서 높은카드 더 못낸 사람이 진거임 만약 조커를 가지고있다면 조커가진 사람이 더 불리
                    if(userList[i].sumCardValue()>userList[j].sumCardValue())
                    {
                        userList[i].rank++;
                    }

                }
            }
        }
        for (int i = 0; i < userList.Count; i++) {
            pv.RPC("changeRank", RpcTarget.All, userList[i].Name, userList[i].rank);
        }
    }


    [PunRPC]
    public void changeRank(string userName, int rank) {
        foreach (User u in userList) {
            if (u.Name == userName) {
                u.rank = rank;
                u.setFace();
                break;
            }
        }
        
    }


    public IEnumerator showCollectMoneyPanel() {

        float time = 0.0f;
        StartCoroutine(UIAnimation.fadeIn(collectMoneyPanel));

        while (true) {
            if (time > 3.0f) {
                StartCoroutine(UIAnimation.fadeOut(collectMoneyPanel));
                collectMoney();
                break;
            }


            time += Time.deltaTime;
            yield return null;
        }
        
    }


    public void collectMoney() {
        //1등일때
        if (user.rank == 1)
        {
            if (currentDirection)
            {
                User receiver;

                foreach (User u in userList) {
                    if (u.rank == 4) {
                        receiver = u;

                        for (int i = 0; i < 2; i++)
                        {
                            string temp = null;

                            foreach (string card in user.userCard)
                            {
                                if (temp == null || card.Substring(1, 2).CompareTo(temp.Substring(1, 2)) < 0)
                                {
                                    temp = card;
                                }
                            }
                            pv.RPC("RemoveCard", RpcTarget.All, user.Name, temp);
                            pv.RPC("SendCard", RpcTarget.All, receiver.Name, temp);
                            Debug.Log(user.Name + "send " + temp + " to " + receiver.Name);

                        }
                    }
                }
            }
            else
            {
                User receiver;

                foreach (User u in userList)
                {
                    if (u.rank == 4)
                    {
                        receiver = u;

                        for (int i = 0; i < 2; i++)
                        {
                            string temp = null;

                            foreach (string card in user.userCard)
                            {
                                if (temp == null || card.Substring(1, 2).CompareTo(temp.Substring(1, 2)) > 0)
                                {
                                    temp = card;
                                }
                            }
                            pv.RPC("RemoveCard", RpcTarget.All, user.Name, temp);
                            pv.RPC("SendCard", RpcTarget.All, receiver.Name, temp);
                            Debug.Log(user.Name + "send " + temp + " to " + receiver.Name);

                        }
                    }
                }
            }

        }
        //2등일때
        else if (user.rank == 2)
        {
            if (currentDirection)
            {
                User receiver;

                foreach (User u in userList)
                {
                    if (u.rank == 3)
                    {
                        receiver = u;


                        string temp = null;

                        foreach (string card in user.userCard)
                        {
                            if (temp == null || card.Substring(1, 2).CompareTo(temp.Substring(1, 2)) < 0)
                            {
                                temp = card;
                            }
                        }
                        pv.RPC("RemoveCard", RpcTarget.All, user.Name, temp);
                        pv.RPC("SendCard", RpcTarget.All, receiver.Name, temp);
                        Debug.Log(user.Name + "send " + temp + " to " + receiver.Name);
                    }
                }
            }
            else
            {
                User receiver;

                foreach (User u in userList)
                {
                    if (u.rank == 3)
                    {
                        receiver = u;


                        string temp = null;

                        foreach (string card in user.userCard)
                        {
                            if (temp == null || card.Substring(1, 2).CompareTo(temp.Substring(1, 2)) > 0)
                            {
                                temp = card;
                            }
                        }
                        pv.RPC("RemoveCard", RpcTarget.All, user.Name, temp);
                        pv.RPC("SendCard", RpcTarget.All, receiver.Name, temp);
                        Debug.Log(user.Name + "send " + temp + " to " + receiver.Name);
                    }
                }
            }
        }
        //3등일때
        else if (user.rank == 3)
        {
            if (currentDirection)
            {
                User receiver;

                foreach (User u in userList)
                {
                    if (u.rank == 2)
                    {
                        receiver = u;

                        
                        string temp = null;

                        foreach (string card in user.userCard)
                        {
                            if (temp == null || card.Substring(1, 2).CompareTo(temp.Substring(1, 2)) > 0)
                            {
                                temp = card;
                            }
                        }
                        pv.RPC("RemoveCard", RpcTarget.All, user.Name, temp);
                        pv.RPC("SendCard", RpcTarget.All, receiver.Name, temp);
                        Debug.Log(user.Name + "send " + temp + " to " + receiver.Name);
                    }
                }
            }
            else
            {
                User receiver;

                foreach (User u in userList)
                {
                    if (u.rank == 2)
                    {
                        receiver = u;

                        
                        string temp = null;

                        foreach (string card in user.userCard)
                        {
                            if (temp == null || card.Substring(1, 2).CompareTo(temp.Substring(1, 2)) < 0)
                            {
                                temp = card;
                            }
                        }
                        pv.RPC("RemoveCard", RpcTarget.All, user.Name, temp);
                        pv.RPC("SendCard", RpcTarget.All, receiver.Name, temp);
                        Debug.Log(user.Name + "send " + temp + " to " + receiver.Name);

                        
                    }
                }
            }

        }
        //4등일때
        else if (user.rank == 4)
        {
            if (currentDirection)
            {
                User receiver;

                foreach (User u in userList)
                {
                    if (u.rank == 1)
                    {
                        receiver = u;

                        for (int i = 0; i < 2; i++)
                        {
                            string temp = null;

                            foreach (string card in user.userCard)
                            {
                                if (temp == null || card.Substring(1, 2).CompareTo(temp.Substring(1, 2)) > 0)
                                {
                                    temp = card;
                                }
                            }
                            pv.RPC("RemoveCard", RpcTarget.All, user.Name, temp);
                            pv.RPC("SendCard", RpcTarget.All, receiver.Name, temp);
                            Debug.Log(user.Name + "send " + temp + " to " + receiver.Name);

                        }
                    }
                }
            }
            else
            {
                User receiver;

                foreach (User u in userList)
                {
                    if (u.rank == 1)
                    {
                        receiver = u;

                        for (int i = 0; i < 2; i++)
                        {
                            string temp = null;

                            foreach (string card in user.userCard)
                            {
                                if (temp == null || card.Substring(1, 2).CompareTo(temp.Substring(1, 2)) < 0)
                                {
                                    temp = card;
                                }
                            }
                            pv.RPC("RemoveCard", RpcTarget.All, user.Name, temp);
                            pv.RPC("SendCard", RpcTarget.All, receiver.Name, temp);
                            Debug.Log(user.Name + "send " + temp + " to " + receiver.Name);
                        }
                    }
                }
            }
        }
    }

    [PunRPC]
    public void SendCard(string userName, string cardCode)
    {
        foreach (User u in userList) {
            if (userName == u.Name) {
                u.userCard.Add(cardCode);
                break;
            }
        }


    }

    [PunRPC]
    public void RemoveCard(string userName, string cardCode)
    {
        foreach (User u in userList)
        {
            if (userName == u.Name)
            {
                u.userCard.Remove(cardCode);
                break;
            }
        }


    }
    /*---------------------------------------------------------------------------------------*/
    /*---------------------------------------------------------------------------------------*/
    /*---------------------------------------------------------------------------------------*/

    [PunRPC]
    public void giveCard(string userName, string cardcode)
    {
        for (int i = 0; i < userList.Count; i++)
        {
            if (userList[i].Name == userName)
            {
                userList[i].userCard.Add(cardcode);
            }
        }
    }
    void giveCardToUser()
    {
        for (int i = 0; i < 13; i++)
        {
            int r = Random.Range(0, CardDeck.Count);
            //giveCard(userList[0].name, CardDeck[r]);
            pv.RPC("giveCard", RpcTarget.All, userList[0].Name, CardDeck[r]);
            CardDeck.RemoveAt(r);
        }
        for (int i = 0; i < 13; i++)
        {
            int r = Random.Range(0, CardDeck.Count);
            //giveCard(userList[1].name, CardDeck[r]);
            pv.RPC("giveCard", RpcTarget.All, userList[1].Name, CardDeck[r]);
            CardDeck.RemoveAt(r);
        }
        for (int i = 0; i < 13; i++)
        {
            int r = Random.Range(0, CardDeck.Count);
            //giveCard(userList[2].name, CardDeck[r]);
            pv.RPC("giveCard", RpcTarget.All, userList[2].Name, CardDeck[r]);
            CardDeck.RemoveAt(r);
        }
        for (int i = 0; i < 13; i++)
        {
            int r = Random.Range(0, CardDeck.Count);
            //giveCard(userList[3].name, CardDeck[r]);
            pv.RPC("giveCard", RpcTarget.All, userList[3].Name, CardDeck[r]);
            CardDeck.RemoveAt(r);
        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}
