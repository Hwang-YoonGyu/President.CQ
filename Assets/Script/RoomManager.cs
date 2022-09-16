using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public List<string> attenderList = new List<string>();


    public Text attenderListText;
    public Text RoomNameText;

    public Button startBtn;
    public Button exitBtn;

    public PhotonView pv;

    void Start()
    {
        DontDestroyOnLoad(this);
        if (SceneManager.GetActiveScene().name == "Room_Scene")
        {
            RoomNameText.text = PhotonNetwork.CurrentRoom.Name;
            pv.RPC("AddAttender", RpcTarget.AllBuffered, PhotonNetwork.NickName);


            startBtn.onClick.AddListener(() => {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
                pv.RPC("GameStart", RpcTarget.All);
            });
            exitBtn.onClick.AddListener(() => {
                pv.RPC("RemoveAttender", RpcTarget.All, PhotonNetwork.NickName);
                PhotonNetwork.LeaveRoom();
            });
        }
    }
    public override void OnLeftRoom()
    {
        if (SceneManager.GetActiveScene().name == "Room_Scene") {
            SceneManager.LoadScene("Lobby_Scene");
            Destroy(this.gameObject);
            return;
        }
    }
    //public void RoomSetting()
    //{
    //    if (SceneManager.GetActiveScene().name == "Room_Scene")
    //    {
    //        RoomNameText.text = PhotonNetwork.CurrentRoom.Name;
    //        pv.RPC("AddAttender", RpcTarget.AllBuffered, PhotonNetwork.NickName);


    //        startBtn.onClick.AddListener(() => {
    //            pv.RPC("GameStart", RpcTarget.All);
    //        });
    //        exitBtn.onClick.AddListener(() => {
    //            pv.RPC("RemoveAttender", RpcTarget.All, PhotonNetwork.NickName);
    //            PhotonNetwork.LeaveRoom();
    //        });
    //    }
    //}
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Room_Scene")
        {
            string temp = "";
            foreach (string s in attenderList)
            {
                temp += s + "\n";
            }
            attenderListText.text = temp;

            if (PhotonNetwork.IsMasterClient)
            {
                startBtn.interactable = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Game_Scene") { 
            
        }
    }

    [PunRPC]
    void AddAttender(string nickName) {
        attenderList.Add(nickName);
    }

    [PunRPC]
    void RemoveAttender(string nickName) {
        attenderList.Remove(nickName);
    }

    [PunRPC]
    void GameStart() {
        
        PhotonNetwork.LoadLevel("Game_Scene");
    }


}
