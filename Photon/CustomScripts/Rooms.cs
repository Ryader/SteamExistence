using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Rooms : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Информация о комнатах
    public static Rooms room;
    private PhotonView photonView;

    public int currentScene;
    public int inRoomSceneNumber;

    //Информация об игрокке
    Player[] photonPlayers;
    public int playersInRoom;
    public int myIdInRoom;
    public int playersInGame;

    private void Awake()
    {
        if (Rooms.room == null) Rooms.room = this;
        else
        {
            if (Rooms.room != this)
            {
                Destroy(Rooms.room.gameObject);
                Rooms.room = this;
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
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myIdInRoom = playersInRoom;
        PhotonNetwork.NickName = "Marauder " + myIdInRoom.ToString();
        LoadGame();
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void LoadGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        PhotonNetwork.LoadLevel(inRoomSceneNumber);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " Has left the match");
        playersInRoom--;
    }
}
