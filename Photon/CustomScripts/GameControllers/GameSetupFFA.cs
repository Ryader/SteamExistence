using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetupFFA : MonoBehaviour
{

    public static GameSetupFFA gameSetup;

    public Transform[] spawnPositions;

    public  GameObject maincamera;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject HUD;

    public Slider hpBar;

    public Text KillBar;

    private void OnEnable()
    {
        if (GameSetupFFA.gameSetup == null)
        {
            GameSetupFFA.gameSetup = this;
            
        }
    }

    void Update()
    {

        

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!GameIsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
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
