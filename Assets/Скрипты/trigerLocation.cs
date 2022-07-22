using UnityEngine;
using UnityEngine.SceneManagement;

public class trigerLocation : MonoBehaviour
{
    public int numberScene;
    public string playerTag;
    void OnTriggerStay(Collider other)
    {
        if (other.tag == playerTag)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(numberScene);
            }
        }

    }
}

