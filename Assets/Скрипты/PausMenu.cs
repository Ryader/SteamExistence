using UnityEngine;
using UnityEngine.SceneManagement;

public class PausMenu : MonoBehaviour
{
    public float MoveSpeed = 40;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public static bool Inventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Inventory)
            {
                InventoryDeActive();
            }
            else
            {
                InventoryActive();
            }
        }

    }

    public void InventoryActive()
    {
        Inventory = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject obj = GameObject.Find("Camera holder");
        CameraHandler scriptCamera = obj.GetComponent<CameraHandler>();
        scriptCamera.enabled = false;
    }
    public void InventoryDeActive()
    {
        Inventory = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject obj = GameObject.Find("Camera holder");
        CameraHandler scriptCamera = obj.GetComponent<CameraHandler>();
        scriptCamera.enabled = true;
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject obj = GameObject.Find("Camera holder");
        CameraHandler scriptCamera = obj.GetComponent<CameraHandler>();
        scriptCamera.enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject obj = GameObject.Find("Camera holder");
        CameraHandler scriptCamera = obj.GetComponent<CameraHandler>();
        scriptCamera.enabled = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}