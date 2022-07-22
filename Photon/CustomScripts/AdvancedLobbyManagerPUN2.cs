using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdvancedLobbyManagerPUN2 : MonoBehaviourPunCallbacks, ILobbyCallbacks
{

    public static AdvancedLobbyManagerPUN2 lobby;
    public RoomOptions roomOptions = new RoomOptions();
    public InputField JoinRoomNameInputField;
    public InputField RoomNameInputField;
    public InputField MaxCountOfPlayers;
    public GameObject UI;
    public GameObject layer1UI;
    public GameObject CornerUI;
    public GameObject CreateUI;
    public GameObject nameSetWindow;
    public PlayerNameInput nameSet;
    public GameObject waitingPanel;
    public GameObject JoinUI;
    public GameObject roomListing;
    public Transform roomsPanel;
    public Toggle isVisibleToggle;
    public Toggle isOpenToggle;
    //public Slider killLimit;
    //public Text limitText;


    private void Awake()
    {
        lobby = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        //limitText.text = Convert.ToInt32(killLimit.value).ToString();
        if (!isOpenToggle.isOn)
        {
            isVisibleToggle.isOn = false;
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.AutomaticallySyncScene = true;
        waitingPanel.SetActive(false);
        nameSetWindow.SetActive(false);
        UI.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        RemoveRoomListings();
        foreach (RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }

    void RemoveRoomListings()
    {
        while (roomsPanel.childCount != 0)
        {
            Destroy(roomsPanel.GetChild(0).gameObject);
        }
    }

    void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListing, roomsPanel);
            roomOnClick tempButton = tempListing.GetComponent<roomOnClick>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.PlayerCount.ToString() + "/" + room.MaxPlayers.ToString();
            tempButton.SetRoom();
            if (room.PlayerCount == room.MaxPlayers)
            {
                Button[] roombuttons = GameObject.FindObjectsOfType<Button>();
                foreach (var item in roombuttons)
                {
                    if (item.gameObject.name == "RoomListItem" && room.MaxPlayers == room.PlayerCount)
                        item.enabled = false;

                    if (item.gameObject.name == "RoomListItem" && room.MaxPlayers > room.PlayerCount)
                        item.enabled = true;
                }

            }
        }
    }

    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            try
            {
                PhotonNetwork.JoinLobby();
            }
            catch (Exception exc)
            {
                Debug.LogError(exc);
            }
        }
    }

    public void OnMenuClick()
    {
        PhotonNetwork.Disconnect();
        Destroy(Rooms.room.gameObject);
        SceneManager.LoadScene(0);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

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
        byte count = byte.Parse(MaxCountOfPlayers.text);
        if (RoomNameInputField.text != "" && count > 0 && count < 9)
        {
            
            roomOptions.MaxPlayers = count;
            roomOptions.IsOpen = isOpenToggle;
            roomOptions.IsVisible = isVisibleToggle;
            //AdvancedRooms.room.maxKills = Convert.ToInt32(killLimit.value);


            //Selector = GetComponent<Dropdown>().options[GetComponent<Dropdown>().value].text;
            PhotonNetwork.CreateRoom(RoomNameInputField.text, roomOptions, TypedLobby.Default);
            Debug.Log("Room " + RoomNameInputField.text + " was created! \nOptions: \nOpened = " + roomOptions.IsOpen.ToString() + "\nVisible = " + roomOptions.IsVisible.ToString());
            CreateUI.SetActive(false);
            CornerUI.SetActive(true);

            //UI.SetActive(false);
        }
        else
        {
            Debug.LogError("Please set roomname and max players count(2-8)");
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
    public void ShowNameInput()
    {
        CornerUI.SetActive(false);
        nameSetWindow.SetActive(true);
    }


    public void HideNameInput()
    {
        CornerUI.SetActive(true);
        nameSetWindow.SetActive(false);
    }
}
