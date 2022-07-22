using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Location : MonoBehaviour
{
    public int numberScene;
    public string playerTag;
    void OnTriggerStay(Collider other)
    {
        Debug.Log("!");
        if (other.tag == playerTag)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(numberScene);
            }
        }
    }
}
