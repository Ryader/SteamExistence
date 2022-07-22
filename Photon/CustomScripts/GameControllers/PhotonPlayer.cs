using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviourPunCallbacks
{

    public static PhotonPlayer pp;
    private PhotonView photonView;
    public GameObject localAvatar;
    int spawnPicker;
    int ttlKills;

    public bool isDied;


    private void OnEnable()
    {
        if (PhotonPlayer.pp == null) PhotonPlayer.pp = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            spawnPicker = UnityEngine.Random.Range(0, GameSetup.gameSetup.spawnPositions.Length);
            localAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
               GameSetup.gameSetup.spawnPositions[spawnPicker].position, GameSetup.gameSetup.spawnPositions[spawnPicker].rotation, 0);
            GameSetup.gameSetup.maincamera.SetActive(false);
            isDied = false;
            GameSetup.gameSetup.myName.text = "Nickname: " + PhotonNetwork.NickName;
            GameSetup.gameSetup.myName.color = Color.white;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public string remainingTime;
    private void Update()
    {
        if (photonView.IsMine)
        {
            //Timer();
            //if (PhotonNetwork.IsMasterClient)
            //{
            //    photonView.RPC("RPC_Timer", RpcTarget.AllBuffered, remainingTime);
            //}
            GameSetup.gameSetup.totalKills.text = GameSetup.gameSetup.totalKillCount.ToString() + "/" + GameSetup.gameSetup.totalMAXkillCount + " kills";
            GameSetup.gameSetup.totalKills.color = Color.white;
            //GameSetup.gameSetup.timerText.text = remainingTime;
            //GameSetup.gameSetup.timerText.color = Color.white;

            if(localAvatar.GetComponent<PlayerCondition>().playerHealth == 0)
            {
                localAvatar.GetComponent<PlayerMovement>().enabled = false;
                localAvatar.GetComponent<WeaponScriptForMp>().enabled = false;

                localAvatar.GetComponent<CharacterController>().enabled = false;
            }
            if (isDied)
            {
                PhotonNetwork.Destroy(localAvatar);
                spawnPicker = UnityEngine.Random.Range(0, GameSetup.gameSetup.spawnPositions.Length);

                if (photonView.IsMine)
                {
                    isDied = false;
                    localAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
                      GameSetup.gameSetup.spawnPositions[spawnPicker].position, GameSetup.gameSetup.spawnPositions[spawnPicker].rotation, 0);
                    GameSetup.gameSetup.maincamera.SetActive(false);
                }
            }

            if(GameSetup.gameSetup.GameIsPaused)
            {
                localAvatar.GetComponent<PlayerMovement>().enabled = false;
                localAvatar.GetComponent<WeaponScriptForMp>().enabled = false;

            }
            else
            {
                localAvatar.GetComponent<PlayerMovement>().enabled = true;
                localAvatar.GetComponent<WeaponScriptForMp>().enabled = true;

            }

            if (/*remainingTime == "00:00" || */GameSetup.gameSetup.totalKillCount == GameSetup.gameSetup.totalMAXkillCount)
            {
                photonView.RPC("EndMatch", RpcTarget.AllBuffered, GameSetup.gameSetup.matchEnded);
                GameSetup.gameSetup.matchEnded = true;
                //GameSetup.gameSetup.finalPanel.SetActive(true);

            }
        }
    }

    [PunRPC]
    void EndMatch(bool endOfMatch)
    {
        GameSetup.gameSetup.matchEnded = endOfMatch;
        localAvatar.GetComponent<PlayerMovement>().enabled = false;
        localAvatar.GetComponent<WeaponScriptForMp>().enabled = false;
        localAvatar.GetComponent<MouseLook>().enabled = false;
        localAvatar.GetComponent<PhotonView>().enabled = false;
        GameSetup.gameSetup.finalPanel.SetActive(true);
        GameSetup.gameSetup.HUD.SetActive(false);
    }
    //void Timer()
    //{
    //    TimeSpan delta = GameSetup.gameSetup.timerEnd - DateTime.Now;
    //    remainingTime = delta.Minutes.ToString("00") + ":" + delta.Seconds.ToString("00");
    //}
    //[PunRPC]
    //public void RPC_Timer(string syncTime)
    //{
    //    remainingTime = syncTime;
    //}
}
