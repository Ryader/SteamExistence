using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdvancedRooms : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Информация о комнатах
    public static AdvancedRooms room;
    private PhotonView photonView;

    public int currentScene;
    public int inRoomSceneNumber;
    public bool isGameLoaded;

    //Информация об игрокке
    Player[] photonPlayers;
    public int playersInRoom;
    public int myIdInRoom;
    public int playersInGame;

    public int maxKills;

    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float atMaxPlayers;
    private float timeToStart;

    public GameObject GOlobby;
    public GameObject GOroom;
    public Transform playersPanel;
    public GameObject playerListingPref;
    public GameObject MasterStartBtn;

    private void Awake()
    {
        if (AdvancedRooms.room == null) AdvancedRooms.room = this;
        else
        {
            if (AdvancedRooms.room != this)
            {
                Destroy(AdvancedRooms.room.gameObject);
                AdvancedRooms.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == inRoomSceneNumber)
        {
            CreatePlayer();
            //GameSetup.gameSetup.totalMAXkillCount = maxKills;
        }
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined");

        GOlobby.SetActive(false);
        GOroom.SetActive(true);


        CleaningPlayerList();
        ListPlayers();


        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myIdInRoom = playersInRoom;
        if(playersInRoom > 1)
        {
            readyToCount = true;
        }
        if(playersInRoom == AdvancedLobbyManagerPUN2.lobby.roomOptions.MaxPlayers)
        {
            readyToStart = true;
            if (!PhotonNetwork.IsMasterClient) return;
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        //LoadGame();
    }
    void CleaningPlayerList()
    {
        for (int item = playersPanel.childCount -1; item>=0; item --)
        {
            Destroy(playersPanel.GetChild(item).gameObject);
        }
    }

    void ListPlayers()
    {
        if(PhotonNetwork.InRoom)
        {
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                GameObject tempListItem = Instantiate(playerListingPref, playersPanel);
                Text tempText = tempListItem.transform.GetChild(0).GetComponent<Text>();
                tempText.text = player.NickName;
            }
        }
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        atMaxPlayers = 8;
        timeToStart = startingTime;
    }
    void Update()
    {
        if (PhotonNetwork.IsMasterClient && playersInRoom == AdvancedLobbyManagerPUN2.lobby.roomOptions.MaxPlayers)
        {
            MasterStartBtn.SetActive(true);
        }

    }

    public void LoadGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        PhotonNetwork.LoadLevel(inRoomSceneNumber);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
        CleaningPlayerList();
        ListPlayers();

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " Has left the match");
        CleaningPlayerList();
        ListPlayers();
        playersInRoom--;
        if(PhotonNetwork.IsMasterClient)
        {
            MasterStartBtn.SetActive(false);
        }

    }

    public void LeavingRoom()
    {
        GOroom.SetActive(false);
        PhotonNetwork.LeaveRoom();
        GOlobby.SetActive(true);
    }
}
