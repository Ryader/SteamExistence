using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManagerPUN2 : MonoBehaviourPunCallbacks
{

    public static LobbyManagerPUN2 lobby;
    public InputField JoinRoomNameInputField;
    public InputField RoomNameInputField;
    public InputField MaxCountOfPlayers;
    public GameObject UI;
    public GameObject CornerUI;
    public GameObject CreateUI;
    public GameObject JoinUI;
    //public string Selector;


    private void Awake()
    {
        lobby = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.AutomaticallySyncScene = true;
        UI.SetActive(true);
    }

    //public void OnPlayClick()
    //{
    //    PhotonNetwork.JoinRandomRoom();
    //}

    public void OnMenuClick()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        Destroy(Rooms.room.gameObject);
        SceneManager.LoadScene(0);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
    //public override void OnJoinRandomFailed(short returnCode, string message)
    //{
    //    base.OnJoinRandomFailed(returnCode, message);
    //    CreateRoom();
    //}

    //void CreateRoom()
    //{
    //    int newRoom = Random.Range(0, 1000);

    //    RoomOptions roomOpt = new RoomOptions { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
    //    PhotonNetwork.CreateRoom("Room" + newRoom, roomOpt);
    //}

    public void ToJoinRoom()
    {
        if (JoinRoomNameInputField.text != "")
        {
            PhotonNetwork.JoinRoom(JoinRoomNameInputField.text);
            Debug.Log("Was joined to " + JoinRoomNameInputField.text + " room");
            //UI.SetActive(false);
        }
        else
        {
            Debug.Log("RoomName is null, you've been joined to random room");
            PhotonNetwork.JoinRandomRoom();
            //JOCPanel.SetActive(false);
            //RoomListPanel.SetActive(false);
            //errorNameRoomMessage.SetActive(true);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.LogWarning("No one room found");
    }

    public void ToCreateRoom()
    {
        if (RoomNameInputField.text != "")
        {
            RoomOptions roomOptions = new RoomOptions();

            byte count = byte.Parse(MaxCountOfPlayers.text);
            roomOptions.MaxPlayers = count;

            //Selector = GetComponent<Dropdown>().options[GetComponent<Dropdown>().value].text;
            PhotonNetwork.CreateRoom(RoomNameInputField.text, roomOptions, TypedLobby.Default);
            Debug.Log("Room " + RoomNameInputField.text + " was created!");
            //UI.SetActive(false);
        }
        else
        {
            Debug.LogError("RoomName is null");
            //JOCPanel.SetActive(false);
            //RoomListPanel.SetActive(false);
            //errorNameRoomMessage.SetActive(true);
        }
    }

    public void ShowJoin()
    {
        CornerUI.SetActive(false);
        JoinUI.SetActive(true);
    }
    public void HideJoin()
    {
        CornerUI.SetActive(true);
        JoinUI.SetActive(false);
    }
    public void ShowCreate()
    {
        CornerUI.SetActive(false);
        CreateUI.SetActive(true);
    }
    public void HideCreate()
    {
        CornerUI.SetActive(true);
        CreateUI.SetActive(false);
    }
}
