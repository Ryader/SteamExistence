using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{

    public static GameSetup gameSetup;

    public Transform[] spawnPositions;

    public GameObject maincamera;

    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public GameObject HUD;
    public Slider hpBar;
    public Text KillBar;

    public Text AmmoTotalText;
    public Text AmmoNowText;

    public int totalKillCount;
    public int totalMAXkillCount = 10;

    public Text totalKills;
    //public Text timerText;

    //public float timer = 150f;
    //public DateTime timerEnd;
    public bool matchEnded;
    public GameObject finalPanel;
    public Text myName;

    private void OnEnable()
    {
        if (GameSetup.gameSetup == null)
        {
            GameSetup.gameSetup = this;

        }
    }

    void Start()
    {
        //timerEnd = DateTime.Now.AddSeconds(timer);
        matchEnded = false;
    }
    void Update()
    {
        if(totalKillCount == totalMAXkillCount)
        {
            finalPanel.SetActive(true);
            DisconnectMe();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!GameIsPaused)
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        //totalKills.text = totalKillCount.ToString() + "/" + totalMAXkillCount.ToString() + " kills";

    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        
        GameIsPaused = true;
    }

    public void DisconnectMe()
    {
        StartCoroutine(DisconnectAndShowLobby());
    }
    IEnumerator DisconnectAndShowLobby()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected) yield return null;
        SceneManager.LoadScene(2);
    }
}
