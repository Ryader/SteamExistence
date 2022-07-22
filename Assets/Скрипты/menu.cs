using UnityEngine;
using UnityEngine.SceneManagement;
public class menu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void multiplayer()
    {
        SceneManager.LoadScene(2);
    }

    public void URL()
    {
        Application.OpenURL("https://gamerrksi.azurewebsites.net");
    }

    public void Exit()
    {
        Application.Quit();
    }
}